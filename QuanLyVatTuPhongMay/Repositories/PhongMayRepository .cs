using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using System.Data;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class PhongMayRepository : BaseRepository<PhongMay>, IPhongMayRepository
    {
        public PhongMayRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<PhongMay>> GetAllAsync()
        {
            return await _context.PhongMays
                .Include(p => p.TrangTBs)
                .Include(p => p.LichTrucs)
                .ToListAsync();
        }

        public override async Task<PhongMay> CreateAsync(PhongMay entity)
        {
            var parameter = new SqlParameter("@TenPhongMay", entity.TenPhongMay ?? (object)DBNull.Value);
            var result = await _context.Database.SqlQueryRaw<int>("EXEC sp_ThemPhongMay @TenPhongMay", parameter).FirstOrDefaultAsync();
            entity.MaPhongMay = result;
            return entity;
        }

        public override async Task<PhongMay> UpdateAsync(PhongMay entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaPhongMay", entity.MaPhongMay),
                new SqlParameter("@TenPhongMay", entity.TenPhongMay ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaPhongMay @MaPhongMay, @TenPhongMay", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaPhongMay", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaPhongMay @MaPhongMay", parameter);
            return true;
        }

        public async Task<bool> KiemTraPhongKhaDungAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaPhongMay", maPhongMay),
                new SqlParameter("@ThoiGianBD", thoiGianBD),
                new SqlParameter("@ThoiGianKT", thoiGianKT)
            };

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_KiemTraPhongKhaDung";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.Select(p => (IDbDataParameter)p).ToArray());

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            bool khaDung = false;

            if (await reader.ReadAsync())
            {
                khaDung = reader.GetBoolean("KhaDung");
            }

            await connection.CloseAsync();
            return khaDung;
        }

        public async Task<IEnumerable<object>> GetBaoCaoSuDungAsync(DateTime tuNgay, DateTime denNgay, int? maPhongMay = null)
        {
            var parameters = new[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay),
                new SqlParameter("@MaPhongMay", maPhongMay ?? (object)DBNull.Value)
            };

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_BaoCaoSuDungPhongMay";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.Select(p => (IDbDataParameter)p).ToArray());

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    TenPhongMay = reader.GetString("TenPhongMay"),
                    TongSoLich = reader.GetInt32("TongSoLich"),
                    SoLichHoanThanh = reader.GetInt32("SoLichHoanThanh"),
                    SoLichHuy = reader.GetInt32("SoLichHuy"),
                    TongPhutSuDung = reader.IsDBNull("TongPhutSuDung") ? 0 : reader.GetInt32("TongPhutSuDung"),
                    TrungBinhPhutMoiLich = reader.IsDBNull("TrungBinhPhutMoiLich") ? 0 : reader.GetInt32("TrungBinhPhutMoiLich")
                });
            }

            await connection.CloseAsync();
            return results;
        }
    }


}
