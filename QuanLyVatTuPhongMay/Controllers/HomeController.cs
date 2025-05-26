using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuPhongMay.Services.Interfaces;
using QuanLyVatTuPhongMay.Models;
using System.Diagnostics;

namespace QuanLyVatTuPhongMay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITrangTBService _trangTBService;
        private readonly IPhongMayService _phongMayService;
        private readonly ILichThucHanhService _lichThucHanhService;
        private readonly IBaoCaoService _baoCaoService;

        public HomeController(
            ITrangTBService trangTBService,
            IPhongMayService phongMayService,
            ILichThucHanhService lichThucHanhService,
            IBaoCaoService baoCaoService)
        {
            _trangTBService = trangTBService;
            _phongMayService = phongMayService;
            _lichThucHanhService = lichThucHanhService;
            _baoCaoService = baoCaoService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                // Lấy thống kê tổng quan
                var tongSoThietBi = (await _trangTBService.GetAllAsync()).Count();
                var tongSoPhongMay = (await _phongMayService.GetAllAsync()).Count();
                var thietBiCanBaoTri = await _trangTBService.GetCanBaoTriAsync();
                var lichSapToi = await _lichThucHanhService.GetLichTrongKhoangAsync(DateTime.Now, DateTime.Now.AddDays(7));
                var canhBaoThietBi = await _trangTBService.GetCanhBaoThietBiAsync();

                ViewBag.TongSoThietBi = tongSoThietBi;
                ViewBag.TongSoPhongMay = tongSoPhongMay;
                ViewBag.ThietBiCanBaoTri = thietBiCanBaoTri.Count();
                ViewBag.LichSapToi = lichSapToi.Count();
                ViewBag.CanhBaoThietBi = canhBaoThietBi.Take(5); // Hiển thị 5 cảnh báo gần nhất

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi tải trang chủ: " + ex.Message;
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // API để lấy thống kê cho dashboard
        [HttpGet]
        public async Task<IActionResult> GetDashboardStats()
        {
            try
            {
                var stats = new
                {
                    TongSoThietBi = (await _trangTBService.GetAllAsync()).Count(),
                    TongSoPhongMay = (await _phongMayService.GetAllAsync()).Count(),
                    ThietBiCanBaoTri = (await _trangTBService.GetCanBaoTriAsync()).Count(),
                    LichHomNay = (await _lichThucHanhService.GetLichTrongKhoangAsync(DateTime.Today, DateTime.Today.AddDays(1))).Count()
                };

                return Json(stats);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message });
            }
        }
    }
}