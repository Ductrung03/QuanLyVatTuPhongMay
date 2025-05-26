using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class ThuongHieuController : Controller
    {
        private readonly IThuongHieuService _thuongHieuService;

        public ThuongHieuController(IThuongHieuService thuongHieuService)
        {
            _thuongHieuService = thuongHieuService;
        }

        // GET: ThuongHieu
        public async Task<IActionResult> Index()
        {
            try
            {
                var thuongHieus = await _thuongHieuService.GetAllAsync();
                return View(thuongHieus);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách thương hiệu: " + ex.Message;
                return View(new List<ThuongHieu>());
            }
        }

        // GET: ThuongHieu/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var thuongHieu = await _thuongHieuService.GetByIdAsync(id);
                if (thuongHieu == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thương hiệu";
                    return RedirectToAction(nameof(Index));
                }

                var thietBiThuocThuongHieu = await _thuongHieuService.GetThietBiTheoThuongHieuAsync(id);
                ViewBag.ThietBiThuocThuongHieu = thietBiThuocThuongHieu;

                return View(thuongHieu);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: ThuongHieu/Create
        public IActionResult Create()
        {
            return View(new ThuongHieu());
        }

        // POST: ThuongHieu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ThuongHieu thuongHieu)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _thuongHieuService.KiemTraTenTonTaiAsync(thuongHieu.TenThuongHieu))
                    {
                        ModelState.AddModelError("TenThuongHieu", "Tên thương hiệu đã tồn tại");
                        return View(thuongHieu);
                    }

                    await _thuongHieuService.CreateAsync(thuongHieu);
                    TempData["SuccessMessage"] = "Thêm thương hiệu thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(thuongHieu);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm thương hiệu: " + ex.Message;
                return View(thuongHieu);
            }
        }

        // GET: ThuongHieu/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var thuongHieu = await _thuongHieuService.GetByIdAsync(id);
                if (thuongHieu == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thương hiệu";
                    return RedirectToAction(nameof(Index));
                }

                return View(thuongHieu);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ThuongHieu/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ThuongHieu thuongHieu)
        {
            if (id != thuongHieu.MaThuongHieu)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _thuongHieuService.UpdateAsync(thuongHieu);
                    TempData["SuccessMessage"] = "Cập nhật thương hiệu thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(thuongHieu);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật thương hiệu: " + ex.Message;
                return View(thuongHieu);
            }
        }

        // GET: ThuongHieu/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var thuongHieu = await _thuongHieuService.GetByIdAsync(id);
                if (thuongHieu == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thương hiệu";
                    return RedirectToAction(nameof(Index));
                }

                var soThietBi = (await _thuongHieuService.GetThietBiTheoThuongHieuAsync(id)).Count();
                ViewBag.SoThietBi = soThietBi;

                return View(thuongHieu);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: ThuongHieu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _thuongHieuService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa thương hiệu thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa thương hiệu";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa thương hiệu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
