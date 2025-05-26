using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyVatTuPhongMay.Services
{
    // PhongMayService
    public class PhongMayService : BaseService<PhongMay>, IPhongMayService
    {
        private readonly IPhongMayRepository _phongMayRepository;
        private readonly ITrangTBRepository _trangTBRepository;
        private readonly IValidationService _validationService;

        public PhongMayService(
            IPhongMayRepository phongMayRepository,
            ITrangTBRepository trangTBRepository,
            IValidationService validationService) : base(phongMayRepository)
        {
            _phongMayRepository = phongMayRepository;
            _trangTBRepository = trangTBRepository;
            _validationService = validationService;
        }

        public async Task<bool> KiemTraPhongKhaDungAsync(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT)
        {
            return await _phongMayRepository.KiemTraPhongKhaDungAsync(maPhongMay, thoiGianBD, thoiGianKT);
        }

        public async Task<IEnumerable<object>> GetBaoCaoSuDungAsync(DateTime tuNgay, DateTime denNgay, int? maPhongMay = null)
        {
            return await _phongMayRepository.GetBaoCaoSuDungAsync(tuNgay, denNgay, maPhongMay);
        }

        public async Task<bool> ValidatePhongMayDataAsync(PhongMay phongMay)
        {
            var errors = await _validationService.ValidateModelAsync(phongMay);
            return errors.Count == 0;
        }

        public async Task<IEnumerable<TrangTB>> GetThietBiTrongPhongAsync(int maPhongMay)
        {
            return await _trangTBRepository.GetByPhongMayAsync(maPhongMay);
        }

        public override async Task<PhongMay> CreateAsync(PhongMay entity)
        {
            if (!await ValidatePhongMayDataAsync(entity))
            {
                throw new ArgumentException("Dữ liệu phòng máy không hợp lệ");
            }
            return await base.CreateAsync(entity);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var thietBiTrongPhong = await GetThietBiTrongPhongAsync(id);
            if (thietBiTrongPhong.Any())
            {
                throw new InvalidOperationException("Không thể xóa phòng máy còn có thiết bị");
            }
            return await base.DeleteAsync(id);
        }
    }
}