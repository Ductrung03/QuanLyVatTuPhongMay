using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class LichTrucService : BaseService<LichTruc>, ILichTrucService
    {
        private readonly ILichTrucRepository _lichTrucRepository;
        private readonly IValidationService _validationService;

        public LichTrucService(
            ILichTrucRepository lichTrucRepository,
            IValidationService validationService) : base(lichTrucRepository)
        {
            _lichTrucRepository = lichTrucRepository;
            _validationService = validationService;
        }

        public async Task<IEnumerable<LichTruc>> GetByNgayAsync(DateTime ngay)
        {
            return await _lichTrucRepository.GetByNgayAsync(ngay);
        }

        public async Task<IEnumerable<LichTruc>> GetByPhongMayAsync(int maPhongMay)
        {
            return await _lichTrucRepository.GetByPhongMayAsync(maPhongMay);
        }

        public async Task<IEnumerable<LichTruc>> GetByNhanVienAsync(int maNV)
        {
            return await _lichTrucRepository.GetByNhanVienAsync(maNV);
        }

        public async Task<bool> ValidateLichTrucDataAsync(LichTruc lichTruc)
        {
            var errors = await _validationService.ValidateModelAsync(lichTruc);

            if (await KiemTraTrungLichTrucAsync(lichTruc.MaNV, lichTruc.Ngay ?? DateTime.Now, lichTruc.MaLich))
            {
                errors.Add("Nhân viên đã có lịch trực trong ngày này");
            }

            return errors.Count == 0;
        }

        public async Task<bool> KiemTraTrungLichTrucAsync(int maNV, DateTime ngay, int? maLichBỏQua = null)
        {
            var lichTrongNgay = await GetByNgayAsync(ngay);
            return lichTrongNgay.Any(l => l.MaNV == maNV && l.MaLich != maLichBỏQua);
        }
    }
} 