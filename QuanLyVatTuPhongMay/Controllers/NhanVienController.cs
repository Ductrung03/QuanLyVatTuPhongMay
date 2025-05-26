using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class NhanVienController : Controller
    {
        private readonly INhanVienService _nhanVienService;

        public NhanVienController(INhanVienService nhanVienService)
        {
            _nhanVienService = nhanVienService;
        }

        // GET: NhanVien
        public async Task<IActionResult> Index(string? search)
        {
            try
            {
                IEnumerable<NhanVien> nhanViens;

                if (!string.IsNullOrEmpty(search))
                {
                    nhanViens = await _nhanVienService.TimKiemTheoTenAsync(search);
                }
                else
                {
                    nhanViens = await _nhanVienService.GetAllAsync();
                }

                ViewBag.Search = search;
                return View(nhanViens);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách nhân viên: " + ex.Message;
                return View(new List<NhanVien>());
            }
        }

        // GET: NhanVien/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var nhanVien = await _nhanVienService.GetByIdAsync(id);
                if (nhanVien == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy nhân viên";
                    return RedirectToAction(nameof(Index));
                }

                var lichTrucNhanVien = await _nhanVienService.GetLichTrucNhanVienAsync(id);
                ViewBag.LichTrucNhanVien = lichTrucNhanVien;

                return View(nhanVien);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: NhanVien/Create
        public IActionResult Create()
        {
            return View(new NhanVien());
        }

        // POST: NhanVien/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhanVien nhanVien)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _nhanVienService.CreateAsync(nhanVien);
                    TempData["SuccessMessage"] = "Thêm nhân viên thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(nhanVien);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm nhân viên: " + ex.Message;
                return View(nhanVien);
            }
        }

        // GET: NhanVien/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var nhanVien = await _nhanVienService.GetByIdAsync(id);
                if (nhanVien == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy nhân viên";
                    return RedirectToAction(nameof(Index));
                }

                return View(nhanVien);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NhanVien/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhanVien nhanVien)
        {
            if (id != nhanVien.MaNV)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _nhanVienService.UpdateAsync(nhanVien);
                    TempData["SuccessMessage"] = "Cập nhật nhân viên thành công";
                    return RedirectToAction(nameof(Index));
                }

                return View(nhanVien);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật nhân viên: " + ex.Message;
                return View(nhanVien);
            }
        }

        // GET: NhanVien/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var nhanVien = await _nhanVienService.GetByIdAsync(id);
                if (nhanVien == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy nhân viên";
                    return RedirectToAction(nameof(Index));
                }

                var soLichTruc = (await _nhanVienService.GetLichTrucNhanVienAsync(id)).Count();
                ViewBag.SoLichTruc = soLichTruc;

                return View(nhanVien);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: NhanVien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _nhanVienService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa nhân viên thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa nhân viên";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa nhân viên: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }

}
