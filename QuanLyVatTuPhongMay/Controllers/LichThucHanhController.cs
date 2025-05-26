using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class LichThucHanhController : Controller
    {
        private readonly ILichThucHanhService _lichThucHanhService;
        private readonly IPhongMayService _phongMayService;
        private readonly INguoiDungService _nguoiDungService;

        public LichThucHanhController(
            ILichThucHanhService lichThucHanhService,
            IPhongMayService phongMayService,
            INguoiDungService nguoiDungService)
        {
            _lichThucHanhService = lichThucHanhService;
            _phongMayService = phongMayService;
            _nguoiDungService = nguoiDungService;
        }

        // GET: LichThucHanh
        public async Task<IActionResult> Index(string? trangThai, DateTime? tuNgay, DateTime? denNgay)
        {
            try
            {
                IEnumerable<LichThucHanh> lichThucHanhs;

                if (!string.IsNullOrEmpty(trangThai))
                {
                    lichThucHanhs = await _lichThucHanhService.GetByTrangThaiAsync(trangThai);
                }
                else if (tuNgay.HasValue && denNgay.HasValue)
                {
                    lichThucHanhs = await _lichThucHanhService.GetLichTrongKhoangAsync(tuNgay.Value, denNgay.Value);
                }
                else
                {
                    lichThucHanhs = await _lichThucHanhService.GetAllAsync();
                }

                ViewBag.TrangThais = new SelectList(new[]
                {
                    new { Value = "", Text = "Tất cả" },
                    new { Value = "Đã đặt", Text = "Đã đặt" },
                    new { Value = "Đang thực hiện", Text = "Đang thực hiện" },
                    new { Value = "Hoàn thành", Text = "Hoàn thành" },
                    new { Value = "Đã hủy", Text = "Đã hủy" }
                }, "Value", "Text", trangThai);

                ViewBag.TrangThai = trangThai;
                ViewBag.TuNgay = tuNgay?.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay?.ToString("yyyy-MM-dd");

                return View(lichThucHanhs.OrderByDescending(l => l.ThoiGianBD));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách lịch thực hành: " + ex.Message;
                return View(new List<LichThucHanh>());
            }
        }

        // GET: LichThucHanh/Details/5
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var lichThucHanh = await _lichThucHanhService.GetByIdAsync(id);
                if (lichThucHanh == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch thực hành";
                    return RedirectToAction(nameof(Index));
                }

                return View(lichThucHanh);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: LichThucHanh/Create
        public async Task<IActionResult> Create()
        {
            await LoadSelectListsAsync();
            var lichThucHanh = new LichThucHanh
            {
                ThoiGianBD = DateTime.Now.AddHours(1),
                ThoiGianKT = DateTime.Now.AddHours(3),
                TrangThai = "Đã đặt"
            };
            return View(lichThucHanh);
        }

        // POST: LichThucHanh/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LichThucHanh lichThucHanh)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _lichThucHanhService.CreateAsync(lichThucHanh);
                    TempData["SuccessMessage"] = "Tạo lịch thực hành thành công";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(lichThucHanh);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo lịch thực hành: " + ex.Message;
                await LoadSelectListsAsync();
                return View(lichThucHanh);
            }
        }

        // GET: LichThucHanh/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var lichThucHanh = await _lichThucHanhService.GetByIdAsync(id);
                if (lichThucHanh == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch thực hành";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(lichThucHanh);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LichThucHanh/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LichThucHanh lichThucHanh)
        {
            if (id != lichThucHanh.MaLich)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                if (ModelState.IsValid)
                {
                    await _lichThucHanhService.UpdateAsync(lichThucHanh);
                    TempData["SuccessMessage"] = "Cập nhật lịch thực hành thành công";
                    return RedirectToAction(nameof(Index));
                }

                await LoadSelectListsAsync();
                return View(lichThucHanh);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật lịch thực hành: " + ex.Message;
                await LoadSelectListsAsync();
                return View(lichThucHanh);
            }
        }

        // GET: LichThucHanh/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var lichThucHanh = await _lichThucHanhService.GetByIdAsync(id);
                if (lichThucHanh == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy lịch thực hành";
                    return RedirectToAction(nameof(Index));
                }

                return View(lichThucHanh);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: LichThucHanh/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _lichThucHanhService.DeleteAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Xóa lịch thực hành thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể xóa lịch thực hành";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa lịch thực hành: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: LichThucHanh/DatLich
        public async Task<IActionResult> DatLich()
        {
            await LoadSelectListsAsync();
            ViewBag.PhongMays = await GetPhongMaySelectListAsync();
            return View();
        }

        // POST: LichThucHanh/DatLich
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DatLich(int maPhongMay, DateTime thoiGianBD, DateTime thoiGianKT, int maNguoiDung)
        {
            try
            {
                var result = await _lichThucHanhService.DatLichAsync(maPhongMay, thoiGianBD, thoiGianKT, maNguoiDung);

                if (result)
                {
                    TempData["SuccessMessage"] = "Đặt lịch thành công";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể đặt lịch. Phòng có thể đã được đặt trong thời gian này.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi đặt lịch: " + ex.Message;
            }

            await LoadSelectListsAsync();
            ViewBag.PhongMays = await GetPhongMaySelectListAsync();
            return View();
        }

        // POST: LichThucHanh/HuyLich
        [HttpPost]
        public async Task<IActionResult> HuyLich(int id, int maNguoiDung)
        {
            try
            {
                var result = await _lichThucHanhService.HuyLichAsync(id, maNguoiDung);

                if (result)
                {
                    return Json(new { success = true, message = "Hủy lịch thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Không thể hủy lịch" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // POST: LichThucHanh/CapNhatTrangThai
        [HttpPost]
        public async Task<IActionResult> CapNhatTrangThai(int id, string trangThai)
        {
            try
            {
                var result = await _lichThucHanhService.CapNhatTrangThaiAsync(id, trangThai);

                if (result)
                {
                    return Json(new { success = true, message = "Cập nhật trạng thái thành công" });
                }
                else
                {
                    return Json(new { success = false, message = "Không thể cập nhật trạng thái" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // API: Kiểm tra lịch trùng
        [HttpPost]
        public async Task<IActionResult> KiemTraTrungLich(DateTime thoiGianBD, DateTime thoiGianKT, int? maLichBỏQua = null)
        {
            try
            {
                var trungLich = await _lichThucHanhService.KiemTraTrungLichAsync(thoiGianBD, thoiGianKT, maLichBỏQua);
                return Json(new { trungLich = trungLich });
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // GET: LichThucHanh/LichHomNay
        public async Task<IActionResult> LichHomNay()
        {
            try
            {
                var homNay = DateTime.Today;
                var lichHomNay = await _lichThucHanhService.GetLichTrongKhoangAsync(homNay, homNay.AddDays(1));

                return View(lichHomNay.OrderBy(l => l.ThoiGianBD));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<LichThucHanh>());
            }
        }

        // GET: LichThucHanh/LichCuaToi
        public async Task<IActionResult> LichCuaToi(int maNguoiDung)
        {
            try
            {
                var lichCuaToi = await _lichThucHanhService.GetByNguoiDungAsync(maNguoiDung);
                var lichSapToi = await _lichThucHanhService.GetLichSapToiAsync(maNguoiDung);

                ViewBag.LichSapToi = lichSapToi;

                return View(lichCuaToi.OrderByDescending(l => l.ThoiGianBD));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra: " + ex.Message;
                return View(new List<LichThucHanh>());
            }
        }

        private async Task LoadSelectListsAsync()
        {
            var nguoiDungs = await _nguoiDungService.GetAllAsync();
            ViewBag.NguoiDungs = new SelectList(nguoiDungs, "MaNguoiDung", "TenDangNhap");

            ViewBag.TrangThais = new SelectList(new[]
            {
                new { Value = "Đã đặt", Text = "Đã đặt" },
                new { Value = "Đang thực hiện", Text = "Đang thực hiện" },
                new { Value = "Hoàn thành", Text = "Hoàn thành" },
                new { Value = "Đã hủy", Text = "Đã hủy" }
            }, "Value", "Text");
        }

        private async Task<SelectList> GetPhongMaySelectListAsync()
        {
            var phongMays = await _phongMayService.GetAllAsync();
            return new SelectList(phongMays, "MaPhongMay", "TenPhongMay");
        }
    }
}