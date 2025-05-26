using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class NhanVienService : BaseService<NhanVien>, INhanVienService
    {
        private readonly INhanVienRepository _nhanVienRepository;
        private readonly ILichTrucRepository _lichTrucRepository;
        private readonly IValidationService _validationService;

        public NhanVienService(
            INhanVienRepository nhanVienRepository,
            ILichTrucRepository lichTrucRepository,
            IValidationService validationService) : base(nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
            _lichTrucRepository = lichTrucRepository;
            _validationService = validationService;
        }

        public async Task<IEnumerable<NhanVien>> TimKiemTheoTenAsync(string ten)
        {
            return await _nhanVienRepository.TimKiemTheoTenAsync(ten);
        }

        public async Task<bool> ValidateNhanVienDataAsync(NhanVien nhanVien)
        {
            var errors = await _validationService.ValidateModelAsync(nhanVien);
            return errors.Count == 0;
        }

        public async Task<IEnumerable<LichTruc>> GetLichTrucNhanVienAsync(int maNV)
        {
            return await _lichTrucRepository.GetByNhanVienAsync(maNV);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var lichTruc = await GetLichTrucNhanVienAsync(id);
            if (lichTruc.Any())
            {
                throw new InvalidOperationException("Không thể xóa nhân viên đang có lịch trực");
            }
            return await base.DeleteAsync(id);
        }
    }

}