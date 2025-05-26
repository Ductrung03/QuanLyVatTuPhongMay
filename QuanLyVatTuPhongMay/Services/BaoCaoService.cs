using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class BaoCaoService : IBaoCaoService
    {
        private readonly IBaoCaoRepository _baoCaoRepository;

        public BaoCaoService(IBaoCaoRepository baoCaoRepository)
        {
            _baoCaoRepository = baoCaoRepository;
        }

        public async Task<object> GetBaoCaoTongHopAsync(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            return await _baoCaoRepository.GetBaoCaoTongHopAsync(tuNgay, denNgay);
        }

        public async Task<object> GetThongKeTheoThangAsync(int nam, int? thang = null)
        {
            return await _baoCaoRepository.GetThongKeTheoThangAsync(nam, thang);
        }

        public async Task<IEnumerable<object>> GetCanhBaoThietBiAsync()
        {
            return await _baoCaoRepository.GetCanhBaoThietBiAsync();
        }

        public async Task<object> GetLichSuHoatDongThietBiAsync(int maTTB)
        {
            return await _baoCaoRepository.GetLichSuHoatDongThietBiAsync(maTTB);
        }

        public async Task<bool> SaoLuuDuLieuAsync()
        {
            return await _baoCaoRepository.SaoLuuDuLieuAsync();
        }

        public async Task<bool> DonDepDuLieuCuAsync(int soNgayGiuLai = 365)
        {
            return await _baoCaoRepository.DonDepDuLieuCuAsync(soNgayGiuLai);
        }

        public async Task<byte[]> XuatBaoCaoExcelAsync(string loaiBaoCao, object duLieu)
        {
            // Implement Excel export logic here
            // Có thể sử dụng thư viện như EPPlus hoặc ClosedXML
            throw new NotImplementedException("Chức năng xuất Excel sẽ được triển khai sau");
        }

        public async Task<byte[]> XuatBaoCaoPdfAsync(string loaiBaoCao, object duLieu)
        {
            // Implement PDF export logic here
            // Có thể sử dụng thư viện như iTextSharp hoặc DinkToPdf
            throw new NotImplementedException("Chức năng xuất PDF sẽ được triển khai sau");
        }
    }
} 