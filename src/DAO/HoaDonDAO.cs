using System.Data;
namespace QuanLyNhaHang.DAO;

public class HoaDonDAO
{
    private MyConnection ketNoi;
    public HoaDonDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public DataTable DanhSach()
    {
        return ketNoi.Doc("SELECT h.mahd,h.maban,b.tenban,n.hoten,h.ngaylap,h.ngaythanhtoan,h.trangthai,h.tongtien FROM hoadon h JOIN ban b ON h.maban=b.maban JOIN nhanvien n ON h.manv=n.manv ORDER BY h.mahd DESC");
    }

    public DataTable ThongTin(int ma)
    {
        return ketNoi.Doc("SELECT h.*,b.tenban,n.hoten FROM hoadon h JOIN ban b ON h.maban=b.maban JOIN nhanvien n ON h.manv=n.manv WHERE h.mahd=@ma", new string[] {"@ma"}, new object[] {ma});
    }

    public DataTable Khoa(int ma)
    {
        return ketNoi.Doc("SELECT * FROM hoadon WHERE mahd=@ma FOR UPDATE", new string[] {"@ma"}, new object[] {ma});
    }

    public int DemDangMo(int ban,int boQua)
    {
        return Convert.ToInt32(ketNoi.Doc("SELECT COUNT(*) FROM hoadon WHERE maban=@ban AND trangthai='Chưa thanh toán' AND mahd<>@boQua", new string[] {"@ban","@boQua"}, new object[] {ban,boQua}).Rows[0][0]);
    }

    public int Tao(int ban,int nv)
    {
        ketNoi.Ghi("INSERT INTO hoadon (maban,manv) VALUES (@ban,@nv)", new string[] {"@ban","@nv"}, new object[] {ban,nv});
        return ketNoi.MaVuaThem;
    }

    public void SuaBan(int ma,int ban)
    {
        ketNoi.Ghi("UPDATE hoadon SET maban=@ban WHERE mahd=@ma", new string[] {"@ma","@ban"}, new object[] {ma,ban});
    }

    public DataTable ChiTiet(int ma)
    {
        return ketNoi.Doc("SELECT c.mamon,m.tenmon,c.soluong,c.dongia,c.soluong*c.dongia AS thanhtien FROM chitiethoadon c JOIN monnuoc m ON c.mamon=m.mamon WHERE c.mahd=@ma ORDER BY c.mamon", new string[] {"@ma"}, new object[] {ma});
    }

    public int SoLuong(int ma,int mon)
    {
        return Convert.ToInt32(ketNoi.Doc("SELECT COALESCE(SUM(soluong),0) FROM chitiethoadon WHERE mahd=@ma AND mamon=@mon", new string[] {"@ma","@mon"}, new object[] {ma,mon}).Rows[0][0]);
    }

    public void ThemMon(int ma,int mon,int sl,int gia)
    {
        ketNoi.Ghi("INSERT INTO chitiethoadon (mahd,mamon,soluong,dongia) VALUES (@ma,@mon,@sl,@gia) ON DUPLICATE KEY UPDATE soluong=soluong+@sl", new string[] {"@ma","@mon","@sl","@gia"}, new object[] {ma,mon,sl,gia});
    }

    public void SuaMon(int ma,int mon,int sl)
    {
        ketNoi.Ghi("UPDATE chitiethoadon SET soluong=@sl WHERE mahd=@ma AND mamon=@mon", new string[] {"@ma","@mon","@sl"}, new object[] {ma,mon,sl});
    }

    public void XoaMon(int ma,int mon)
    {
        ketNoi.Ghi("DELETE FROM chitiethoadon WHERE mahd=@ma AND mamon=@mon", new string[] {"@ma","@mon"}, new object[] {ma,mon});
    }

    public long TongChiTiet(int ma)
    {
        return Convert.ToInt64(ketNoi.Doc("SELECT COALESCE(SUM(soluong*dongia),0) FROM chitiethoadon WHERE mahd=@ma", new string[] {"@ma"}, new object[] {ma}).Rows[0][0]);
    }

    public void CapNhatTong(int ma,int tong)
    {
        ketNoi.Ghi("UPDATE hoadon SET tongtien=@tong WHERE mahd=@ma", new string[] {"@ma","@tong"}, new object[] {ma,tong});
    }

    public void ThanhToan(int ma,int tong)
    {
        ketNoi.Ghi("UPDATE hoadon SET tongtien=@tong,trangthai='Đã thanh toán',ngaythanhtoan=NOW() WHERE mahd=@ma", new string[] {"@ma","@tong"}, new object[] {ma,tong});
    }

    public void XoaChiTiet(int ma)
    {
        ketNoi.Ghi("DELETE FROM chitiethoadon WHERE mahd=@ma", new string[] {"@ma"}, new object[] {ma});
    }

    public void Xoa(int ma)
    {
        ketNoi.Ghi("DELETE FROM hoadon WHERE mahd=@ma", new string[] {"@ma"}, new object[] {ma});
    }
}
