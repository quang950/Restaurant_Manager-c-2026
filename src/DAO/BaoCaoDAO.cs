using System.Data;
using System.Collections;
using System.Globalization;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
namespace QuanLyNhaHang.DAO;

public static class BaoCaoDAO
{
    private sealed class ArialResolver : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName,bool bold,bool italic)
        { return new FontResolverInfo(bold?"ArialBold":"ArialRegular"); }
        public byte[] GetFont(string faceName)
        { return File.ReadAllBytes(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts),faceName=="ArialBold"?"arialbd.ttf":"arial.ttf")); }
    }
    private static ArrayList BocDong(XGraphics g,string text,XFont font,double rong)
    {
        ArrayList dong=new ArrayList();
        string[] doan=text.Replace("\r","").Split('\n');
        for (int d=0;d<doan.Length;d++)
        {
            string hienTai=""; string[] tu=doan[d].Split(' ');
            for (int i=0;i<tu.Length;i++)
            {
                string thu=hienTai==""?tu[i]:hienTai+" "+tu[i];
                if (g.MeasureString(thu,font).Width<=rong) hienTai=thu;
                else
                {
                    if (hienTai!="") { dong.Add(hienTai); hienTai=""; }
                    for (int k=0;k<tu[i].Length;k++)
                    {
                        if (g.MeasureString(hienTai+tu[i][k],font).Width>rong && hienTai!="") { dong.Add(hienTai); hienTai=""; }
                        hienTai+=tu[i][k];
                    }
                }
            }
            dong.Add(hienTai);
        }
        return dong;
    }
    private static double VeDong(XGraphics g,string[] noiDung,double[] rong,double y,XFont font,bool toMau)
    {
        ArrayList[] cacDong=new ArrayList[noiDung.Length]; int soDong=1;
        for (int j=0;j<noiDung.Length;j++) { cacDong[j]=BocDong(g,noiDung[j],font,rong[j]-12); if (cacDong[j].Count>soDong) soDong=cacDong[j].Count; }
        double cao=soDong*15+14, x=36;
        for (int j=0;j<noiDung.Length;j++)
        {
            g.DrawRectangle(new XSolidBrush(toMau?XColor.FromArgb(226,237,233):XColors.White),x,y,rong[j],cao);
            g.DrawRectangle(new XPen(XColors.LightGray,0.5),x,y,rong[j],cao);
            for (int k=0;k<cacDong[j].Count;k++) g.DrawString(cacDong[j][k].ToString(),font,XBrushes.Black,new XRect(x+6,y+7+k*15,rong[j]-12,15),XStringFormats.TopLeft);
            x+=rong[j];
        }
        return cao;
    }
    public static void Ghi(string duongDan,string tieuDe,string moTa,DataTable bang,double[] tiLe,bool ngang,string ghiChu)
    {
        if (GlobalFontSettings.FontResolver==null) GlobalFontSettings.FontResolver=new ArialResolver();
        if (bang.Columns.Count==0 || tiLe.Length!=bang.Columns.Count) throw new InvalidOperationException("Cấu trúc báo cáo không hợp lệ.");
        string tam=duongDan+"."+Guid.NewGuid().ToString("N")+".tmp";
        try
        {
            using (PdfDocument pdf=new PdfDocument())
            {
                pdf.Info.Title=tieuDe; pdf.Info.Author="Quản lý nhà hàng";
                XFont chu=new XFont("Arial",10,XFontStyleEx.Regular),dam=new XFont("Arial",10,XFontStyleEx.Bold),lon=new XFont("Arial",18,XFontStyleEx.Bold);
                double[] rong=new double[tiLe.Length]; string[] headers=new string[bang.Columns.Count];
                for (int j=0;j<headers.Length;j++) headers[j]=bang.Columns[j].ColumnName;
                XGraphics g=null; PdfPage page=null; double y=0;
                int i=0;
                do
                {
                    if (g!=null) g.Dispose();
                    page=pdf.AddPage(); page.Size=PdfSharp.PageSize.A4; page.Orientation=ngang?PdfSharp.PageOrientation.Landscape:PdfSharp.PageOrientation.Portrait;
                    g=XGraphics.FromPdfPage(page); double sum=0;
                    for (int j=0;j<tiLe.Length;j++) sum+=tiLe[j];
                    for (int j=0;j<tiLe.Length;j++) rong[j]=(page.Width.Point-72)*tiLe[j]/sum;
                    y=36;
                    ArrayList titleLines=BocDong(g,tieuDe,lon,page.Width.Point-72);
                    for (int k=0;k<titleLines.Count;k++) { g.DrawString(titleLines[k].ToString(),lon,XBrushes.Black,new XRect(36,y,page.Width.Point-72,25),XStringFormats.TopLeft); y+=25; }
                    ArrayList desc=BocDong(g,moTa,chu,page.Width.Point-72);
                    for (int k=0;k<desc.Count;k++) { g.DrawString(desc[k].ToString(),chu,XBrushes.DarkSlateGray,new XRect(36,y+5,page.Width.Point-72,16),XStringFormats.TopLeft); y+=16; }
                    y+=18; y+=VeDong(g,headers,rong,y,dam,true);
                    bool daVe=false;
                    while (i<bang.Rows.Count)
                    {
                        string[] values=new string[bang.Columns.Count]; int maxLines=1;
                        for (int j=0;j<values.Length;j++)
                        {
                            object v=bang.Rows[i][j]; values[j]=v==DBNull.Value?"":v is int||v is long||v is decimal?Convert.ToDecimal(v).ToString("N0",CultureInfo.GetCultureInfo("vi-VN")):v.ToString();
                            int lines=BocDong(g,values[j],chu,rong[j]-12).Count; if (lines>maxLines) maxLines=lines;
                        }
                        double cao=maxLines*15+14;
                        if (y+cao>page.Height.Point-60 && daVe) break;
                        if (y+cao>page.Height.Point-60) throw new InvalidOperationException("Một dòng quá dài để vừa trang PDF. Hãy rút ngắn nội dung.");
                        bool tong=values[0]=="Tổng"; y+=VeDong(g,values,rong,y,tong?dam:chu,tong); i++; daVe=true;
                    }
                    if (bang.Rows.Count==0) { g.DrawString("Không có dữ liệu trong kỳ.",chu,XBrushes.DimGray,new XRect(36,y+14,page.Width.Point-72,20),XStringFormats.TopLeft); y+=45; }
                } while (i<bang.Rows.Count);
                if (ghiChu!="")
                {
                    ArrayList notes=BocDong(g,ghiChu,chu,page.Width.Point-72); double cao=notes.Count*15+20;
                    if (y+cao>page.Height.Point-55)
                    {
                        g.Dispose(); page=pdf.AddPage(); page.Size=PdfSharp.PageSize.A4; page.Orientation=ngang?PdfSharp.PageOrientation.Landscape:PdfSharp.PageOrientation.Portrait; g=XGraphics.FromPdfPage(page); y=36;
                    }
                    for (int k=0;k<notes.Count;k++) g.DrawString(notes[k].ToString(),chu,XBrushes.DarkSlateGray,new XRect(36,y+12+k*15,page.Width.Point-72,16),XStringFormats.TopLeft);
                }
                g.Dispose();
                for (int n=0;n<pdf.PageCount;n++)
                {
                    using (XGraphics footer=XGraphics.FromPdfPage(pdf.Pages[n],XGraphicsPdfPageOptions.Append))
                        footer.DrawString("Trang "+(n+1)+" / "+pdf.PageCount,chu,XBrushes.Gray,new XRect(36,pdf.Pages[n].Height.Point-32,pdf.Pages[n].Width.Point-72,18),XStringFormats.TopRight);
                }
                pdf.Save(tam);
            }
            File.Move(tam,duongDan,true);
        }
        finally { if (File.Exists(tam)) File.Delete(tam); }
    }
}
