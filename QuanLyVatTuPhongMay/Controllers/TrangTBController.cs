using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class TrangTBController : Controller
    {
        private readonly ITrangTBService _trangTBService;
        private readonly IPhongMayService _phongMayService;
        private readonly ILoaiService _loaiService;
        private readonly IThuongHieuService _thuongHieuService;

        public TrangTBController(
            ITrangTBService trangTBService,
            IPhongMayService phongMayService,
            ILoaiService loaiService,
            IThuongHieuService thuongHieuService)
        {
            _trangTBService = trangTBService;
            _phongMayService = phongMayService;
            _loaiService = loaiService;
            _thuongHieuService = thuongHieuService;
        }

        // GET: TrangTB
        public async Task<IActionResult> Index(string? search, int? phongMayId, int? loaiId, int? thuongHieuId, string? tinhTrang)
        {
            try
            {
                IEnumerable<TrangTB> thietBis;

                if (!string.IsNullOrEmpty(search) || phongMayId.HasValue || loaiId.HasValue ||
                    thuongHieuId.HasValue || !string.IsNullOrEmpty(tinhTrang))
                {
                    thietBis = await _trangTBService.TimKiemNangCaoAsync(
                        search, phongMayId, loaiId, thuongHieuId, tinhTrang, null, null, null, null);
                }
                else
                {
                    thietBis = await _trangTBService.GetAllAsync();
                }

                await LoadSelectListsAsync();

                ViewBag.Search = search;
                ViewBag.PhongMayId = phongMayId;
                ViewBag.LoaiId = loaiId;
                ViewBag.ThuongHieuId = thuongHieuId;
                ViewBag.TinhTrang = tinhTrang;

                return View(thietBis);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách thiết bị: " + ex.Message;
                return View(new List<TrangTB>());
            }
        }

        // GET: TrangTB/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var thietBi = await _trangTBService.GetByIdAsync(id);
                if (thietBi == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thiết bị";
                    return RedirectToAction(nameof(Index));
                }

                var lichSuHoatDong = await _trangTBService.GetLichSuHoatDongAsync(id);
                ViewBag.LichSuHoatDong = lichSuHoatDong;

                return View(thietBi);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: TrangTB/Create
        public async Task<IActionResult> Create()
        {
            await LoadSelectListsAsync();
            var thietBi = new TrangTB
            {
                NgayNhap = DateTime.Now,
                SoLanSua = 0,
                TinhTrang = "Mới"
            };
            return View(thietBi);
        }

        // POST: TrangTB/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrangTB thietBi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _trangTBService.CreateAsync(thietBi);
                    TempData["SuccessMessage"] = "Thêm thiết bị thành công";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(thietBi);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi thêm thiết bị: " + ex.Message;
                await LoadSelectListsAsync();
                return View(thietBi);
            }
        }

        // GET: TrangTB/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var thietBi = await _trangTBService.GetByIdAsync(id);
                if (thietBi == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thiết bị";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(thietBi);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TrangTB/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TrangTB thietBi)
        {
            if (id != thietBi.MaTTB)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _trangTBService.UpdateAsync(thietBi);
                    TempData["SuccessMessage"] = "Cập nhật thiết bị thành công";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(thietBi);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật thiết bị: " + ex.Message;
                await LoadSelectListsAsync();
                return View(thietBi);
            }
        }

        // GET: TrangTB/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var thietBi = await _trangTBService.GetByIdAsync(id);
                if (thietBi == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thiết bị";
                    return RedirectToAction(nameof(Index));
                }

                return View(thietBi);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: TrangTB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _trangTBService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa thiết bị thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa thiết bị";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa thiết bị: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: TrangTB/CanBaoTri
        public async Task<IActionResult> CanBaoTri()
        {
            try
            {
                var thietBiCanBaoTri = await _trangTBService.GetCanBaoTriAsync();
                return View(thietBiCanBaoTri);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<TrangTB>());
            }
        }

        // POST: TrangTB/ChuyenTrangThai
        [HttpPost]
        public async Task<IActionResult> ChuyenTrangThai(int id, string tinhTrangMoi, string? ghiChu)
        {
            try
            {
                // Giả sử user ID = 1 (cần implement authentication)
                var result = await _trangTBService.ChuyenTrangThaiThietBiAsync(id, tinhTrangMoi, 1, ghiChu);

                if (result)
                {
                    return Json(new { success = true, message = "Chuyển trạng thái thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Không thể chuyển trạng thái" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: TrangTB/TimKiem
        public async Task<IActionResult> TimKiem()
        {
            await LoadSelectListsAsync();
            return View();
        }

        // POST: TrangTB/TimKiem
        [HttpPost]
        public async Task<IActionResult> TimKiem(string? tuKhoa, int? maPhongMay, int? maLoai,
            int? maThuongHieu, string? tinhTrang, DateTime? tuNgay, DateTime? denNgay,
            int? giaMin, int? giaMax)
        {
            try
            {
                var ketQua = await _trangTBService.TimKiemNangCaoAsync(
                    tuKhoa, maPhongMay, maLoai, maThuongHieu, tinhTrang,
                    tuNgay, denNgay, giaMin, giaMax);

                await LoadSelectListsAsync();

                ViewBag.KetQuaTimKiem = ketQua;
                ViewBag.DaTimKiem = true;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tìm kiếm: " + ex.Message;
                await LoadSelectListsAsync();
                return View();
            }
        }

        // API Methods
        [HttpGet]
        public async Task<IActionResult> GetThietBiByPhong(int phongMayId)
        {
            try
            {
                var thietBis = await _trangTBService.GetByPhongMayAsync(phongMayId);
                return Json(thietBis.Select(t => new
                {
                    t.MaTTB,
                    TenLoai = t.Loai?.TenLoai,
                    TenThuongHieu = t.ThuongHieu?.TenThuongHieu,
                    t.TinhTrang,
                    t.GiaTien
                }));
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCanhBao()
        {
            try
            {
                var canhBaos = await _trangTBService.GetCanhBaoThietBiAsync();
                return Json(canhBaos);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        private async Task LoadSelectListsAsync()
        {
            var phongMays = await _phongMayService.GetAllAsync();
            var loais = await _loaiService.GetAllAsync();
            var thuongHieus = await _thuongHieuService.GetAllAsync();

            ViewBag.PhongMays = new SelectList(phongMays, "MaPhongMay", "TenPhongMay");
            ViewBag.Loais = new SelectList(loais, "MaLoai", "TenLoai");
            ViewBag.ThuongHieus = new SelectList(thuongHieus, "MaThuongHieu", "TenThuongHieu");

            ViewBag.TinhTrangs = new SelectList(new[]
            {
                new { Value = "Mới", Text = "Mới" },
                new { Value = "Tốt", Text = "Tốt" },
                new { Value = "Bình thường", Text = "Bình thường" },
                new { Value = "Cần sửa", Text = "Cần sửa" },
                new { Value = "Hỏng", Text = "Hỏng" },
                new { Value = "Đang sửa", Text = "Đang sửa" },
                new { Value = "Ngưng sử dụng", Text = "Ngưng sử dụng" }
            }, "Value", "Text");
        }
    }
}