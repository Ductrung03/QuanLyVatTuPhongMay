using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class PhongMayController : Controller
    {
        private readonly IPhongMayService _phongMayService;
        private readonly ITrangTBService _trangTBService;

        public PhongMayController(IPhongMayService phongMayService, ITrangTBService trangTBService)
        {
            _phongMayService = phongMayService;
            _trangTBService = trangTBService;
        }

        // GET: PhongMay
        public async Task<IActionResult> Index()
        {
            try
            {
                var phongMays = await _phongMayService.GetAllAsync();
                return View(phongMays);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách phòng máy: " + ex.Message;
                return View(new List<PhongMay>());
            }
        }

        // GET: PhongMay/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var phongMay = await _phongMayService.GetByIdAsync(id);
                if (phongMay == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy phòng máy";
                    return RedirectToAction(nameof(Index));
                }

                var thietBiTrongPhong = await _phongMayService.GetThietBiTrongPhongAsync(id);
                ViewBag.ThietBiTrongPhong = thietBiTrongPhong;

                return View(phongMay);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: PhongMay/Create
        public IActionResult Create()
        {
            return View(new PhongMay());
        }

        // POST: PhongMay/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhongMay phongMay)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _phongMayService.CreateAsync(phongMay);
                    TempData["SuccessMessage"] = "Thêm phòng máy thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(phongMay);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm phòng máy: " + ex.Message;
                return View(phongMay);
            }
        }

        // GET: PhongMay/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var phongMay = await _phongMayService.GetByIdAsync(id);
                if (phongMay == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy phòng máy";
                    return RedirectToAction(nameof(Index));
                }

                return View(phongMay);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PhongMay/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PhongMay phongMay)
        {
            if (id != phongMay.MaPhongMay)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _phongMayService.UpdateAsync(phongMay);
                    TempData["SuccessMessage"] = "Cập nhật phòng máy thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(phongMay);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật phòng máy: " + ex.Message;
                return View(phongMay);
            }
        }

        // GET: PhongMay/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var phongMay = await _phongMayService.GetByIdAsync(id);
                if (phongMay == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy phòng máy";
                    return RedirectToAction(nameof(Index));
                }

                var thietBiTrongPhong = await _phongMayService.GetThietBiTrongPhongAsync(id);
                ViewBag.SoThietBiTrongPhong = thietBiTrongPhong.Count();

                return View(phongMay);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: PhongMay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _phongMayService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa phòng máy thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa phòng máy";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa phòng máy: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: PhongMay/BaoCaoSuDung
        public IActionResult BaoCaoSuDung()
        {
            ViewBag.TuNgay = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
            ViewBag.DenNgay = DateTime.Now.ToString("yyyy-MM-dd");
            return View();
        }

        // POST: PhongMay/BaoCaoSuDung
        [HttpPost]
        public async Task<IActionResult> BaoCaoSuDung(DateTime tuNgay, DateTime denNgay, int? maPhongMay)
        {
            try
            {
                var baoCao = await _phongMayService.GetBaoCaoSuDungAsync(tuNgay, denNgay, maPhongMay);

                ViewBag.BaoCao = baoCao;
                ViewBag.TuNgay = tuNgay.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay.ToString("yyyy-MM-dd");
                ViewBag.MaPhongMay = maPhongMay;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo báo cáo: " + ex.Message;
                ViewBag.TuNgay = tuNgay.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay.ToString("yyyy-MM-dd");
                return View();
            }
        }

        // API: Kiểm tra phòng khả dụng
        [HttpPost]
        public async Task<IActionResult> KiemTraKhaDung(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT)
        {
            try
            {
                var khaDung = await _phongMayService.KiemTraPhongKhaDungAsync(maPhongMay, thoiGianBD, thoiGianKT);
                return Json(new { khaDung = khaDung });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // API: Lấy danh sách thiết bị trong phòng
        [HttpGet]
        public async Task<IActionResult> GetThietBiTrongPhong(int id)
        {
            try
            {
                var thietBis = await _phongMayService.GetThietBiTrongPhongAsync(id);
                return Json(thietBis.Select(t => new
                {
                    t.MaTTB,
                    TenLoai = t.Loai?.TenLoai,
                    TenThuongHieu = t.ThuongHieu?.TenThuongHieu,
                    t.TinhTrang,
                    t.GiaTien,
                    NgayNhap = t.NgayNhap.ToString("dd/MM/yyyy"),
                    t.SoLanSua
                }));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}