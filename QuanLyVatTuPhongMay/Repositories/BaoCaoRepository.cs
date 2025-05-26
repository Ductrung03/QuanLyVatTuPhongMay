using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Models;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using System.Data;

namespace QuanLyVatTuPhongMay.Repositories
{
    public class BaoCaoRepository : IBaoCaoRepository
    {
        private readonly ApplicationDbContext _context;

        public BaoCaoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> GetBaoCaoTongHopAsync(DateTime? tuNgay = null, DateTime? denNgay = null)
        {
            var parameters = new[]
            {
                new SqlParameter("@TuNgay", tuNgay ?? (object)DBNull.Value),
                new SqlParameter("@DenNgay", denNgay ?? (object)DBNull.Value)
            };

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_BaoCaoTongHop";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.Select(p => (IDbDataParameter)p).ToArray());

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            // Đọc multiple result sets
            do
            {
                var resultSet = new List<Dictionary<string, object>>();
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    resultSet.Add(row);
                }
                results.Add(resultSet);
            } while (await reader.NextResultAsync());

            await connection.CloseAsync();
            return results;
        }

        public async Task<object> GetThongKeTheoThangAsync(int nam, int? thang = null)
        {
            var parameters = new[]
            {
                new SqlParameter("@Nam", nam),
                new SqlParameter("@Thang", thang ?? (object)DBNull.Value)
            };

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_ThongKeTheoThang";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddRange(parameters.Select(p => (IDbDataParameter)p).ToArray());

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            do
            {
                var resultSet = new List<Dictionary<string, object>>();
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    resultSet.Add(row);
                }
                results.Add(resultSet);
            } while (await reader.NextResultAsync());

            await connection.CloseAsync();
            return results;
        }

        public async Task<IEnumerable<object>> GetCanhBaoThietBiAsync()
        {
            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_CanhBaoThietBi";
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            while (await reader.ReadAsync())
            {
                results.Add(new
                {
                    LoaiCanhBao = reader.GetString("LoaiCanhBao"),
                    MaTTB = reader.GetInt32("MaTTB"),
                    TenPhongMay = reader.GetString("TenPhongMay"),
                    TenLoai = reader.GetString("TenLoai"),
                    TenThuongHieu = reader.GetString("TenThuongHieu"),
                    TinhTrang = reader.GetString("TinhTrang"),
                    SoLanSua = reader.GetInt32("SoLanSua"),
                    NgayNhap = reader.GetDateTime("NgayNhap"),
                    NoiDungCanhBao = reader.GetString("NoiDungCanhBao")
                });
            }

            await connection.CloseAsync();
            return results;
        }

        public async Task<object> GetLichSuHoatDongThietBiAsync(int maTTB)
        {
            var parameter = new SqlParameter("@MaTTB", maTTB);

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_LichSuHoatDongThietBi";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add((IDbDataParameter)parameter);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            var results = new List<object>();

            do
            {
                var resultSet = new List<Dictionary<string, object>>();
                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row[reader.GetName(i)] = reader.GetValue(i);
                    }
                    resultSet.Add(row);
                }
                results.Add(resultSet);
            } while (await reader.NextResultAsync());

            await connection.CloseAsync();
            return results;
        }

        public async Task<bool> SaoLuuDuLieuAsync()
        {
            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_SaoLuuDuLieu";
            command.CommandType = CommandType.StoredProcedure;

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            bool thanhCong = false;

            if (await reader.ReadAsync())
            {
                thanhCong = reader.GetInt32("ThanhCong") == 1;
            }

            await connection.CloseAsync();
            return thanhCong;
        }

        public async Task<bool> DonDepDuLieuCuAsync(int soNgayGiuLai = 365)
        {
            var parameter = new SqlParameter("@SoNgayGiuLai", soNgayGiuLai);

            var connection = _context.Database.GetDbConnection();
            var command = connection.CreateCommand();
            command.CommandText = "sp_DonDepDuLieuCu";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add((IDbDataParameter)parameter);

            await connection.OpenAsync();
            var reader = await command.ExecuteReaderAsync();
            bool thanhCong = false;

            if (await reader.ReadAsync())
            {
                thanhCong = reader.GetInt32("ThanhCong") == 1;
            }

            await connection.CloseAsync();
            return thanhCong;
        }
    }



}