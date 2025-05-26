using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;

namespace QuanLyVatTuPhongMay.Repositories
{
    // NhanVienRepository
    public class LichTrucRepository : BaseRepository<LichTruc>, ILichTrucRepository
    {
        public LichTrucRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<LichTruc>> GetAllAsync()
        {
            return await _context.LichTrucs
                .Include(lt => lt.NhanVien)
                .Include(lt => lt.PhongMay)
                .ToListAsync();
        }

        public override async Task<LichTruc> CreateAsync(LichTruc entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLich", entity.MaLich),
                new SqlParameter("@Ngay", entity.Ngay ?? (object)DBNull.Value),
                new SqlParameter("@MaNV", entity.MaNV),
                new SqlParameter("@MaPhongMay", entity.MaPhongMay)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ThemLichTruc @MaLich, @Ngay, @MaNV, @MaPhongMay", parameters);
            return entity;
        }

        public override async Task<LichTruc> UpdateAsync(LichTruc entity)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLich", entity.MaLich),
                new SqlParameter("@Ngay", entity.Ngay ?? (object)DBNull.Value),
                new SqlParameter("@MaNV", entity.MaNV),
                new SqlParameter("@MaPhongMay", entity.MaPhongMay)
            };
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_SuaLichTruc @MaLich, @Ngay, @MaNV, @MaPhongMay", parameters);
            return entity;
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@MaLich", id);
            await _context.Database.ExecuteSqlRawAsync("EXEC sp_XoaLichTruc @MaLich", parameter);
            return true;
        }

        public async Task<IEnumerable<LichTruc>> GetByNgayAsync(DateTime ngay)
        {
            var parameter = new SqlParameter("@Ngay", ngay.Date);
            return await _context.LichTrucs
                .FromSqlRaw("EXEC sp_LayLichTruc NULL, @Ngay, NULL", parameter)
                .Include(lt => lt.NhanVien)
                .Include(lt => lt.PhongMay)
                .ToListAsync();
        }

        public async Task<IEnumerable<LichTruc>> GetByPhongMayAsync(int maPhongMay)
        {
            var parameter = new SqlParameter("@MaPhongMay", maPhongMay);
            return await _context.LichTrucs
                .FromSqlRaw("EXEC sp_LayLichTruc NULL, NULL, @MaPhongMay", parameter)
                .Include(lt => lt.NhanVien)
                .Include(lt => lt.PhongMay)
                .ToListAsync();
        }

        public async Task<IEnumerable<LichTruc>> GetByNhanVienAsync(int maNV)
        {
            return await _context.LichTrucs
                .Include(lt => lt.NhanVien)
                .Include(lt => lt.PhongMay)
                .Where(lt => lt.MaNV == maNV)
                .ToListAsync();
        }
    }

}