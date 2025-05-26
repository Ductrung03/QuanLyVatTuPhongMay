using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class LoaiRepository : BaseRepository<Loai>, ILoaiRepository
    {
        public LoaiRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Loai> CreateAsync(Loai entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLoai", entity.MaLoai),
                new SqlParameter("@TenLoai", entity.TenLoai)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ThemLoai @MaLoai, @TenLoai", parameters);
            return entity;
        }

        public override async Task<Loai> UpdateAsync(Loai entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLoai", entity.MaLoai),
                new SqlParameter("@TenLoai", entity.TenLoai)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaLoai @MaLoai, @TenLoai", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaLoai", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaLoai @MaLoai", parameter);
            return true;
        }

        public async Task<bool> KiemTraTenTonTaiAsync(string tenLoai)
        {
            return await _context.Loais.AnyAsync(l => l.TenLoai == tenLoai);
        }
    }

}