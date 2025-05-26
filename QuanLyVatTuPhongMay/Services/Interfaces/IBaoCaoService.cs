namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface IBaoCaoService
    {
        Task<object> GetBaoCaoTongHopAsync(DateTime? tuNgay = null, DateTime? denNgay = null);
        Task<object> GetThongKeTheoThangAsync(int nam, int? thang = null);
        Task<IEnumerable<object>> GetCanhBaoThietBiAsync();
        Task<object> GetLichSuHoatDongThietBiAsync(int maTTB);
        Task<bool> SaoLuuDuLieuAsync();
        Task<bool> DonDepDuLieuCuAsync(int soNgayGiuLai = 365);
        Task<byte[]> XuatBaoCaoExcelAsync(string loaiBaoCao, object duLieu);
        Task<byte[]> XuatBaoCaoPdfAsync(string loaiBaoCao, object duLieu);
    }
} 