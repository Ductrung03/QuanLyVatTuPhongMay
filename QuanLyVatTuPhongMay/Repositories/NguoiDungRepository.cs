using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class NguoiDungRepository : BaseRepository<NguoiDung>, INguoiDungRepository
    {
        public NguoiDungRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<NguoiDung> CreateAsync(NguoiDung entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaNguoiDung", entity.MaNguoiDung),
                new SqlParameter("@TenDangNhap", entity.TenDangNhap),
                new SqlParameter("@MatKhau", entity.MatKhau)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ThemNguoiDung @MaNguoiDung, @TenDangNhap, @MatKhau", parameters);
            return entity;
        }

        public override async Task<NguoiDung> UpdateAsync(NguoiDung entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaNguoiDung", entity.MaNguoiDung),
                new SqlParameter("@TenDangNhap", entity.TenDangNhap),
                new SqlParameter("@MatKhau", entity.MatKhau)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaNguoiDung @MaNguoiDung, @TenDangNhap, @MatKhau", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaNguoiDung", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaNguoiDung @MaNguoiDung", parameter);
            return true;
        }

        public async Task<NguoiDung?> DangNhapAsync(string tenDangNhap, string matKhau)
        {
            var parameters = new[]
            {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhau", matKhau)
            };

            return await _context.NguoiDungs
                .FromSqlRaw("EXEC sp_DangNhap @TenDangNhap, @MatKhau", parameters)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> KiemTraTenDangNhapTonTaiAsync(string tenDangNhap)
        {
            return await _context.NguoiDungs.AnyAsync(u => u.TenDangNhap == tenDangNhap);
        }
    }

}
