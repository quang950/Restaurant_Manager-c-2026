using System.Data;
using System.Globalization;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
namespace QuanLyNhaHang.DAO;

public static class TepXlsDAO
{
    public static void Ghi(string duongDan,DataTable bang)
    {
        if (bang.Columns.Count<1 || bang.Columns.Count>256 || bang.Rows.Count>65535)
            throw new InvalidOperationException("Bảng vượt giới hạn .xls: 256 cột, 65.535 dòng dữ liệu.");
        string tam=duongDan+"."+Guid.NewGuid().ToString("N")+".tmp";
        try
        {
            using (HSSFWorkbook so=new HSSFWorkbook())
            {
                ISheet sheet=so.CreateSheet("DuLieu"); sheet.CreateFreezePane(0,1);
                IFont font=so.CreateFont(); font.FontName="Arial"; font.FontHeightInPoints=11;
                IFont dam=so.CreateFont(); dam.FontName="Arial"; dam.FontHeightInPoints=11; dam.IsBold=true; dam.Color=IndexedColors.White.Index;
                ICellStyle dau=so.CreateCellStyle(); dau.SetFont(dam); dau.FillForegroundColor=IndexedColors.Teal.Index; dau.FillPattern=FillPattern.SolidForeground; dau.WrapText=true;
                ICellStyle chu=so.CreateCellStyle(); chu.SetFont(font); chu.DataFormat=so.CreateDataFormat().GetFormat("@"); chu.VerticalAlignment=VerticalAlignment.Center; chu.WrapText=true;
                ICellStyle tien=so.CreateCellStyle(); tien.SetFont(font); tien.DataFormat=so.CreateDataFormat().GetFormat("#,##0;[Red]-#,##0");
                IRow header=sheet.CreateRow(0); header.HeightInPoints=32;
                for (int j=0;j<bang.Columns.Count;j++)
                {
                    ICell cell=header.CreateCell(j); cell.SetCellValue(bang.Columns[j].ColumnName); cell.CellStyle=dau;
                    int width=bang.Columns[j].DataType==typeof(string)?28:20;
                    if (bang.Columns[j].ColumnName.Length+2>width) width=bang.Columns[j].ColumnName.Length+2;
                    sheet.SetColumnWidth(j,Math.Min(width,60)*256);
                }
                for (int i=0;i<bang.Rows.Count;i++)
                {
                    IRow row=sheet.CreateRow(i+1); row.HeightInPoints=32;
                    for (int j=0;j<bang.Columns.Count;j++)
                    {
                        ICell cell=row.CreateCell(j); object value=bang.Rows[i][j];
                        if (value==DBNull.Value) { cell.SetCellValue(""); cell.CellStyle=chu; }
                        else if (value is int || value is long || value is decimal || value is double)
                        { cell.SetCellValue(Convert.ToDouble(value)); cell.CellStyle=tien; }
                        else { cell.SetCellValue(value is DateTime?((DateTime)value).ToString("dd/MM/yyyy HH:mm"):value.ToString()); cell.CellStyle=chu; }
                    }
                }
                using (FileStream tep=new FileStream(tam,FileMode.CreateNew)) so.Write(tep,true);
            }
            File.Move(tam,duongDan,true);
        }
        finally { if (File.Exists(tam)) File.Delete(tam); }
    }
    public static DataTable Doc(string duongDan)
    {
        using (FileStream tep=new FileStream(duongDan,FileMode.Open,FileAccess.Read,FileShare.Read))
        using (HSSFWorkbook so=new HSSFWorkbook(tep))
        {
            if (so.NumberOfSheets<1) throw new InvalidOperationException("File không có sheet dữ liệu.");
            ISheet sheet=so.GetSheetAt(0); IRow header=sheet.GetRow(0);
            if (header==null || header.LastCellNum<1) throw new InvalidOperationException("Dòng đầu phải là tiêu đề các cột.");
            if (sheet.LastRowNum>5000) throw new InvalidOperationException("Mỗi lần import tối đa 5.000 dòng.");
            DataTable bang=new DataTable();
            for (int j=0;j<header.LastCellNum;j++)
            {
                string ten=header.GetCell(j)?.ToString().Trim()??"";
                if (ten=="" || bang.Columns.Contains(ten)) throw new InvalidOperationException("Tiêu đề cột trống hoặc trùng.");
                bang.Columns.Add(ten,typeof(string));
            }
            for (int i=1;i<=sheet.LastRowNum;i++)
            {
                IRow row=sheet.GetRow(i); if (row==null) continue;
                DataRow dong=bang.NewRow(); bool coDuLieu=false;
                for (int j=0;j<bang.Columns.Count;j++)
                {
                    ICell cell=row.GetCell(j); string value="";
                    if (cell!=null)
                    {
                        if (cell.CellType==CellType.Formula) throw new InvalidOperationException("Dòng "+(i+1)+" chứa công thức. Hãy dán giá trị trước khi import.");
                        value=cell.CellType==CellType.Numeric?cell.NumericCellValue.ToString("R",CultureInfo.InvariantCulture):cell.ToString().Trim();
                    }
                    dong[j]=value; if (value!="") coDuLieu=true;
                }
                if (coDuLieu) bang.Rows.Add(dong);
            }
            return bang;
        }
    }
}
