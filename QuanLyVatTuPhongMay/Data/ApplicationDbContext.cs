
using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QuanLyVatTuPhongMay.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet cho từng bảng
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<PhongMay> PhongMays { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<LichThucHanh> LichThucHanhs { get; set; }
        public DbSet<LichTruc> LichTrucs { get; set; }
        public DbSet<LichSuDangNhap> LichSuDangNhaps { get; set; }
        public DbSet<NhatKyThayDoi> NhatKyThayDois { get; set; }
        public DbSet<TrangTB> TrangTBs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng NguoiDung
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.ToTable("NguoiDung");
                entity.HasKey(e => e.MaNguoiDung);
                entity.Property(e => e.TenDangNhap)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            // Cấu hình bảng NhanVien
            modelBuilder.Entity<NhanVien>(entity =>
            {
                entity.ToTable("NhanVien");
                entity.HasKey(e => e.MaNV);
                entity.Property(e => e.HoTen).HasMaxLength(100);
            });

            // Cấu hình bảng PhongMay
            modelBuilder.Entity<PhongMay>(entity =>
            {
                entity.ToTable("PhongMay");
                entity.HasKey(e => e.MaPhongMay);
                entity.Property(e => e.MaPhongMay).ValueGeneratedOnAdd();
                entity.Property(e => e.TenPhongMay).HasMaxLength(50);
            });

            // Cấu hình bảng Lop
            modelBuilder.Entity<Lop>(entity =>
            {
                entity.ToTable("Lop");
                entity.HasKey(e => e.MaLop);
                entity.Property(e => e.MaLop).ValueGeneratedOnAdd();
                entity.Property(e => e.TenLop).HasMaxLength(50);
            });

            // Cấu hình bảng ThuongHieu
            modelBuilder.Entity<ThuongHieu>(entity =>
            {
                entity.ToTable("ThuongHieu");
                entity.HasKey(e => e.MaThuongHieu);
                entity.Property(e => e.MaThuongHieu).ValueGeneratedOnAdd();
                entity.Property(e => e.TenThuongHieu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            // Cấu hình bảng Loai
            modelBuilder.Entity<Loai>(entity =>
            {
                entity.ToTable("Loai");
                entity.HasKey(e => e.MaLoai);
                entity.Property(e => e.TenLoai)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            // Cấu hình bảng LichThucHanh
            modelBuilder.Entity<LichThucHanh>(entity =>
            {
                entity.ToTable("LichThucHanh");
                entity.HasKey(e => e.MaLich);
                entity.Property(e => e.MaLich).ValueGeneratedOnAdd();
                entity.Property(e => e.TrangThai).HasMaxLength(50);
                entity.Property(e => e.ThoiGianBD).IsRequired();
                entity.Property(e => e.ThoiGianKT).IsRequired();

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.LichThucHanhs)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình bảng LichTruc
            modelBuilder.Entity<LichTruc>(entity =>
            {
                entity.ToTable("LichTruc");
                entity.HasKey(e => e.MaLich);
                entity.Property(e => e.Ngay).HasColumnType("date");

                entity.HasOne(d => d.NhanVien)
                    .WithMany(p => p.LichTrucs)
                    .HasForeignKey(d => d.MaNV)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PhongMay)
                    .WithMany(p => p.LichTrucs)
                    .HasForeignKey(d => d.MaPhongMay)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình bảng LichSuDangNhap
            modelBuilder.Entity<LichSuDangNhap>(entity =>
            {
                entity.ToTable("LichSuDangNhap");
                entity.HasKey(e => e.MaLichSu);

                entity.HasOne(d => d.NguoiDung)
                    .WithMany(p => p.LichSuDangNhaps)
                    .HasForeignKey(d => d.MaNguoiDung)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình bảng NhatKyThayDoi
            modelBuilder.Entity<NhatKyThayDoi>(entity =>
            {
                entity.ToTable("NhatKyThayDoi");
                entity.HasKey(e => e.MaNhatKy);

                entity.HasOne(d => d.LichSuDangNhap)
                    .WithMany(p => p.NhatKyThayDois)
                    .HasForeignKey(d => d.MaLichSu)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Cấu hình bảng TrangTB
            modelBuilder.Entity<TrangTB>(entity =>
            {
                entity.ToTable("TrangTB");
                entity.HasKey(e => e.MaTTB);
                entity.Property(e => e.MaTTB).ValueGeneratedOnAdd();
                entity.Property(e => e.TinhTrang).HasMaxLength(100);
                entity.Property(e => e.NgayNhap)
                    .IsRequired()
                    .HasColumnType("date");

                entity.HasOne(d => d.PhongMay)
                    .WithMany(p => p.TrangTBs)
                    .HasForeignKey(d => d.MaPhongMay)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Loai)
                    .WithMany(p => p.TrangTBs)
                    .HasForeignKey(d => d.MaLoai)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.ThuongHieu)
                    .WithMany(p => p.TrangTBs)
                    .HasForeignKey(d => d.MaThuongHieu)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.LichThucHanh)
                    .WithMany(p => p.TrangTBs)
                    .HasForeignKey(d => d.MaLich)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data có thể được thêm vào đây nếu cần
        }
    }

}
