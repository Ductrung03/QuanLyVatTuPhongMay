using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class NguoiDungController : Controller
    {
        private readonly INguoiDungService _nguoiDungService;
        private readonly ILichSuDangNhapService _lichSuDangNhapService;

        public NguoiDungController(INguoiDungService nguoiDungService, ILichSuDangNhapService lichSuDangNhapService)
        {
            _nguoiDungService = nguoiDungService;
            _lichSuDangNhapService = lichSuDangNhapService;
        }

        // GET: NguoiDung
        public async Task<IActionResult> Index()
        {
            try
            {
                var nguoiDungs = await _nguoiDungService.GetAllAsync();
                return View(nguoiDungs);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách người dùng: " + ex.Message;
                return View(new List<NguoiDung>());
            }
        }

        // GET: NguoiDung/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var nguoiDung = await _nguoiDungService.GetByIdAsync(id);
                if (nguoiDung == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy người dùng";
                    return RedirectToAction(nameof(Index));
                }

                return View(nguoiDung);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NguoiDung/Create
        public IActionResult Create()
        {
            return View(new NguoiDung());
        }

        // POST: NguoiDung/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NguoiDung nguoiDung)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra tên đăng nhập đã tồn tại
                    if (await _nguoiDungService.KiemTraTenDangNhapTonTaiAsync(nguoiDung.TenDangNhap))
                    {
                        ModelState.AddModelError("TenDangNhap", "Tên đăng nhập đã tồn tại");
                        return View(nguoiDung);
                    }

                    await _nguoiDungService.CreateAsync(nguoiDung);
                    TempData["SuccessMessage"] = "Thêm người dùng thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(nguoiDung);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm người dùng: " + ex.Message;
                return View(nguoiDung);
            }
        }

        // GET: NguoiDung/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var nguoiDung = await _nguoiDungService.GetByIdAsync(id);
                if (nguoiDung == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy người dùng";
                    return RedirectToAction(nameof(Index));
                }

                // Không hiển thị mật khẩu khi edit
                nguoiDung.MatKhau = "";
                return View(nguoiDung);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NguoiDung/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NguoiDung nguoiDung, string? matKhauMoi)
        {
            if (id != nguoiDung.MaNguoiDung)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var nguoiDungHienTai = await _nguoiDungService.GetByIdAsync(id);
                    if (nguoiDungHienTai == null)
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy người dùng";
                        return RedirectToAction(nameof(Index));
                    }

                    // Cập nhật thông tin
                    nguoiDungHienTai.TenDangNhap = nguoiDung.TenDangNhap;

                    // Chỉ cập nhật mật khẩu nếu có nhập mật khẩu mới
                    if (!string.IsNullOrEmpty(matKhauMoi))
                    {
                        nguoiDungHienTai.MatKhau = await _nguoiDungService.MaHoaMatKhauAsync(matKhauMoi);
                    }

                    await _nguoiDungService.UpdateAsync(nguoiDungHienTai);
                    TempData["SuccessMessage"] = "Cập nhật người dùng thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(nguoiDung);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật người dùng: " + ex.Message;
                return View(nguoiDung);
            }
        }

        // GET: NguoiDung/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var nguoiDung = await _nguoiDungService.GetByIdAsync(id);
                if (nguoiDung == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy người dùng";
                    return RedirectToAction(nameof(Index));
                }

                return View(nguoiDung);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NguoiDung/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _nguoiDungService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa người dùng thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa người dùng";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa người dùng: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: NguoiDung/DangNhap
        public IActionResult DangNhap()
        {
            return View();
        }

        // POST: NguoiDung/DangNhap
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DangNhap(string tenDangNhap, string matKhau)
        {
            try
            {
                if (string.IsNullOrEmpty(tenDangNhap) || string.IsNullOrEmpty(matKhau))
                {
                    TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin đăng nhập";
                    return View();
                }

                var nguoiDung = await _nguoiDungService.DangNhapAsync(tenDangNhap, matKhau);
                if (nguoiDung != null)
                {
                    // Lưu thông tin đăng nhập vào session
                    HttpContext.Session.SetInt32("MaNguoiDung", nguoiDung.MaNguoiDung);
                    HttpContext.Session.SetString("TenDangNhap", nguoiDung.TenDangNhap);

                    // Ghi lịch sử đăng nhập
                    await _lichSuDangNhapService.ThemLichSuDangNhapAsync(nguoiDung.MaNguoiDung, DateTime.Now);

                    TempData["SuccessMessage"] = "Đăng nhập thành công";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi đăng nhập: " + ex.Message;
                return View();
            }
        }

        // POST: NguoiDung/DangXuat
        [HttpPost]
        public async Task<IActionResult> DangXuat()
        {
            try
            {
                var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
                if (maNguoiDung.HasValue)
                {
                    // Cập nhật thời điểm đăng xuất (cần cải thiện để lấy đúng MaLichSu)
                    // Tạm thời comment vì cần implement đúng logic
                    // await _lichSuDangNhapService.CapNhatDangXuatAsync(maLichSu, DateTime.Now);
                }

                HttpContext.Session.Clear();
                TempData["SuccessMessage"] = "Đăng xuất thành công";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi đăng xuất: " + ex.Message;
            }

            return RedirectToAction(nameof(DangNhap));
        }

        // GET: NguoiDung/DoiMatKhau
        public IActionResult DoiMatKhau()
        {
            return View();
        }

        // POST: NguoiDung/DoiMatKhau
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DoiMatKhau(string matKhauCu, string matKhauMoi, string xacNhanMatKhau)
        {
            try
            {
                var maNguoiDung = HttpContext.Session.GetInt32("MaNguoiDung");
                if (!maNguoiDung.HasValue)
                {
                    TempData["ErrorMessage"] = "Vui lòng đăng nhập";
                    return RedirectToAction(nameof(DangNhap));
                }

                if (string.IsNullOrEmpty(matKhauCu) || string.IsNullOrEmpty(matKhauMoi) || string.IsNullOrEmpty(xacNhanMatKhau))
                {
                    TempData["ErrorMessage"] = "Vui lòng nhập đầy đủ thông tin";
                    return View();
                }

                if (matKhauMoi != xacNhanMatKhau)
                {
                    TempData["ErrorMessage"] = "Mật khẩu mới và xác nhận không khớp";
                    return View();
                }

                var result = await _nguoiDungService.DoiMatKhauAsync(maNguoiDung.Value, matKhauCu, matKhauMoi);
                if (result)
                {
                    TempData["SuccessMessage"] = "Đổi mật khẩu thành công";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Mật khẩu cũ không đúng";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi đổi mật khẩu: " + ex.Message;
                return View();
            }
        }

        // GET: NguoiDung/LichSuDangNhap
        public async Task<IActionResult> LichSuDangNhap(int? maNguoiDung, DateTime? tuNgay, DateTime? denNgay)
        {
            try
            {
                IEnumerable<LichSuDangNhap> lichSu;

                if (maNguoiDung.HasValue)
                {
                    lichSu = await _lichSuDangNhapService.GetByNguoiDungAsync(maNguoiDung.Value, tuNgay, denNgay);
                }
                else
                {
                    lichSu = await _lichSuDangNhapService.GetTatCaLichSuAsync();
                    if (tuNgay.HasValue && denNgay.HasValue)
                    {
                        lichSu = lichSu.Where(ls => ls.ThoiDiemDN >= tuNgay && ls.ThoiDiemDN <= denNgay);
                    }
                }

                var danhSachNguoiDung = await _nguoiDungService.GetAllAsync();
                ViewBag.DanhSachNguoiDung = danhSachNguoiDung;
                ViewBag.MaNguoiDung = maNguoiDung;
                ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

                return View(lichSu.OrderByDescending(ls => ls.ThoiDiemDN));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<LichSuDangNhap>());
            }
        }

        // API: Kiểm tra tên đăng nhập
        [HttpGet]
        public async Task<IActionResult> KiemTraTenDangNhap(string tenDangNhap)
        {
            try
            {
                var tonTai = await _nguoiDungService.KiemTraTenDangNhapTonTaiAsync(tenDangNhap);
                return Json(new { tonTai = tonTai });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}