using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class ThuongHieuService : BaseService<ThuongHieu>, IThuongHieuService
    {
        private readonly IThuongHieuRepository _thuongHieuRepository;
        private readonly ITrangTBRepository _trangTBRepository;
        private readonly IValidationService _validationService;

        public ThuongHieuService(
            IThuongHieuRepository thuongHieuRepository,
            ITrangTBRepository trangTBRepository,
            IValidationService validationService) : base(thuongHieuRepository)
        {
            _thuongHieuRepository = thuongHieuRepository;
            _trangTBRepository = trangTBRepository;
            _validationService = validationService;
        }

        public async Task<bool> KiemTraTenTonTaiAsync(string tenThuongHieu)
        {
            return await _thuongHieuRepository.KiemTraTenTonTaiAsync(tenThuongHieu);
        }

        public async Task<bool> ValidateThuongHieuDataAsync(ThuongHieu thuongHieu)
        {
            var errors = await _validationService.ValidateModelAsync(thuongHieu);
            
            if (await KiemTraTenTonTaiAsync(thuongHieu.TenThuongHieu))
            {
                errors.Add("Tên thương hiệu đã tồn tại");
            }

            return errors.Count == 0;
        }

        public async Task<IEnumerable<TrangTB>> GetThietBiTheoThuongHieuAsync(int maThuongHieu)
        {
            return await _trangTBRepository.GetByThuongHieuAsync(maThuongHieu);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var thietBi = await GetThietBiTheoThuongHieuAsync(id);
            if (thietBi.Any())
            {
                throw new InvalidOperationException("Không thể xóa thương hiệu còn có thiết bị");
            }
            return await base.DeleteAsync(id);
        }
    }
} 