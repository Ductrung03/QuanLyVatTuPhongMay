namespace QuanLyVatTuPhongMay.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> GuiEmailThongBaoAsync(string emailNhan, string tieuDe, string noiDung);
        Task<bool> GuiEmailCanhBaoThietBiAsync(List<string> danhSachEmail, IEnumerable<object> danhSachCanhBao);
        Task<bool> GuiEmailBaoCaoHangNgayAsync(List<string> danhSachEmail, object baoCao);
    }
} 