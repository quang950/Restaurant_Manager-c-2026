using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.BUS;
using QuanLyNhaHang.WebApi.Helpers;

namespace QuanLyNhaHang.WebApi.Controllers;

[ApiController]
[Route("api/hoadon")]
public class HoaDonController : ControllerBase
{
    public record MoBanRequest(int MaBan);
    public record LuuChiTietRequest(int MaMon, int SoLuong, string ThaoTac);

    [HttpGet("pos-data")]
    public IActionResult PosData()
    {
        var ban = DataTableHelper.ToList(BanBUS.GoiMon());
        var mon = DataTableHelper.ToList(MonNuocBUS.GoiMon());
        var hoaDon = DataTableHelper.ToList(HoaDonBUS.DanhSach(""));
        return Ok(new { ban, mon, hoaDon });
    }

    [HttpGet]
    public IActionResult DanhSach([FromQuery] string tim = "")
    {
        return Ok(DataTableHelper.ToList(HoaDonBUS.DanhSach(tim ?? "")));
    }

    [HttpGet("{id:int}")]
    public IActionResult ChiTiet(int id)
    {
        var thongTin = DataTableHelper.ToList(HoaDonBUS.ThongTin(id));
        if (thongTin.Count == 0) return NotFound(new { message = "Hóa đơn không tồn tại." });
        var chiTiet = DataTableHelper.ToList(HoaDonBUS.ChiTiet(id));
        return Ok(new { thongTin = thongTin[0], chiTiet });
    }

    [HttpPost("mo-ban")]
    public IActionResult MoBan([FromBody] MoBanRequest req)
    {
        int mahd = HoaDonBUS.MoBan(req?.MaBan ?? 0);
        return Ok(new { mahd, message = "Đã mở hóa đơn mới." });
    }

    [HttpPut("{id:int}/ban")]
    public IActionResult SuaBan(int id, [FromBody] MoBanRequest req)
    {
        HoaDonBUS.SuaBan(id, req?.MaBan ?? 0);
        return Ok(new { message = "Đã đổi bàn cho hóa đơn." });
    }

    [HttpPost("{id:int}/chi-tiet")]
    public IActionResult LuuChiTiet(int id, [FromBody] LuuChiTietRequest req)
    {
        HoaDonBUS.LuuChiTiet(id, req?.MaMon ?? 0, req?.SoLuong ?? 0, req?.ThaoTac ?? "");
        return Ok(new { message = "Đã cập nhật chi tiết hóa đơn và tồn kho." });
    }

    [HttpPost("{id:int}/thanh-toan")]
    public IActionResult ThanhToan(int id)
    {
        HoaDonBUS.ThanhToan(id);
        return Ok(new { message = "Đã thanh toán hóa đơn." });
    }

    [HttpDelete("{id:int}")]
    public IActionResult Xoa(int id)
    {
        HoaDonBUS.Xoa(id);
        return Ok(new { message = "Đã xóa hóa đơn và hoàn tồn kho." });
    }

    [HttpGet("{id:int}/pdf")]
    public IActionResult XuatPdf(int id)
    {
        byte[] bytes = DataTableHelper.ExportTempFile(".pdf", path => BaoCaoBUS.HoaDonPdf(path, id));
        return File(bytes, "application/pdf", $"HoaDon_{id}.pdf");
    }
}
