using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class LichSuDangNhapRepository : BaseRepository<LichSuDangNhap>, ILichSuDangNhapRepository
    {
        public LichSuDangNhapRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<IEnumerable<LichSuDangNhap>> GetAllAsync()
        {
            return await _context.LichSuDangNhaps
                .Include(ls => ls.NguoiDung)
                .OrderByDescending(ls => ls.ThoiDiemDN)
                .ToListAsync();
        }

        public async Task<int> ThemLichSuDangNhapAsync(int maNguoiDung, DateTime thoiDiemDN)
        {
            // Tạo mã lịch sử mới
            var maxMaLichSu = await _context.LichSuDangNhaps.MaxAsync(ls => (int?)ls.MaLichSu) ?? 0;
            var maLichSuMoi = maxMaLichSu + 1;

            var parameters = new[]
            {
                new SqlParameter("@MaLichSu", maLichSuMoi),
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@ThoiDiemDN", thoiDiemDN)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_ThemLichSuDangNhap @MaLichSu, @MaNguoiDung, @ThoiDiemDN", parameters);
            return maLichSuMoi;
        }

        public async Task<bool> CapNhatDangXuatAsync(int maLichSu, DateTime thoiDiemDX)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaLichSu", maLichSu),
                new SqlParameter("@ThoiDiemDX", thoiDiemDX)
            };

            await _context.Database.ExecuteSqlRawAsync("EXEC sp_CapNhatDangXuat @MaLichSu, @ThoiDiemDX", parameters);
            return true;
        }

        public async Task<IEnumerable<LichSuDangNhap>> GetByNguoiDungAsync(int maNguoiDung, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var parameters = new[]
            {
                new SqlParameter("@MaNguoiDung", maNguoiDung),
                new SqlParameter("@TuNgay", tuNgay ?? (object)DBNull.Value),
                new SqlParameter("@DenNgay", denNgay ?? (object)DBNull.Value)
            };

            return await _context.LichSuDangNhaps
                .FromSqlRaw("EXEC sp_LayLichSuDangNhap @MaNguoiDung, @TuNgay, @DenNgay", parameters)
                .Include(ls => ls.NguoiDung)
                .ToListAsync();
        }
    }


}