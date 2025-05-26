using Microsoft.EntityFrameworkCore;
using QuanLyVatTuPhongMay.Data;
using QuanLyVatTuPhongMay.Repositories.Interfaces;
using QuanLyVatTuPhongMay.Repositories;
using QuanLyVatTuPhongMay.Services.Interfaces;
using QuanLyVatTuPhongMay.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register Repositories
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<ITrangTBRepository, TrangTBRepository>();
builder.Services.AddScoped<IPhongMayRepository, PhongMayRepository>();
builder.Services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
builder.Services.AddScoped<ILichThucHanhRepository, LichThucHanhRepository>();
builder.Services.AddScoped<IThuongHieuRepository, ThuongHieuRepository>();
builder.Services.AddScoped<ILoaiRepository, LoaiRepository>();
builder.Services.AddScoped<INhanVienRepository, NhanVienRepository>();
builder.Services.AddScoped<ILichTrucRepository, LichTrucRepository>();
builder.Services.AddScoped<ILichSuDangNhapRepository, LichSuDangNhapRepository>();
builder.Services.AddScoped<INhatKyThayDoiRepository, NhatKyThayDoiRepository>();
builder.Services.AddScoped<ILopRepository, LopRepository>();
builder.Services.AddScoped<IBaoCaoRepository, BaoCaoRepository>();

// Register Services
builder.Services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
builder.Services.AddScoped<ITrangTBService, TrangTBService>();
builder.Services.AddScoped<IPhongMayService, PhongMayService>();
builder.Services.AddScoped<INguoiDungService, NguoiDungService>();
builder.Services.AddScoped<ILichThucHanhService, LichThucHanhService>();
builder.Services.AddScoped<IThuongHieuService, ThuongHieuService>();
builder.Services.AddScoped<ILoaiService, LoaiService>();
builder.Services.AddScoped<INhanVienService, NhanVienService>();
builder.Services.AddScoped<ILichTrucService, LichTrucService>();
builder.Services.AddScoped<ILopService, LopService>();
builder.Services.AddScoped<ILichSuDangNhapService, LichSuDangNhapService>();
builder.Services.AddScoped<INhatKyThayDoiService, NhatKyThayDoiService>();
builder.Services.AddScoped<IBaoCaoService, BaoCaoService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddSingleton<ICacheService, CacheService>();
builder.Services.AddScoped<IEmailService, EmailService>();

// Add Memory Cache
builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use Session
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();

