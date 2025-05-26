using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class LichThucHanhService : BaseService<LichThucHanh>, ILichThucHanhService
    {
        private readonly ILichThucHanhRepository _lichThucHanhRepository;
        private readonly IPhongMayRepository _phongMayRepository;
        private readonly IValidationService _validationService;

        public LichThucHanhService(
            ILichThucHanhRepository lichThucHanhRepository,
            IPhongMayRepository phongMayRepository,
            IValidationService validationService) : base(lichThucHanhRepository)
        {
            _lichThucHanhRepository = lichThucHanhRepository;
            _phongMayRepository = phongMayRepository;
            _validationService = validationService;
        }

        public async Task<IEnumerable<LichThucHanh>> GetByNguoiDungAsync(int maNguoiDung)
        {
            return await _lichThucHanhRepository.GetByNguoiDungAsync(maNguoiDung);
        }

        public async Task<IEnumerable<LichThucHanh>> GetByTrangThaiAsync(string trangThai)
        {
            return await _lichThucHanhRepository.GetByTrangThaiAsync(trangThai);
        }

        public async Task<bool> DatLichAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT, int maNguoiDung)
        {
            // Kiểm tra phòng có khả dụng không
            var khaDung = await _phongMayRepository.KiemTraPhongKhaDungAsync(maPhongMay, thoiGianBD, thoiGianKT);
            if (!khaDung) return false;

            return await _lichThucHanhRepository.DatLichAsync(maPhongMay, thoiGianBD, thoiGianKT, maNguoiDung);
        }

        public async Task<bool> HuyLichAsync(int maLich, int maNguoiDung)
        {
            return await _lichThucHanhRepository.HuyLichAsync(maLich, maNguoiDung);
        }

        public async Task<bool> CapNhatTrangThaiAsync(int maLich, string trangThai)
        {
            var lich = await GetByIdAsync(maLich);
            if (lich == null) return false;

            lich.TrangThai = trangThai;
            await UpdateAsync(lich);
            return true;
        }

        public async Task<IEnumerable<LichThucHanh>> GetLichTrongKhoangAsync(DateTime tuNgay, DateTime denNgay)
        {
            return await _lichThucHanhRepository.GetLichTrongKhoangAsync(tuNgay, denNgay);
        }

        public async Task<bool> ValidateLichThucHanhAsync(LichThucHanh lichThucHanh)
        {
            var errors = await _validationService.ValidateModelAsync(lichThucHanh);

            // Validation bổ sung
            if (lichThucHanh.ThoiGianBD >= lichThucHanh.ThoiGianKT)
            {
                errors.Add("Thời gian bắt đầu phải nhỏ hơn thời gian kết thúc");
            }

            if (lichThucHanh.ThoiGianBD < DateTime.Now)
            {
                errors.Add("Thời gian bắt đầu không được nhỏ hơn thời gian hiện tại");
            }

            // Kiểm tra trung lịch
            if (await KiemTraTrungLichAsync(lichThucHanh.ThoiGianBD, lichThucHanh.ThoiGianKT, lichThucHanh.MaLich))
            {
                errors.Add("Lịch thực hành bị trung với lịch khác");
            }

            return errors.Count == 0;
        }

        public async Task<IEnumerable<LichThucHanh>> GetLichSapToiAsync(int maNguoiDung)
        {
            var lichCuaNguoiDung = await GetByNguoiDungAsync(maNguoiDung);
            return lichCuaNguoiDung.Where(l => l.ThoiGianBD >= DateTime.Now && l.TrangThai != "Đã hủy")
                                   .OrderBy(l => l.ThoiGianBD)
                                   .Take(5);
        }

        public async Task<bool> KiemTraTrungLichAsync(DateTime thoiGianBD, DateTime thoiGianKT, int? maLichBỏQua = null)
        {
            var tatCaLich = await GetAllAsync();
            return tatCaLich.Any(l => l.MaLich != maLichBỏQua &&
                                l.TrangThai != "Đã hủy" &&
                                ((thoiGianBD >= l.ThoiGianBD && thoiGianBD < l.ThoiGianKT) ||
                                 (thoiGianKT > l.ThoiGianBD && thoiGianKT <= l.ThoiGianKT) ||
                                 (thoiGianBD <= l.ThoiGianBD && thoiGianKT >= l.ThoiGianKT)));
        }

        public override async Task<LichThucHanh> CreateAsync(LichThucHanh entity)
        {
            if (!await ValidateLichThucHanhAsync(entity))
            {
                throw new ArgumentException("Dữ liệu lịch thực hành không hợp lệ");
            }

            if (string.IsNullOrEmpty(entity.TrangThai))
            {
                entity.TrangThai = "Đã đặt";
            }

            return await base.CreateAsync(entity);
        }
    }

}