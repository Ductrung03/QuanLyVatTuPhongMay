using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class ThuongHieuRepository : BaseRepository<ThuongHieu>, IThuongHieuRepository
    {
        public ThuongHieuRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<ThuongHieu> CreateAsync(ThuongHieu entity)
        {
            var parameter = new SqlParameter("@TenThuongHieu", entity.TenThuongHieu);
            var result = await _context.Database.SqlQueryRaw<int>("EXEC sp_ThemThuongHieu @TenThuongHieu", parameter).FirstOrDefaultAsync();
            entity.MaThuongHieu = result;
            return entity;
        }

        public override async Task<ThuongHieu> UpdateAsync(ThuongHieu entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaThuongHieu", entity.MaThuongHieu),
                new SqlParameter("@TenThuongHieu", entity.TenThuongHieu)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaThuongHieu @MaThuongHieu, @TenThuongHieu", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaThuongHieu", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaThuongHieu @MaThuongHieu", parameter);
            return true;
        }

        public async Task<bool> KiemTraTenTonTaiAsync(string tenThuongHieu)
        {
            return await _context.ThuongHieus.AnyAsync(t => t.TenThuongHieu == tenThuongHieu);
        }

    }
}