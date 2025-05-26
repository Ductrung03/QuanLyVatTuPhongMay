using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class LopService : BaseService<Lop>, ILopService
    {
        private readonly ILopRepository _lopRepository;
        private readonly IValidationService _validationService;

        public LopService(
            ILopRepository lopRepository,
            IValidationService validationService) : base(lopRepository)
        {
            _lopRepository = lopRepository;
            _validationService = validationService;
        }

        public async Task<bool> KiemTraTenTonTaiAsync(string tenLop)
        {
            return await _lopRepository.KiemTraTenTonTaiAsync(tenLop);
        }

        public async Task<bool> ValidateLopDataAsync(Lop lop)
        {
            var errors = await _validationService.ValidateModelAsync(lop);
            
            if (await KiemTraTenTonTaiAsync(lop.TenLop))
            {
                errors.Add("Tên lớp đã tồn tại");
            }

            return errors.Count == 0;
        }
    }
} 