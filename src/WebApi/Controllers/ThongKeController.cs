using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.BUS;
using QuanLyNhaHang.WebApi.Helpers;

namespace QuanLyNhaHang.WebApi.Controllers;

[ApiController]
[Route("api/thongke")]
public class ThongKeController : ControllerBase
{
    [HttpGet("tong-quan")]
    public IActionResult TongQuan()
    {
        var tongQuan = DataTableHelper.ToList(ThongKeBUS.TongQuan());
        List<Dictionary<string, object>> tonThap = null;
        if (NhanVienBUS.VaiTro == "Quản lý")
        {
            var mon = DataTableHelper.ToList(MonNuocBUS.DanhSach(""));
            tonThap = mon.Where(m => Convert.ToInt32(m["soluongton"]) < 10)
                         .OrderBy(m => Convert.ToInt32(m["soluongton"]))
                         .Take(8)
                         .ToList();
        }
        return Ok(new { tongQuan = tongQuan[0], tonThap });
    }

    [HttpGet("bao-cao")]
    public IActionResult BaoCao(
        [FromQuery] int loai = 0,
        [FromQuery] DateTime? tu = null,
        [FromQuery] DateTime? den = null,
        [FromQuery] bool thang = false,
        [FromQuery] int nam = 0)
    {
        DateTime dTu = tu ?? DateTime.Today.AddDays(-29);
        DateTime dDen = den ?? DateTime.Today;
        int n = nam > 0 ? nam : DateTime.Today.Year;
        DataTable bang = BaoCaoBUS.Bang(loai, dTu, dDen, thang, n);
        var columns = bang.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
        return Ok(new
        {
            ten = BaoCaoBUS.Ten(loai),
            columns,
            rows = DataTableHelper.ToList(bang)
        });
    }

    [HttpGet("xuat-pdf")]
    public IActionResult XuatPdf(
        [FromQuery] int loai = 0,
        [FromQuery] DateTime? tu = null,
        [FromQuery] DateTime? den = null,
        [FromQuery] bool thang = false,
        [FromQuery] int nam = 0)
    {
        DateTime dTu = tu ?? DateTime.Today.AddDays(-29);
        DateTime dDen = den ?? DateTime.Today;
        int n = nam > 0 ? nam : DateTime.Today.Year;
        DataTable bang = BaoCaoBUS.Bang(loai, dTu, dDen, thang, n);
        string ky = (loai == 1 || loai == 2)
            ? $"Năm {n}"
            : $"Từ {dTu:dd/MM/yyyy} đến {dDen:dd/MM/yyyy}";
        string ghiChu = loai == 0
            ? "Tổng thu = hóa đơn đã thanh toán. Tổng chi = tiền nhập hàng. Lợi nhuận = tổng thu - tổng chi."
            : "Chỉ tính hóa đơn đã thanh toán; thời gian dựa trên ngày thanh toán.";

        byte[] bytes = DataTableHelper.ExportTempFile(".pdf", path =>
            BaoCaoBUS.XuatPdf(path, BaoCaoBUS.Ten(loai), $"{ky} | Đơn vị tiền: VND", bang, ghiChu));
        return File(bytes, "application/pdf", $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
    }

    [HttpGet("xuat-xls")]
    public IActionResult XuatXls(
        [FromQuery] int loai = 0,
        [FromQuery] DateTime? tu = null,
        [FromQuery] DateTime? den = null,
        [FromQuery] bool thang = false,
        [FromQuery] int nam = 0)
    {
        DateTime dTu = tu ?? DateTime.Today.AddDays(-29);
        DateTime dDen = den ?? DateTime.Today;
        int n = nam > 0 ? nam : DateTime.Today.Year;
        DataTable bang = BaoCaoBUS.Bang(loai, dTu, dDen, thang, n);

        byte[] bytes = DataTableHelper.ExportTempFile(".xls", path => TepXlsBUS.Xuat(path, bang));
        return File(bytes, "application/vnd.ms-excel", $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.xls");
    }
}
