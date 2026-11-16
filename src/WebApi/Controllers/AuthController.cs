using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.BUS;

namespace QuanLyNhaHang.WebApi.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    public record LoginRequest(string TaiKhoan, string MatKhau);
    public record ChangePasswordRequest(string Cu, string Moi, string XacNhan);

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        NhanVienBUS.DangNhap(req?.TaiKhoan ?? "", req?.MatKhau ?? "");
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, NhanVienBUS.MaNhanVien.ToString()),
            new(ClaimTypes.Name, NhanVienBUS.TenNhanVien),
            new(ClaimTypes.Role, NhanVienBUS.VaiTro)
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(identity));

        return Ok(new
        {
            maNhanVien = NhanVienBUS.MaNhanVien,
            tenNhanVien = NhanVienBUS.TenNhanVien,
            vaiTro = NhanVienBUS.VaiTro
        });
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        NhanVienBUS.DangXuat();
        return Ok(new { message = "Đã đăng xuất." });
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        NhanVienBUS.YeuCauDangNhap();
        return Ok(new
        {
            maNhanVien = NhanVienBUS.MaNhanVien,
            tenNhanVien = NhanVienBUS.TenNhanVien,
            vaiTro = NhanVienBUS.VaiTro
        });
    }

    [HttpPost("change-password")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest req)
    {
        NhanVienBUS.DoiMatKhau(req?.Cu ?? "", req?.Moi ?? "", req?.XacNhan ?? "");
        return Ok(new { message = "Đã đổi mật khẩu thành công." });
    }
}
