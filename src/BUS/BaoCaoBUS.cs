using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class BaoCaoBUS
{
    public static string Ten(int loai)
    {
        if(loai==0) return "Báo cáo tổng thu, tổng chi, lợi nhuận";
        if(loai==1) return "Tổng tiền hóa đơn theo nhân viên và quý";
        if(loai==2) return "Tổng tiền hóa đơn theo sản phẩm và quý";
        return "Thống kê món / nước bán chạy";
    }
    public static DataTable Bang(int loai,DateTime tu,DateTime den,bool thang,int nam)
    {
        NhanVienBUS.YeuCauQuanLy();
        if(loai<0 || loai>3) throw new InvalidOperationException("Báo cáo không hợp lệ.");
        DataTable nguon=loai==0?ThongKeBUS.DoanhThu(tu,den,thang):loai==1?ThongKeBUS.NhanVienQuy(nam):loai==2?ThongKeBUS.SanPhamQuy(nam):ThongKeBUS.MonBanChay(tu,den);
        string[] cot=loai==0?new string[]{"ky","sohoadon","doanhthu","tongchi","loinhuan"}:loai==3?new string[]{"tenmon","soluong","thanhtien"}:new string[]{"ten","quy1","quy2","quy3","quy4","tong"};
        string[] ten=loai==0?new string[]{"Kỳ","Số hóa đơn","Tổng thu (đ)","Tổng chi (đ)","Lợi nhuận (đ)"}:loai==3?new string[]{"Tên món / nước","Số lượng","Thành tiền (đ)"}:new string[]{loai==1?"Nhân viên":"Sản phẩm","Quý 1","Quý 2","Quý 3","Quý 4","Tổng (đ)"};
        DataTable b=new DataTable();
        for(int j=0;j<cot.Length;j++) b.Columns.Add(ten[j],j==0?typeof(string):typeof(long));
        for(int i=0;i<nguon.Rows.Count;i++) { DataRow r=b.NewRow(); for(int j=0;j<cot.Length;j++) r[j]=nguon.Rows[i][cot[j]]; b.Rows.Add(r); }
        if(loai==0 || loai==3)
        {
            DataRow tong=b.NewRow(); tong[0]="Tổng";
            for(int j=1;j<b.Columns.Count;j++) { long tien=0; for(int i=0;i<b.Rows.Count;i++) tien+=Convert.ToInt64(b.Rows[i][j]); tong[j]=tien; }
            b.Rows.Add(tong);
        }
        return b;
    }
    public static void XuatPdf(string path,string ten,string moTa,DataTable b,string ghiChu="")
    {
        NhanVienBUS.YeuCauDangNhap();
        if(!string.Equals(Path.GetExtension(path),".pdf",StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Hãy chọn file .pdf.");
        double[] rong=new double[b.Columns.Count]; for(int i=0;i<rong.Length;i++) rong[i]=i==0?2:1;
        try { BaoCaoDAO.Ghi(path,ten,moTa,b,rong,b.Columns.Count>=5,ghiChu); }
        catch(IOException ex) { throw new InvalidOperationException("Không ghi được PDF. Hãy đóng file đang mở và thử lại.",ex); }
        catch(UnauthorizedAccessException ex) { throw new InvalidOperationException("Không có quyền ghi file tại thư mục này.",ex); }
    }
    public static void HoaDonPdf(string path,int ma)
    {
        DataTable hd=HoaDonBUS.ThongTin(ma),ct=HoaDonBUS.ChiTiet(ma);
        if(hd.Rows.Count==0) throw new InvalidOperationException("Chọn hóa đơn trước khi xuất PDF.");
        DataTable b=new DataTable(); b.Columns.Add("Tên món / nước"); b.Columns.Add("Số lượng",typeof(long)); b.Columns.Add("Đơn giá (đ)",typeof(long)); b.Columns.Add("Thành tiền (đ)",typeof(long));
        for(int i=0;i<ct.Rows.Count;i++) b.Rows.Add(ct.Rows[i]["tenmon"],ct.Rows[i]["soluong"],ct.Rows[i]["dongia"],ct.Rows[i]["thanhtien"]);
        DataRow h=hd.Rows[0];
        XuatPdf(path,h["trangthai"].ToString()=="Chưa thanh toán"?"Hóa đơn tạm tính":"Hóa đơn thanh toán","Mã hóa đơn: "+ma+" | "+h["tenban"]+" | Nhân viên: "+h["hoten"]+"\nNgày lập: "+Convert.ToDateTime(h["ngaylap"]).ToString("dd/MM/yyyy HH:mm"),b,"Tổng tiền: "+Convert.ToInt64(h["tongtien"]).ToString("N0")+" đ | "+h["trangthai"]);
    }
}
