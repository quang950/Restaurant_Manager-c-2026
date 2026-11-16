using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.BUS;
using QuanLyNhaHang.WebApi.Helpers;

namespace QuanLyNhaHang.WebApi.Controllers;

[ApiController]
[Route("api")]
public class DanhMucController : ControllerBase
{
    public record MonNuocRequest(string TenMon, string Loai, string DonVi, int Gia);
    public record BanRequest(string TenBan, int SoCho);
    public record NccRequest(string TenNcc, string Sdt, string DiaChi);
    public record NhanVienRequest(string HoTen, string Sdt, string TenDangNhap, string VaiTro, string MatKhau);

    // --- MÓN / NƯỚC ---
    [HttpGet("monnuoc")]
    public IActionResult DsMon([FromQuery] string tim = "") => Ok(DataTableHelper.ToList(MonNuocBUS.DanhSach(tim ?? "")));

    [HttpPost("monnuoc")]
    public IActionResult ThemMon([FromBody] MonNuocRequest r)
    {
        MonNuocBUS.Them(r?.TenMon ?? "", r?.Loai ?? "", r?.DonVi ?? "", r?.Gia ?? 0);
        return Ok(new { message = "Đã thêm món/nước thành công." });
    }

    [HttpPut("monnuoc/{id:int}")]
    public IActionResult SuaMon(int id, [FromBody] MonNuocRequest r)
    {
        MonNuocBUS.Sua(id, r?.TenMon ?? "", r?.Loai ?? "", r?.DonVi ?? "", r?.Gia ?? 0);
        return Ok(new { message = "Đã cập nhật món/nước." });
    }

    [HttpDelete("monnuoc/{id:int}")]
    public IActionResult XoaMon(int id)
    {
        MonNuocBUS.Xoa(id);
        return Ok(new { message = "Đã xóa món/nước." });
    }

    // --- BÀN ---
    [HttpGet("ban")]
    public IActionResult DsBan([FromQuery] string tim = "") => Ok(DataTableHelper.ToList(BanBUS.DanhSach(tim ?? "")));

    [HttpPost("ban")]
    public IActionResult ThemBan([FromBody] BanRequest r)
    {
        BanBUS.Them(r?.TenBan ?? "", r?.SoCho ?? 0);
        return Ok(new { message = "Đã thêm bàn thành công." });
    }

    [HttpPut("ban/{id:int}")]
    public IActionResult SuaBan(int id, [FromBody] BanRequest r)
    {
        BanBUS.Sua(id, r?.TenBan ?? "", r?.SoCho ?? 0);
        return Ok(new { message = "Đã cập nhật bàn." });
    }

    [HttpDelete("ban/{id:int}")]
    public IActionResult XoaBan(int id)
    {
        BanBUS.Xoa(id);
        return Ok(new { message = "Đã xóa bàn." });
    }

    // --- NHÀ CUNG CẤP ---
    [HttpGet("nhacungcap")]
    public IActionResult DsNcc([FromQuery] string tim = "") => Ok(DataTableHelper.ToList(NhaCungCapBUS.DanhSach(tim ?? "")));

    [HttpPost("nhacungcap")]
    public IActionResult ThemNcc([FromBody] NccRequest r)
    {
        NhaCungCapBUS.Them(r?.TenNcc ?? "", r?.Sdt ?? "", r?.DiaChi ?? "");
        return Ok(new { message = "Đã thêm nhà cung cấp." });
    }

    [HttpPut("nhacungcap/{id:int}")]
    public IActionResult SuaNcc(int id, [FromBody] NccRequest r)
    {
        NhaCungCapBUS.Sua(id, r?.TenNcc ?? "", r?.Sdt ?? "", r?.DiaChi ?? "");
        return Ok(new { message = "Đã cập nhật nhà cung cấp." });
    }

    [HttpDelete("nhacungcap/{id:int}")]
    public IActionResult XoaNcc(int id)
    {
        NhaCungCapBUS.Xoa(id);
        return Ok(new { message = "Đã xóa nhà cung cấp." });
    }

    // --- NHÂN VIÊN ---
    [HttpGet("nhanvien")]
    public IActionResult DsNv([FromQuery] string tim = "") => Ok(DataTableHelper.ToList(NhanVienBUS.DanhSach(tim ?? "")));

    [HttpPost("nhanvien")]
    public IActionResult ThemNv([FromBody] NhanVienRequest r)
    {
        NhanVienBUS.Them(r?.HoTen ?? "", r?.Sdt ?? "", r?.TenDangNhap ?? "", r?.VaiTro ?? "", r?.MatKhau ?? "");
        return Ok(new { message = "Đã thêm nhân viên." });
    }

    [HttpPut("nhanvien/{id:int}")]
    public IActionResult SuaNv(int id, [FromBody] NhanVienRequest r)
    {
        NhanVienBUS.Sua(id, r?.HoTen ?? "", r?.Sdt ?? "", r?.TenDangNhap ?? "", r?.VaiTro ?? "");
        return Ok(new { message = "Đã cập nhật nhân viên." });
    }

    [HttpDelete("nhanvien/{id:int}")]
    public IActionResult XoaNv(int id)
    {
        NhanVienBUS.Xoa(id);
        return Ok(new { message = "Đã xóa nhân viên." });
    }
}
