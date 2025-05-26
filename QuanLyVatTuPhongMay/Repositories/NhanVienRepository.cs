using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;

namespace QuanLyVatTuPhongMay.Repositories
{
    // NhanVienRepository
    public class NhanVienRepository : BaseRepository<NhanVien>, INhanVienRepository
    {
        public NhanVienRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<NhanVien> CreateAsync(NhanVien entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaNV", entity.MaNV),
                new SqlParameter("@HoTen", entity.HoTen ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ThemNhanVien @MaNV, @HoTen", parameters);
            return entity;
        }

        public override async Task<NhanVien> UpdateAsync(NhanVien entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaNV", entity.MaNV),
                new SqlParameter("@HoTen", entity.HoTen ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaNhanVien @MaNV, @HoTen", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaNV", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaNhanVien @MaNV", parameter);
            return true;
        }

        public async Task<IEnumerable<NhanVien>> TimKiemTheoTenAsync(string ten)
        {
            return await _context.NhanViens
                .Where(nv => nv.HoTen != null && nv.HoTen.Contains(ten))
                .ToListAsync();
        }
    }
}