using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyVatTuPhongMay.Services
{
    public class NguoiDungService : BaseService<NguoiDung>, INguoiDungService
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IValidationService _validationService;

        public NguoiDungService(
            INguoiDungRepository nguoiDungRepository,
            IValidationService validationService) : base(nguoiDungRepository)
        {
            _nguoiDungRepository = nguoiDungRepository;
            _validationService = validationService;
        }

        public async Task<NguoiDung?> DangNhapAsync(string tenDangNhap, string matKhau)
        {
            var matKhauMaHoa = await MaHoaMatKhauAsync(matKhau);
            return await _nguoiDungRepository.DangNhapAsync(tenDangNhap, matKhauMaHoa);
        }

        public async Task<bool> KiemTraTenDangNhapTonTaiAsync(string tenDangNhap)
        {
            return await _nguoiDungRepository.KiemTraTenDangNhapTonTaiAsync(tenDangNhap);
        }

        public async Task<bool> DoiMatKhauAsync(int maNguoiDung, string matKhauCu, string matKhauMoi)
        {
            var nguoiDung = await GetByIdAsync(maNguoiDung);
            if (nguoiDung == null) return false;

            var matKhauCuMaHoa = await MaHoaMatKhauAsync(matKhauCu);
            if (!await KiemTraMatKhauAsync(matKhauCu, nguoiDung.MatKhau))
            {
                return false;
            }

            nguoiDung.MatKhau = await MaHoaMatKhauAsync(matKhauMoi);
            await UpdateAsync(nguoiDung);
            return true;
        }

        public async Task<bool> ValidateNguoiDungDataAsync(NguoiDung nguoiDung)
        {
            var errors = await _validationService.ValidateModelAsync(nguoiDung);

            // Kiểm tra tên đăng nhập đã tồn tại
            if (await KiemTraTenDangNhapTonTaiAsync(nguoiDung.TenDangNhap))
            {
                errors.Add("Tên đăng nhập đã tồn tại");
            }

            return errors.Count == 0;
        }

        public async Task<string> MaHoaMatKhauAsync(string matKhau)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(matKhau));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<bool> KiemTraMatKhauAsync(string matKhau, string matKhauDaMaHoa)
        {
            var matKhauMaHoa = await MaHoaMatKhauAsync(matKhau);
            return matKhauMaHoa == matKhauDaMaHoa;
        }

        public override async Task<NguoiDung> CreateAsync(NguoiDung entity)
        {
            entity.MatKhau = await MaHoaMatKhauAsync(entity.MatKhau);
            return await base.CreateAsync(entity);
        }
    }
} 