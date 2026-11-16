using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Diagnostics;
using QuanLyNhaHang.BUS;

var builder = WebApplication.CreateBuilder(args);

NhanVienBUS.SuDungPhienAsync = true;

builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "QuanLyNhaHang.Auth";
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.ExpireTimeSpan = TimeSpan.FromHours(12);
        options.Events.OnRedirectToLogin = ctx =>
        {
            ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return ctx.Response.WriteAsJsonAsync(new { message = "Bạn cần đăng nhập trước." });
        };
        options.Events.OnRedirectToAccessDenied = ctx =>
        {
            ctx.Response.StatusCode = StatusCodes.Status403Forbidden;
            return ctx.Response.WriteAsJsonAsync(new { message = "Chức năng này chỉ dành cho quản lý." });
        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler(errApp =>
{
    errApp.Run(async context =>
    {
        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        if (ex is InvalidOperationException inv)
        {
            int status = StatusCodes.Status400BadRequest;
            if (inv.Message.Contains("đăng nhập trước")) status = StatusCodes.Status401Unauthorized;
            else if (inv.Message.Contains("chỉ dành cho quản lý")) status = StatusCodes.Status403Forbidden;
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(new { message = inv.Message });
            return;
        }
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsJsonAsync(new { message = "Lỗi hệ thống: " + (ex?.Message ?? "Không xác định") });
    });
});

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.Use(async (context, next) =>
{
    if (context.User.Identity?.IsAuthenticated == true)
    {
        int.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out int ma);
        string ten = context.User.FindFirstValue(ClaimTypes.Name) ?? "";
        string vaiTro = context.User.FindFirstValue(ClaimTypes.Role) ?? "";
        NhanVienBUS.DatPhien(ma, ten, vaiTro);
    }
    else
    {
        NhanVienBUS.DangXuat();
    }
    await next();
});
app.UseAuthorization();

app.MapControllers();

app.Run();
