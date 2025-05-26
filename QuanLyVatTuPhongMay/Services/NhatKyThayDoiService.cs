using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Services.Interfaces;

namespace QuanLyVatTuPhongMay.Services
{
    public class NhatKyThayDoiService : INhatKyThayDoiService
    {
        private readonly INhatKyThayDoiRepository _nhatKyThayDoiRepository;

        public NhatKyThayDoiService(INhatKyThayDoiRepository nhatKyThayDoiRepository)
        {
            _nhatKyThayDoiRepository = nhatKyThayDoiRepository;
        }

        public async Task<bool> ThemNhatKyAsync(int maLichSu, string noiDung, string? thongTinCu, string? thongTinMoi)
        {
            return await _nhatKyThayDoiRepository.ThemNhatKyAsync(maLichSu, noiDung, thongTinCu, thongTinMoi);
        }

        public async Task<IEnumerable<NhatKyThayDoi>> GetByLichSuAsync(int maLichSu)
        {
            return await _nhatKyThayDoiRepository.GetByLichSuAsync(maLichSu);
        }

        public async Task<IEnumerable<NhatKyThayDoi>> GetTrongKhoangAsync(DateTime tuNgay, DateTime denNgay)
        {
            return await _nhatKyThayDoiRepository.GetTrongKhoangAsync(tuNgay, denNgay);
        }

        public async Task<IEnumerable<NhatKyThayDoi>> GetTatCaNhatKyAsync()
        {
            return await _nhatKyThayDoiRepository.GetAllAsync();
        }
    }
} 