using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLyVatTuPhongMay.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Loai",
                columns: table => new
                {
                    MaLoai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLoai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loai", x => x.MaLoai);
                });

            migrationBuilder.CreateTable(
                name: "Lop",
                columns: table => new
                {
                    MaLop = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenLop = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lop", x => x.MaLop);
                });

            migrationBuilder.CreateTable(
                name: "NguoiDung",
                columns: table => new
                {
                    MaNguoiDung = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MatKhau = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguoiDung", x => x.MaNguoiDung);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    MaNV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.MaNV);
                });

            migrationBuilder.CreateTable(
                name: "PhongMay",
                columns: table => new
                {
                    MaPhongMay = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhongMay = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhongMay", x => x.MaPhongMay);
                });

            migrationBuilder.CreateTable(
                name: "ThuongHieu",
                columns: table => new
                {
                    MaThuongHieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenThuongHieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThuongHieu", x => x.MaThuongHieu);
                });

            migrationBuilder.CreateTable(
                name: "LichSuDangNhap",
                columns: table => new
                {
                    MaLichSu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: true),
                    ThoiDiemDN = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ThoiDiemDX = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuDangNhap", x => x.MaLichSu);
                    table.ForeignKey(
                        name: "FK_LichSuDangNhap_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichThucHanh",
                columns: table => new
                {
                    MaLich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ThoiGianBD = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaNguoiDung = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichThucHanh", x => x.MaLich);
                    table.ForeignKey(
                        name: "FK_LichThucHanh_NguoiDung_MaNguoiDung",
                        column: x => x.MaNguoiDung,
                        principalTable: "NguoiDung",
                        principalColumn: "MaNguoiDung",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LichTruc",
                columns: table => new
                {
                    MaLich = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ngay = table.Column<DateTime>(type: "date", nullable: true),
                    MaNV = table.Column<int>(type: "int", nullable: false),
                    MaPhongMay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichTruc", x => x.MaLich);
                    table.ForeignKey(
                        name: "FK_LichTruc_NhanVien_MaNV",
                        column: x => x.MaNV,
                        principalTable: "NhanVien",
                        principalColumn: "MaNV",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LichTruc_PhongMay_MaPhongMay",
                        column: x => x.MaPhongMay,
                        principalTable: "PhongMay",
                        principalColumn: "MaPhongMay",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NhatKyThayDoi",
                columns: table => new
                {
                    MaNhatKy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaLichSu = table.Column<int>(type: "int", nullable: true),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThongTinCu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThongTinMoi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhatKyThayDoi", x => x.MaNhatKy);
                    table.ForeignKey(
                        name: "FK_NhatKyThayDoi_LichSuDangNhap_MaLichSu",
                        column: x => x.MaLichSu,
                        principalTable: "LichSuDangNhap",
                        principalColumn: "MaLichSu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrangTB",
                columns: table => new
                {
                    MaTTB = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaPhongMay = table.Column<int>(type: "int", nullable: true),
                    MaLoai = table.Column<int>(type: "int", nullable: true),
                    GiaTien = table.Column<int>(type: "int", nullable: true),
                    TinhTrang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NgayNhap = table.Column<DateTime>(type: "date", nullable: false),
                    SoLanSua = table.Column<int>(type: "int", nullable: true),
                    MaThuongHieu = table.Column<int>(type: "int", nullable: true),
                    MaLich = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrangTB", x => x.MaTTB);
                    table.ForeignKey(
                        name: "FK_TrangTB_LichThucHanh_MaLich",
                        column: x => x.MaLich,
                        principalTable: "LichThucHanh",
                        principalColumn: "MaLich",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrangTB_Loai_MaLoai",
                        column: x => x.MaLoai,
                        principalTable: "Loai",
                        principalColumn: "MaLoai",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrangTB_PhongMay_MaPhongMay",
                        column: x => x.MaPhongMay,
                        principalTable: "PhongMay",
                        principalColumn: "MaPhongMay",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrangTB_ThuongHieu_MaThuongHieu",
                        column: x => x.MaThuongHieu,
                        principalTable: "ThuongHieu",
                        principalColumn: "MaThuongHieu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LichSuDangNhap_MaNguoiDung",
                table: "LichSuDangNhap",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_LichThucHanh_MaNguoiDung",
                table: "LichThucHanh",
                column: "MaNguoiDung");

            migrationBuilder.CreateIndex(
                name: "IX_LichTruc_MaNV",
                table: "LichTruc",
                column: "MaNV");

            migrationBuilder.CreateIndex(
                name: "IX_LichTruc_MaPhongMay",
                table: "LichTruc",
                column: "MaPhongMay");

            migrationBuilder.CreateIndex(
                name: "IX_NhatKyThayDoi_MaLichSu",
                table: "NhatKyThayDoi",
                column: "MaLichSu");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTB_MaLich",
                table: "TrangTB",
                column: "MaLich");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTB_MaLoai",
                table: "TrangTB",
                column: "MaLoai");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTB_MaPhongMay",
                table: "TrangTB",
                column: "MaPhongMay");

            migrationBuilder.CreateIndex(
                name: "IX_TrangTB_MaThuongHieu",
                table: "TrangTB",
                column: "MaThuongHieu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LichTruc");

            migrationBuilder.DropTable(
                name: "Lop");

            migrationBuilder.DropTable(
                name: "NhatKyThayDoi");

            migrationBuilder.DropTable(
                name: "TrangTB");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "LichSuDangNhap");

            migrationBuilder.DropTable(
                name: "LichThucHanh");

            migrationBuilder.DropTable(
                name: "Loai");

            migrationBuilder.DropTable(
                name: "PhongMay");

            migrationBuilder.DropTable(
                name: "ThuongHieu");

            migrationBuilder.DropTable(
                name: "NguoiDung");
        }
    }
}
