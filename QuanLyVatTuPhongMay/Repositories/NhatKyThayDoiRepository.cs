using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using System.Data;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class NhatKyThayDoiRepository : BaseRepository<NhatKyThayDoi>, INhatKyThayDoiRepository
    {
        public NhatKyThayDoiRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<NhatKyThayDoi>> GetAllAsync()
        {
            return await _context.NhatKyThayDois
                .Include(nk => nk.LichSuDangNhap)
                .ThenInclude(ls => ls.NguoiDung)
                .OrderByDescending(nk => nk.LichSuDangNhap.ThoiDiemDN)
                .ToListAsync();
        }

        public async Task<bool> ThemNhatKyAsync(int maLichSu, string noiDung, string? thongTinCu, string? thongTinMoi)
        {
            var maxMaNhatKy = await _context.NhatKyThayDois.MaxAsync(nk => (int?)nk.MaNhatKy) ?? 0;
            var maNhatKyMoi = maxMaNhatKy + 1;

            var parameters = new[]
            {
                new SqlParameter("@MaNhatKy", maNhatKyMoi),
                new SqlParameter("@MaLichSu", maLichSu),
                new SqlParameter("@NoiDung", noiDung ?? (object)DBNull.Value),
                new SqlParameter("@ThongTinCu", thongTinCu ?? (object)DBNull.Value),
                new SqlParameter("@ThongTinMoi", thongTinMoi ?? (object)DBNull.Value)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ThemNhatKyThayDoi @MaNhatKy, @MaLichSu, @NoiDung, @ThongTinCu, @ThongTinMoi", parameters);
            return true;
        }

        public async Task<IEnumerable<NhatKyThayDoi>> GetByLichSuAsync(int maLichSu)
        {
            return await _context.NhatKyThayDois
                .Include(nk => nk.LichSuDangNhap)
                .ThenInclude(ls => ls.NguoiDung)
                .Where(nk => nk.MaLichSu == maLichSu)
                .ToListAsync();
        }

        public async Task<IEnumerable<NhatKyThayDoi>> GetTrongKhoangAsync(DateTime tuNgay, DateTime denNgay)
        {
            var parameters = new[]
            {
                new SqlParameter("@TuNgay", tuNgay),
                new SqlParameter("@DenNgay", denNgay)
            };

            return await _context.NhatKyThayDois
                .FromSqlRaw("EXEC sp_LayNhatKyThayDoi NULL, @TuNgay, @DenNgay", parameters)
                .Include(nk => nk.LichSuDangNhap)
                .ThenInclude(ls => ls.NguoiDung)
                .ToListAsync();
        }
    }
} 