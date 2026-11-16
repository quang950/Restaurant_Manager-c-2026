using System.Data;
using Microsoft.AspNetCore.Mvc;
using QuanLyNhaHang.BUS;
using QuanLyNhaHang.WebApi.Helpers;

namespace QuanLyNhaHang.WebApi.Controllers;

[ApiController]
[Route("api/xls")]
public class TepXlsController : ControllerBase
{
    [HttpGet("mau/{loai:int}")]
    public IActionResult TaiMau(int loai)
    {
        DataTable b = TepXlsBUS.MauNhap(loai);
        byte[] bytes = DataTableHelper.ExportTempFile(".xls", path => TepXlsBUS.Xuat(path, b));
        return File(bytes, "application/vnd.ms-excel", $"MauNhap_{loai}.xls");
    }

    [HttpGet("xuat/{loai:int}")]
    public IActionResult XuatDanhMuc(int loai)
    {
        DataTable b = TepXlsBUS.DanhMuc(loai);
        byte[] bytes = DataTableHelper.ExportTempFile(".xls", path => TepXlsBUS.Xuat(path, b));
        return File(bytes, "application/vnd.ms-excel", $"DanhMuc_{loai}.xls");
    }

    [HttpPost("doc/{loai:int}")]
    public async Task<IActionResult> DocFile(int loai, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "Vui lòng chọn file .xls." });

        string ext = Path.GetExtension(file.FileName);
        if (!string.Equals(ext, ".xls", StringComparison.OrdinalIgnoreCase))
            return BadRequest(new { message = "Chỉ nhận file .xls." });

        string tempPath = Path.Combine(Path.GetTempPath(), $"qlnh_up_{Guid.NewGuid():N}.xls");
        try
        {
            await using (var fs = new FileStream(tempPath, FileMode.CreateNew))
                await file.CopyToAsync(fs);

            DataTable b = TepXlsBUS.Doc(tempPath, loai);
            var columns = b.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();
            return Ok(new { columns, rows = DataTableHelper.ToList(b) });
        }
        finally
        {
            if (System.IO.File.Exists(tempPath)) System.IO.File.Delete(tempPath);
        }
    }

    [HttpPost("nhap/{loai:int}")]
    public async Task<IActionResult> NhapFile(int loai, IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest(new { message = "Vui lòng chọn file .xls." });

        string tempPath = Path.Combine(Path.GetTempPath(), $"qlnh_imp_{Guid.NewGuid():N}.xls");
        try
        {
            await using (var fs = new FileStream(tempPath, FileMode.CreateNew))
                await file.CopyToAsync(fs);

            DataTable b = TepXlsBUS.Doc(tempPath, loai);
            int soDong = TepXlsBUS.Nhap(loai, b);
            return Ok(new { soDong, message = $"Đã thêm {soDong} bản ghi thành công." });
        }
        finally
        {
            if (System.IO.File.Exists(tempPath)) System.IO.File.Delete(tempPath);
        }
    }
}
