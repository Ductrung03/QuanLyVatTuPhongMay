using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using System.Data;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class LichThucHanhRepository : BaseRepository<LichThucHanh>, ILichThucHanhRepository
    {
        public LichThucHanhRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<LichThucHanh>> GetAllAsync()
        {
            return await _context.LichThucHanhs
                .Include(l => l.NguoiDung)
                .Include(l => l.TrangTBs)
                .ToListAsync();
        }

        public override async Task<LichThucHanh> CreateAsync(LichThucHanh entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@TrangThai", entity.TrangThai ?? (object)DBNull.Value),
                new SqlParameter("@ThoiGianBD", entity.ThoiGianBD),
                new SqlParameter("@ThoiGianKT", entity.ThoiGianKT),
                new SqlParameter("@MaNguoiDung", entity.MaNguoiDung ?? (object)DBNull.Value)
            };

            var result = await _context.Database.SqlQueryRaw<int>("EXEC sp_ThemLichThucHanh @TrangThai, @ThoiGianBD, @ThoiGianKT, @MaNguoiDung", parameters).FirstOrDefaultAsync();
            entity.MaLich = result;
            return entity;
        }

        public async Task<IEnumerable<LichThucHanh>> GetByNguoiDungAsync(int maNguoiDung)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            return await _context.LichThucHanhs
                .FromSqlRaw("EXEC sp_LayLichThucHanh NULL, @MaNguoiDung", parameters)
                .Include(l => l.NguoiDung)
                .ToListAsync();
        }

        public async Task<IEnumerable<LichThucHanh>> GetByTrangThaiAsync(string trangThai)
        {
            return await _context.LichThucHanhs
                .Include(l => l.NguoiDung)
                .Where(l => l.TrangThai == trangThai)
                .ToListAsync();
        }

        public async Task<bool> DatLichAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT, int maNguoiDung, string trangThai = "Đã đặt")
        {
            var parameters = new[]
            {
                new SqlParameter("@MaPhongMay", maPhongMay),
                new SqlParameter("@ThoiGianBD", thoiGianBD),
                new SqlParameter("@ThoiGianKT", thoiGianKT),
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@TrangThai", trangThai)
            };

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_DatLichThucHanh";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.Select(p => (IDbDataParameter)p).ToArray());

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            bool thanhCong = false;

            if (await reader.ReadAsync())
            {
                thanhCong = reader.GetBoolean("ThanhCong");
            }

            await connection.CloseAsync();
            return thanhCong;
        }

        public async Task<bool> HuyLichAsync(int maLich, int maNguoiDung)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLich", maLich),
                new SqlParameter("@MaNguoiDung", maNguoiDung)
            };

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_HuyLichThucHanh";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.Select(p => (IDbDataParameter)p).ToArray());

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            bool thanhCong = false;

            if (await reader.ReadAsync())
            {
                thanhCong = reader.GetBoolean("ThanhCong");
            }

            await connection.CloseAsync();
            return thanhCong;
        }

        public async Task<IEnumerable<LichThucHanh>> GetLichTrongKhoangAsync(DateTime tuNgay, DateTime denNgay)
        {
            return await _context.LichThucHanhs
                .Include(l => l.NguoiDung)
                .Where(l => l.ThoiGianBD.Date >= tuNgay.Date && l.ThoiGianBD.Date <= denNgay.Date)
                .ToListAsync();
        }
    }

}