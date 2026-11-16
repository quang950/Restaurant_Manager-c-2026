using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.BUS;
using QuanLyNhaHang.WebApi.Helpers;

namespace QuanLyNhaHang.WebApi.Controllers;

[ApiController]
[Route("api/nhaphang")]
public class NhapHangController : ControllerBase
{
    public record DongNhap(int MaMon, string TenMon, int SoLuong, int DonGiaNhap);
    public record GhiNhanRequest(int MaNcc, DateTime NgayNhap, string MaXacNhan, List<DongNhap> ChiTiet);

    [HttpGet("danh-muc")]
    public IActionResult DanhMuc()
    {
        return Ok(new
        {
            ncc = DataTableHelper.ToList(NhaCungCapBUS.NhapHang()),
            mon = DataTableHelper.ToList(MonNuocBUS.NhapHang())
        });
    }

    [HttpGet]
    public IActionResult DanhSach([FromQuery] string tim = "")
    {
        return Ok(DataTableHelper.ToList(NhapHangBUS.DanhSach(tim ?? "")));
    }

    [HttpGet("{id:int}")]
    public IActionResult ChiTiet(int id)
    {
        var thongTin = DataTableHelper.ToList(NhapHangBUS.ThongTin(id));
        if (thongTin.Count == 0) return NotFound(new { message = "Phiếu nhập không tồn tại." });
        var chiTiet = DataTableHelper.ToList(NhapHangBUS.ChiTiet(id));
        return Ok(new { thongTin = thongTin[0], chiTiet });
    }

    [HttpPost]
    public IActionResult GhiNhan([FromBody] GhiNhanRequest req)
    {
        DataTable bang = NhapHangBUS.TaoBangNhap();
        if (req?.ChiTiet != null)
        {
            foreach (var d in req.ChiTiet)
            {
                NhapHangBUS.ThemSuaDong(bang, d.MaMon, d.TenMon ?? "", d.SoLuong, d.DonGiaNhap, true);
            }
        }
        int mapn = NhapHangBUS.GhiNhan(req?.MaNcc ?? 0, req?.NgayNhap ?? DateTime.Now, req?.MaXacNhan ?? "", bang);
        return Ok(new { mapn, message = "Đã ghi nhận phiếu nhập và tăng tồn kho." });
    }
}
