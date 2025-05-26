using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class BaoCaoController : Controller
    {
        private readonly IBaoCaoService _baoCaoService;
        private readonly ITrangTBService _trangTBService;
        private readonly IPhongMayService _phongMayService;
        private readonly ILichThucHanhService _lichThucHanhService;

        public BaoCaoController(
            IBaoCaoService baoCaoService,
            ITrangTBService trangTBService,
            IPhongMayService phongMayService,
            ILichThucHanhService lichThucHanhService)
        {
            _baoCaoService = baoCaoService;
            _trangTBService = trangTBService;
            _phongMayService = phongMayService;
            _lichThucHanhService = lichThucHanhService;
        }

        // GET: BaoCao
        public IActionResult Index()
        {
            return View();
        }

        // GET: BaoCao/TongHop
        public async Task<IActionResult> TongHop(DateTime? tuNgay, DateTime? denNgay)
        {
            try
            {
                if (tuNgay == null) tuNgay = DateTime.Now.AddDays(-30);
                if (denNgay == null) denNgay = DateTime.Now;

                var baoCaoTongHop = await _baoCaoService.GetBaoCaoTongHopAsync(tuNgay, denNgay);

                ViewBag.TuNgay = tuNgay.Value.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay.Value.ToString("yyyy-MM-dd");
                ViewBag.BaoCaoTongHop = baoCaoTongHop;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo báo cáo tổng hợp: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/ThietBiTheoPhong
        public async Task<IActionResult> ThietBiTheoPhong()
        {
            try
            {
                var baoCao = await _trangTBService.GetBaoCaoTheoPhongAsync();
                ViewBag.BaoCao = baoCao;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo báo cáo thiết bị theo phòng: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/ThietBiTheoThuongHieu
        public async Task<IActionResult> ThietBiTheoThuongHieu()
        {
            try
            {
                var thongKe = await _trangTBService.GetThongKeTheoThuongHieuAsync();
                ViewBag.ThongKe = thongKe;
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo thống kê thiết bị theo thương hiệu: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/ThongKeTheoThang
        public async Task<IActionResult> ThongKeTheoThang(int? nam, int? thang)
        {
            try
            {
                if (nam == null) nam = DateTime.Now.Year;

                var thongKe = await _baoCaoService.GetThongKeTheoThangAsync(nam.Value, thang);

                ViewBag.Nam = nam;
                ViewBag.Thang = thang;
                ViewBag.ThongKe = thongKe;

                // Tạo danh sách năm cho dropdown
                var danhSachNam = new List<int>();
                for (int i = DateTime.Now.Year; i >= DateTime.Now.Year - 5; i--)
                {
                    danhSachNam.Add(i);
                }
                ViewBag.DanhSachNam = danhSachNam;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo thống kê theo tháng: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/SuDungPhongMay
        public async Task<IActionResult> SuDungPhongMay(DateTime? tuNgay, DateTime? denNgay, int? maPhongMay)
        {
            try
            {
                if (tuNgay == null) tuNgay = DateTime.Now.AddDays(-30);
                if (denNgay == null) denNgay = DateTime.Now;

                var baoCao = await _phongMayService.GetBaoCaoSuDungAsync(tuNgay.Value, denNgay.Value, maPhongMay);
                var danhSachPhongMay = await _phongMayService.GetAllAsync();

                ViewBag.TuNgay = tuNgay.Value.ToString("yyyy-MM-dd");
                ViewBag.DenNgay = denNgay.Value.ToString("yyyy-MM-dd");
                ViewBag.MaPhongMay = maPhongMay;
                ViewBag.BaoCao = baoCao;
                ViewBag.DanhSachPhongMay = danhSachPhongMay;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo báo cáo sử dụng phòng máy: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/ThietBiCanBaoTri
        public async Task<IActionResult> ThietBiCanBaoTri()
        {
            try
            {
                var thietBiCanBaoTri = await _trangTBService.GetCanBaoTriAsync();
                var canhBaoThietBi = await _trangTBService.GetCanhBaoThietBiAsync();

                ViewBag.ThietBiCanBaoTri = thietBiCanBaoTri;
                ViewBag.CanhBaoThietBi = canhBaoThietBi;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo báo cáo thiết bị cần bảo trì: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/LichSuHoatDongThietBi
        public async Task<IActionResult> LichSuHoatDongThietBi(int? maTTB)
        {
            try
            {
                if (maTTB.HasValue)
                {
                    var lichSu = await _baoCaoService.GetLichSuHoatDongThietBiAsync(maTTB.Value);
                    ViewBag.LichSu = lichSu;
                    ViewBag.MaTTB = maTTB;
                }

                var danhSachThietBi = await _trangTBService.GetAllAsync();
                ViewBag.DanhSachThietBi = danhSachThietBi;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tạo báo cáo lịch sử hoạt động thiết bị: " + ex.Message;
                return View();
            }
        }

        // GET: BaoCao/CanhBaoThietBi
        public async Task<IActionResult> CanhBaoThietBi()
        {
            try
            {
                var canhBaos = await _baoCaoService.GetCanhBaoThietBiAsync();
                return View(canhBaos);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải danh sách cảnh báo: " + ex.Message;
                return View(new List<object>());
            }
        }

        // POST: BaoCao/SaoLuuDuLieu
        [HttpPost]
        public async Task<IActionResult> SaoLuuDuLieu()
        {
            try
            {
                var result = await _baoCaoService.SaoLuuDuLieuAsync();

                if (result)
                {
                    TempData["SuccessMessage"] = "Sao lưu dữ liệu thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể sao lưu dữ liệu";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi sao lưu dữ liệu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: BaoCao/DonDepDuLieu
        [HttpPost]
        public async Task<IActionResult> DonDepDuLieu(int soNgayGiuLai = 365)
        {
            try
            {
                var result = await _baoCaoService.DonDepDuLieuCuAsync(soNgayGiuLai);

                if (result)
                {
                    TempData["SuccessMessage"] = "Dọn dẹp dữ liệu cũ thành công";
                }
                else
                {
                    TempData["ErrorMessage"] = "Không thể dọn dẹp dữ liệu cũ";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi dọn dẹp dữ liệu: " + ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // API: Lấy dữ liệu cho chart
        [HttpGet]
        public async Task<IActionResult> GetChartData(string loaiChart, DateTime? tuNgay, DateTime? denNgay)
        {
            try
            {
                switch (loaiChart.ToLower())
                {
                    case "thietbitheoloai":
                        var thietBis = await _trangTBService.GetAllAsync();
                        var dataTheoLoai = thietBis.GroupBy(t => t.Loai?.TenLoai ?? "Không xác định")
                                                  .Select(g => new { Label = g.Key, Value = g.Count() })
                                                  .ToList();
                        return Json(dataTheoLoai);

                    case "thietbietheophong":
                        var thietBisTheoPhong = await _trangTBService.GetAllAsync();
                        var dataTheoPhong = thietBisTheoPhong.GroupBy(t => t.PhongMay?.TenPhongMay ?? "Không xác định")
                                                            .Select(g => new { Label = g.Key, Value = g.Count() })
                                                            .ToList();
                        return Json(dataTheoPhong);

                    case "lichtheoThang":
                        if (tuNgay == null) tuNgay = DateTime.Now.AddMonths(-6);
                        if (denNgay == null) denNgay = DateTime.Now;

                        var lichThucHanhs = await _lichThucHanhService.GetLichTrongKhoangAsync(tuNgay.Value, denNgay.Value);
                        var dataTheoThang = lichThucHanhs.GroupBy(l => new { l.ThoiGianBD.Year, l.ThoiGianBD.Month })
                                                        .Select(g => new
                                                        {
                                                            Label = $"{g.Key.Month}/{g.Key.Year}",
                                                            Value = g.Count()
                                                        })
                                                        .OrderBy(x => x.Label)
                                                        .ToList();
                        return Json(dataTheoThang);

                    case "tinhtrangthietbi":
                        var tatCaThietBi = await _trangTBService.GetAllAsync();
                        var dataTinhTrang = tatCaThietBi.GroupBy(t => t.TinhTrang ?? "Không xác định")
                                                      .Select(g => new { Label = g.Key, Value = g.Count() })
                                                      .ToList();
                        return Json(dataTinhTrang);

                    default:
                        return Json(new { error = "Loại chart không hợp lệ" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }

        // GET: BaoCao/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            try
            {
                // Tính toán các thống kê tổng quan
                var tongSoThietBi = (await _trangTBService.GetAllAsync()).Count();
                var tongSoPhongMay = (await _phongMayService.GetAllAsync()).Count();
                var thietBiCanBaoTri = (await _trangTBService.GetCanBaoTriAsync()).Count();

                var homNay = DateTime.Today;
                var lichHomNay = (await _lichThucHanhService.GetLichTrongKhoangAsync(homNay, homNay.AddDays(1))).Count();

                var tuanNay = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek);
                var lichTuanNay = (await _lichThucHanhService.GetLichTrongKhoangAsync(tuanNay, tuanNay.AddDays(7))).Count();

                ViewBag.TongSoThietBi = tongSoThietBi;
                ViewBag.TongSoPhongMay = tongSoPhongMay;
                ViewBag.ThietBiCanBaoTri = thietBiCanBaoTri;
                ViewBag.LichHomNay = lichHomNay;
                ViewBag.LichTuanNay = lichTuanNay;

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải dashboard: " + ex.Message;
                return View();
            }
        }

        // POST: BaoCao/XuatExcel
        [HttpPost]
        public async Task<IActionResult> XuatExcel(string loaiBaoCao, string duLieuJson)
        {
            try
            {
                // Implement xuất Excel ở đây
                TempData["InfoMessage"] = "Chức năng xuất Excel sẽ được triển khai sau";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xuất Excel: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: BaoCao/XuatPDF
        [HttpPost]
        public async Task<IActionResult> XuatPDF(string loaiBaoCao, string duLieuJson)
        {
            try
            {
                // Implement xuất PDF ở đây
                TempData["InfoMessage"] = "Chức năng xuất PDF sẽ được triển khai sau";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi xuất PDF: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}