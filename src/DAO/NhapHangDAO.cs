using System.Data;
namespace QuanLyNhaHang.DAO;

public class NhapHangDAO
{
    private MyConnection ketNoi;
    public NhapHangDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public DataTable DanhSach()
    {
        return ketNoi.Doc("SELECT p.mapn,n.tenncc,p.ngaynhap,p.tongtien FROM phieunhap p JOIN nhacungcap n ON p.mancc=n.mancc ORDER BY p.mapn DESC");
    }

    public DataTable ThongTin(int ma)
    {
        return ketNoi.Doc("SELECT p.*,n.hoten FROM phieunhap p JOIN nhanvien n ON p.manv=n.manv WHERE p.mapn=@ma", new string[] {"@ma"}, new object[] {ma});
    }

    public DataTable ChiTiet(int ma)
    {
        return ketNoi.Doc("SELECT c.mamon,m.tenmon,c.soluong,c.dongianhap,c.soluong*c.dongianhap AS thanhtien FROM chitietphieunhap c JOIN monnuoc m ON c.mamon=m.mamon WHERE c.mapn=@ma ORDER BY c.mamon", new string[] {"@ma"}, new object[] {ma});
    }

    public DataTable TheoXacNhan(string xacNhan)
    {
        return ketNoi.Doc("SELECT mapn FROM phieunhap WHERE maxacnhan=@xacNhan", new string[] {"@xacNhan"}, new object[] {xacNhan});
    }

    public int Them(int ncc,int nv,DateTime ngay,int tong,string xacNhan)
    {
        ketNoi.Ghi("INSERT INTO phieunhap (mancc,manv,ngaynhap,tongtien,maxacnhan) VALUES (@ncc,@nv,@ngay,@tong,@xacNhan)", new string[] {"@ncc","@nv","@ngay","@tong","@xacNhan"}, new object[] {ncc,nv,ngay,tong,xacNhan});
        return ketNoi.MaVuaThem;
    }

    public void ThemChiTiet(int ma,int mon,int sl,int gia)
    {
        ketNoi.Ghi("INSERT INTO chitietphieunhap (mapn,mamon,soluong,dongianhap) VALUES (@ma,@mon,@sl,@gia)", new string[] {"@ma","@mon","@sl","@gia"}, new object[] {ma,mon,sl,gia});
    }
}
