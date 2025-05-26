using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using System.Data;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class LopRepository : BaseRepository<Lop>, ILopRepository
    {
        public LopRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<Lop> CreateAsync(Lop entity)
        {
            var parameter = new SqlParameter("@TenLop", entity.TenLop ?? (object)DBNull.Value);
            var result = await _context.Database.SqlQueryRaw<int>("EXEC sp_ThemLop @TenLop", parameter).FirstOrDefaultAsync();
            entity.MaLop = result;
            return entity;
        }

        public override async Task<Lop> UpdateAsync(Lop entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLop", entity.MaLop),
                new SqlParameter("@TenLop", entity.TenLop ?? (object)DBNull.Value)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaLop @MaLop, @TenLop", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaLop", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaLop @MaLop", parameter);
            return true;
        }

        public async Task<bool> KiemTraTenTonTaiAsync(string tenLop)
        {
            return await _context.Lops.AnyAsync(l => l.TenLop == tenLop);
        }
    }
} 