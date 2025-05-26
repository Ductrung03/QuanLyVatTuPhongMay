using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class LichSuDangNhapService : ILichSuDangNhapService
    {
        private readonly ILichSuDangNhapRepository _lichSuDangNhapRepository;

        public LichSuDangNhapService(ILichSuDangNhapRepository lichSuDangNhapRepository)
        {
            _lichSuDangNhapRepository = lichSuDangNhapRepository;
        }

        public async Task<int> ThemLichSuDangNhapAsync(int maNguoiDung, DateTime thoiDiemDN)
        {
            return await _lichSuDangNhapRepository.ThemLichSuDangNhapAsync(maNguoiDung, thoiDiemDN);
        }

        public async Task<bool> CapNhatDangXuatAsync(int maLichSu, DateTime thoiDiemDX)
        {
            return await _lichSuDangNhapRepository.CapNhatDangXuatAsync(maLichSu, thoiDiemDX);
        }

        public async Task<IEnumerable<LichSuDangNhap>> GetByNguoiDungAsync(int maNguoiDung, DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return await _lichSuDangNhapRepository.GetByNguoiDungAsync(maNguoiDung, tuNgay, denNgay);
        }

        public async Task<IEnumerable<LichSuDangNhap>> GetTatCaLichSuAsync()
        {
            return await _lichSuDangNhapRepository.GetAllAsync();
        }

        public async Task<object> GetThongKeSuDungAsync(DateTime tuNgay, DateTime denNgay)
        {
            var lichSu = await _lichSuDangNhapRepository.GetAllAsync();
            var lichSuTrongKhoang = lichSu.Where(l => l.ThoiDiemDN >= tuNgay && l.ThoiDiemDN <= denNgay);

            return new
            {
                TongSoLanDangNhap = lichSuTrongKhoang.Count(),
                TongThoiGianSuDung = lichSuTrongKhoang.Where(l => l.ThoiDiemDX.HasValue)
                                                    .Sum(l => (l.ThoiDiemDX.Value - l.ThoiDiemDN.Value).TotalMinutes),
                TrungBinhThoiGianSuDung = lichSuTrongKhoang.Where(l => l.ThoiDiemDX.HasValue)
                                                         .Average(l => (l.ThoiDiemDX.Value - l.ThoiDiemDN.Value).TotalMinutes),
                SoNguoiDungHoatDong = lichSuTrongKhoang.Select(l => l.MaNguoiDung).Distinct().Count()
            };
        }
    }
} 