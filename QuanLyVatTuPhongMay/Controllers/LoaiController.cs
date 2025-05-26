using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class LoaiController : Controller
    {
        private readonly ILoaiService _loaiService;

        public LoaiController(ILoaiService loaiService)
        {
            _loaiService = loaiService;
        }

        // GET: Loai
        public async Task<IActionResult> Index()
        {
            try
            {
                var loais = await _loaiService.GetAllAsync();
                return View(loais);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách loại thiết bị: " + ex.Message;
                return View(new List<Loai>());
            }
        }

        // GET: Loai/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var loai = await _loaiService.GetByIdAsync(id);
                if (loai == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy loại thiết bị";
                    return RedirectToAction(nameof(Index));
                }

                var thietBiThuocLoai = await _loaiService.GetThietBiTheoLoaiAsync(id);
                ViewBag.ThietBiThuocLoai = thietBiThuocLoai;

                return View(loai);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Loai/Create
        public IActionResult Create()
        {
            return View(new Loai());
        }

        // POST: Loai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Loai loai)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _loaiService.KiemTraTenTonTaiAsync(loai.TenLoai))
                    {
                        ModelState.AddModelError("TenLoai", "Tên loại thiết bị đã tồn tại");
                        return View(loai);
                    }

                    await _loaiService.CreateAsync(loai);
                    TempData["SuccessMessage"] = "Thêm loại thiết bị thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(loai);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm loại thiết bị: " + ex.Message;
                return View(loai);
            }
        }

        // GET: Loai/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var loai = await _loaiService.GetByIdAsync(id);
                if (loai == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy loại thiết bị";
                    return RedirectToAction(nameof(Index));
                }

                return View(loai);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Loai/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Loai loai)
        {
            if (id != loai.MaLoai)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _loaiService.UpdateAsync(loai);
                    TempData["SuccessMessage"] = "Cập nhật loại thiết bị thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(loai);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật loại thiết bị: " + ex.Message;
                return View(loai);
            }
        }

        // GET: Loai/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var loai = await _loaiService.GetByIdAsync(id);
                if (loai == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy loại thiết bị";
                    return RedirectToAction(nameof(Index));
                }

                var soThietBi = (await _loaiService.GetThietBiTheoLoaiAsync(id)).Count();
                ViewBag.SoThietBi = soThietBi;

                return View(loai);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Loai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _loaiService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa loại thiết bị thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa loại thiết bị";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa loại thiết bị: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
