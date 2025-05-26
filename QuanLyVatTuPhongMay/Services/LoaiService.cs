using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class LoaiService : BaseService<Loai>, ILoaiService
    {
        private readonly ILoaiRepository _loaiRepository;
        private readonly ITrangTBRepository _trangTBRepository;
        private readonly IValidationService _validationService;

        public LoaiService(
            ILoaiRepository loaiRepository,
            ITrangTBRepository trangTBRepository,
            IValidationService validationService) : base(loaiRepository)
        {
            _loaiRepository = loaiRepository;
            _trangTBRepository = trangTBRepository;
            _validationService = validationService;
        }

        public async Task<bool> KiemTraTenTonTaiAsync(string tenLoai)
        {
            return await _loaiRepository.KiemTraTenTonTaiAsync(tenLoai);
        }

        public async Task<bool> ValidateLoaiDataAsync(Loai loai)
        {
            var errors = await _validationService.ValidateModelAsync(loai);

            if (await KiemTraTenTonTaiAsync(loai.TenLoai))
            {
                errors.Add("Tên loại đã tồn tại");
            }

            return errors.Count == 0;
        }

        public async Task<IEnumerable<TrangTB>> GetThietBiTheoLoaiAsync(int maLoai)
        {
            return await _trangTBRepository.GetByLoaiAsync(maLoai);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var thietBi = await GetThietBiTheoLoaiAsync(id);
            if (thietBi.Any())
            {
                throw new InvalidOperationException("Không thể xóa loại thiết bị còn có thiết bị");
            }
            return await base.DeleteAsync(id);
        }
    }
}