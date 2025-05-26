using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class LichTrucController : Controller
    {
        private readonly ILichTrucService _lichTrucService;
        private readonly INhanVienService _nhanVienService;
        private readonly IPhongMayService _phongMayService;

        public LichTrucController(
            ILichTrucService lichTrucService,
            INhanVienService nhanVienService,
            IPhongMayService phongMayService)
        {
            _lichTrucService = lichTrucService;
            _nhanVienService = nhanVienService;
            _phongMayService = phongMayService;
        }

        // GET: LichTruc
        public async Task<IActionResult> Index(DateTime? ngay, int? maNV, int? maPhongMay)
        {
            try
            {
                IEnumerable<LichTruc> lichTrucs;

                if (ngay.HasValue)
                {
                    lichTrucs = await _lichTrucService.GetByNgayAsync(ngay.Value);
                }
                else if (maNV.HasValue)
                {
                    lichTrucs = await _lichTrucService.GetByNhanVienAsync(maNV.Value);
                }
                else if (maPhongMay.HasValue)
                {
                    lichTrucs = await _lichTrucService.GetByPhongMayAsync(maPhongMay.Value);
                }
                else
                {
                    lichTrucs = await _lichTrucService.GetAllAsync();
                }

                await LoadSelectListsAsync();

                ViewBag.Ngay = ngay?.ToString("yyyy-MM-dd");
                ViewBag.MaNV = maNV;
                ViewBag.MaPhongMay = maPhongMay;

                return View(lichTrucs.OrderByDescending(l => l.Ngay));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách lịch trực: " + ex.Message;
                return View(new List<LichTruc>());
            }
        }

        // GET: LichTruc/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var lichTruc = await _lichTrucService.GetByIdAsync(id);
                if (lichTruc == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch trực";
                    return RedirectToAction(nameof(Index));
                }

                return View(lichTruc);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LichTruc/Create
        public async Task<IActionResult> Create()
        {
            await LoadSelectListsAsync();
            var lichTruc = new LichTruc
            {
                Ngay = DateTime.Today
            };
            return View(lichTruc);
        }

        // POST: LichTruc/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LichTruc lichTruc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra trùng lịch trực
                    if (await _lichTrucService.KiemTraTrungLichTrucAsync(lichTruc.MaNV, lichTruc.Ngay ?? DateTime.Today))
                    {
                        ModelState.AddModelError("", "Nhân viên đã có lịch trực trong ngày này");
                        await LoadSelectListsAsync();
                        return View(lichTruc);
                    }

                    await _lichTrucService.CreateAsync(lichTruc);
                    TempData["SuccessMessage"] = "Thêm lịch trực thành công";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(lichTruc);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm lịch trực: " + ex.Message;
                await LoadSelectListsAsync();
                return View(lichTruc);
            }
        }

        // GET: LichTruc/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var lichTruc = await _lichTrucService.GetByIdAsync(id);
                if (lichTruc == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch trực";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(lichTruc);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LichTruc/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LichTruc lichTruc)
        {
            if (id != lichTruc.MaLich)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    // Kiểm tra trùng lịch trực (bỏ qua lịch hiện tại)
                    if (await _lichTrucService.KiemTraTrungLichTrucAsync(lichTruc.MaNV, lichTruc.Ngay ?? DateTime.Today, lichTruc.MaLich))
                    {
                        ModelState.AddModelError("", "Nhân viên đã có lịch trực khác trong ngày này");
                        await LoadSelectListsAsync();
                        return View(lichTruc);
                    }

                    await _lichTrucService.UpdateAsync(lichTruc);
                    TempData["SuccessMessage"] = "Cập nhật lịch trực thành công";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(lichTruc);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật lịch trực: " + ex.Message;
                await LoadSelectListsAsync();
                return View(lichTruc);
            }
        }

        // GET: LichTruc/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lichTruc = await _lichTrucService.GetByIdAsync(id);
                if (lichTruc == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch trực";
                    return RedirectToAction(nameof(Index));
                }

                return View(lichTruc);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LichTruc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _lichTrucService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa lịch trực thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa lịch trực";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa lịch trực: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LichTruc/LichTrucHomNay
        public async Task<IActionResult> LichTrucHomNay()
        {
            try
            {
                var lichTrucHomNay = await _lichTrucService.GetByNgayAsync(DateTime.Today);
                return View(lichTrucHomNay);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<LichTruc>());
            }
        }

        // API: Kiểm tra trùng lịch trực
        [HttpPost]
        public async Task<IActionResult> KiemTraTrungLichTruc(int maNV, DateTime ngay, int? maLichBỏQua = null)
        {
            try
            {
                var trungLich = await _lichTrucService.KiemTraTrungLichTrucAsync(maNV, ngay, maLichBỏQua);
                return Json(new { trungLich = trungLich });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private async Task LoadSelectListsAsync()
        {
            var nhanViens = await _nhanVienService.GetAllAsync();
            var phongMays = await _phongMayService.GetAllAsync();

            ViewBag.NhanViens = new SelectList(nhanViens, "MaNV", "HoTen");
            ViewBag.PhongMays = new SelectList(phongMays, "MaPhongMay", "TenPhongMay");
        }
    }

}
