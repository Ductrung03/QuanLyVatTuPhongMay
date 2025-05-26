namespace QuanLyVatTuPhongMay.Repositories.Interfaces
{
    public interface IBaoCaoRepository
    {
        Task<object> GetBaoCaoTongHopAsync(DateTime? tuNgay = null, DateTime? denNgay = null);
        Task<object> GetThongKeTheoThangAsync(int nam, int? thang = null);
        Task<IEnumerable<object>> GetCanhBaoThietBiAsync();
        Task<object> GetLichSuHoatDongThietBiAsync(int maTTB);
        Task<bool> SaoLuuDuLieuAsync();
        Task<bool> DonDepDuLieuCuAsync(int soNgayGiuLai = 365);
    }
}
