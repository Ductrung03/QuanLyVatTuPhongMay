using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class TrangTBService : BaseService<TrangTB>, ITrangTBService
    {
        private readonly ITrangTBRepository _trangTBRepository;
        private readonly INhatKyThayDoiService _nhatKyService;
        private readonly IValidationService _validationService;

        public TrangTBService(
            ITrangTBRepository trangTBRepository,
            INhatKyThayDoiService nhatKyService,
            IValidationService validationService) : base(trangTBRepository)
        {
            _trangTBRepository = trangTBRepository;
            _nhatKyService = nhatKyService;
            _validationService = validationService;
        }

        public async Task<IEnumerable<TrangTB>> GetByPhongMayAsync(int maPhongMay)
        {
            return await _trangTBRepository.GetByPhongMayAsync(maPhongMay);
        }

        public async Task<IEnumerable<TrangTB>> GetByLoaiAsync(int maLoai)
        {
            return await _trangTBRepository.GetByLoaiAsync(maLoai);
        }

        public async Task<IEnumerable<TrangTB>> GetByThuongHieuAsync(int maThuongHieu)
        {
            return await _trangTBRepository.GetByThuongHieuAsync(maThuongHieu);
        }

        public async Task<IEnumerable<TrangTB>> GetByTinhTrangAsync(string tinhTrang)
        {
            return await _trangTBRepository.GetByTinhTrangAsync(tinhTrang);
        }

        public async Task<IEnumerable<TrangTB>> GetCanBaoTriAsync()
        {
            return await _trangTBRepository.GetCanBaoTriAsync();
        }

        public async Task<bool> CapNhatSoLanSuaAsync(int maTTB)
        {
            var thietBi = await _trangTBRepository.GetByIdAsync(maTTB);
            if (thietBi == null) return false;

            return await _trangTBRepository.CapNhatSoLanSuaAsync(maTTB);
        }

        public async Task<bool> ChuyenTrangThaiThietBiAsync(int maTTB, string tinhTrangMoi, int maNguoiDung, string? ghiChu = null)
        {
            var thietBi = await _trangTBRepository.GetByIdAsync(maTTB);
            if (thietBi == null) return false;

            var tinhTrangCu = thietBi.TinhTrang;
            thietBi.TinhTrang = tinhTrangMoi;

            // Nếu chuyển sang trạng thái sửa chữa, tăng số lần sửa
            if (tinhTrangMoi.Contains("sửa") || tinhTrangMoi.Contains("bảo trì"))
            {
                await CapNhatSoLanSuaAsync(maTTB);
            }

            await _trangTBRepository.UpdateAsync(thietBi);

            // Ghi nhật ký thay đổi
            var noiDung = $"Thay đổi tình trạng thiết bị ID: {maTTB}";
            if (!string.IsNullOrEmpty(ghiChu))
            {
                noiDung += $". Ghi chú: {ghiChu}";
            }

            // Tạo lịch sử đăng nhập tạm để ghi nhật ký (cần cải thiện)
            await _nhatKyService.ThemNhatKyAsync(0, noiDung, tinhTrangCu, tinhTrangMoi);

            return true;
        }

        public async Task<IEnumerable<TrangTB>> TimKiemNangCaoAsync(string? tuKhoa, int? maPhongMay, int? maLoai,
            int? maThuongHieu, string? tinhTrang, DateTime? tuNgay, DateTime? denNgay,
            int? giaMin, int? giaMax)
        {
            return await _trangTBRepository.TimKiemAsync(tuKhoa, maPhongMay, maLoai, maThuongHieu,
                tinhTrang, tuNgay, denNgay, giaMin, giaMax);
        }

        public async Task<object> GetBaoCaoTheoPhongAsync()
        {
            return await _trangTBRepository.GetBaoCaoTheoPhongAsync();
        }

        public async Task<object> GetThongKeTheoThuongHieuAsync()
        {
            return await _trangTBRepository.GetThongKeTheoThuongHieuAsync();
        }

        public async Task<object> GetLichSuHoatDongAsync(int maTTB)
        {
            var thietBi = await _trangTBRepository.GetByIdAsync(maTTB);
            if (thietBi == null) return null;

            return new
            {
                ThietBi = thietBi,
                SoNgayDaSuDung = (DateTime.Now - thietBi.NgayNhap).Days,
                TrangThaiHoatDong = thietBi.SoLanSua >= 5 ? "Cần bảo trì" :
                                  thietBi.TinhTrang?.Contains("hỏng") == true ? "Hỏng" : "Bình thường",
                CanhBao = thietBi.SoLanSua >= 5 ? "Thiết bị đã sửa nhiều lần, cần kiểm tra tổng thể" : null
            };
        }

        public async Task<IEnumerable<object>> GetCanhBaoThietBiAsync()
        {
            var thietBiCanBaoTri = await GetCanBaoTriAsync();
            var canhBaos = new List<object>();

            foreach (var thietBi in thietBiCanBaoTri)
            {
                var loaiCanhBao = "CANH_BAO_SUA_CHUA";
                var noiDung = $"Thiết bị đã sửa {thietBi.SoLanSua} lần";

                if (thietBi.TinhTrang?.Contains("hỏng") == true)
                {
                    loaiCanhBao = "CANH_BAO_HONG";
                    noiDung = $"Thiết bị đang ở trạng thái: {thietBi.TinhTrang}";
                }

                var soNgaySuDung = (DateTime.Now - thietBi.NgayNhap).Days;
                if (soNgaySuDung > 365 * 5) // Trên 5 năm
                {
                    loaiCanhBao = "CANH_BAO_CU";
                    noiDung = $"Thiết bị đã sử dụng {soNgaySuDung} ngày";
                }

                canhBaos.Add(new
                {
                    LoaiCanhBao = loaiCanhBao,
                    MaTTB = thietBi.MaTTB,
                    TenPhongMay = thietBi.PhongMay?.TenPhongMay,
                    TenLoai = thietBi.Loai?.TenLoai,
                    TenThuongHieu = thietBi.ThuongHieu?.TenThuongHieu,
                    TinhTrang = thietBi.TinhTrang,
                    SoLanSua = thietBi.SoLanSua,
                    NgayNhap = thietBi.NgayNhap,
                    NoiDungCanhBao = noiDung
                });
            }

            return canhBaos;
        }

        public async Task<bool> ValidateThietBiDataAsync(TrangTB thietBi)
        {
            var errors = await _validationService.ValidateModelAsync(thietBi);

            // Validation bổ sung
            if (thietBi.NgayNhap > DateTime.Now)
            {
                errors.Add("Ngày nhập không được lớn hơn ngày hiện tại");
            }

            if (thietBi.GiaTien <= 0)
            {
                errors.Add("Giá tiền phải lớn hơn 0");
            }

            if (thietBi.SoLanSua < 0)
            {
                errors.Add("Số lần sửa không được âm");
            }

            return errors.Count == 0;
        }

        public override async Task<TrangTB> CreateAsync(TrangTB entity)
        {
            if (!await ValidateThietBiDataAsync(entity))
            {
                throw new ArgumentException("Dữ liệu thiết bị không hợp lệ");
            }

            // Thiết lập giá trị mặc định
            if (entity.SoLanSua == null) entity.SoLanSua = 0;
            if (string.IsNullOrEmpty(entity.TinhTrang)) entity.TinhTrang = "Mới";

            return await base.CreateAsync(entity);
        }

        public override async Task<TrangTB> UpdateAsync(TrangTB entity)
        {
            if (!await ValidateThietBiDataAsync(entity))
            {
                throw new ArgumentException("Dữ liệu thiết bị không hợp lệ");
            }

            return await base.UpdateAsync(entity);
        }

        public override async Task<bool> DeleteAsync(int id)
        {
            var thietBi = await GetByIdAsync(id);
            if (thietBi == null) return false;

            // Kiểm tra xem thiết bị có đang được sử dụng không
            if (thietBi.MaLich != null)
            {
                throw new InvalidOperationException("Không thể xóa thiết bị đang được sử dụng trong lịch thực hành");
            }

            return await base.DeleteAsync(id);
        }
    }
}