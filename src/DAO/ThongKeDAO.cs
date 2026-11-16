using System.Data;
namespace QuanLyNhaHang.DAO;

public class ThongKeDAO
{
    private MyConnection ketNoi;
    public ThongKeDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public int BanTrong()
    {
        return Convert.ToInt32(ketNoi.Doc("SELECT COUNT(*) FROM ban WHERE maban NOT IN (SELECT maban FROM hoadon WHERE trangthai='Chưa thanh toán')").Rows[0][0]);
    }

    public int DangPhucVu()
    {
        return Convert.ToInt32(ketNoi.Doc("SELECT COUNT(*) FROM hoadon WHERE trangthai='Chưa thanh toán'").Rows[0][0]);
    }

    public long DoanhThuHomNay()
    {
        return Convert.ToInt64(ketNoi.Doc("SELECT COALESCE(SUM(tongtien),0) FROM hoadon WHERE trangthai='Đã thanh toán' AND ngaythanhtoan>=CURRENT_DATE AND ngaythanhtoan<CURRENT_DATE+INTERVAL 1 DAY").Rows[0][0]);
    }

    public DataTable DoanhThu(DateTime tu,DateTime den,string dinhDang)
    {
        return ketNoi.Doc("SELECT ky,SUM(sohoadon) AS sohoadon,SUM(doanhthu) AS doanhthu,SUM(tongchi) AS tongchi FROM (SELECT DATE_FORMAT(ngaythanhtoan,@dinhDang) AS ky,COUNT(*) AS sohoadon,SUM(tongtien) AS doanhthu,0 AS tongchi FROM hoadon WHERE trangthai='Đã thanh toán' AND ngaythanhtoan>=@tu AND ngaythanhtoan<@den GROUP BY ky UNION ALL SELECT DATE_FORMAT(ngaynhap,@dinhDang) AS ky,0 AS sohoadon,0 AS doanhthu,SUM(tongtien) AS tongchi FROM phieunhap WHERE ngaynhap>=@tu AND ngaynhap<@den GROUP BY ky) t GROUP BY ky ORDER BY ky",new string[]{"@tu","@den","@dinhDang"},new object[]{tu,den,dinhDang});
    }

    public DataTable MonBanChay(DateTime tu,DateTime den)
    {
        return ketNoi.Doc("SELECT m.tenmon,SUM(c.soluong) AS soluong,SUM(c.soluong*c.dongia) AS thanhtien FROM chitiethoadon c JOIN monnuoc m ON c.mamon=m.mamon JOIN hoadon h ON c.mahd=h.mahd WHERE h.trangthai='Đã thanh toán' AND h.ngaythanhtoan>=@tu AND h.ngaythanhtoan<@den GROUP BY m.mamon,m.tenmon ORDER BY soluong DESC,thanhtien DESC,m.mamon", new string[] {"@tu","@den"}, new object[] {tu,den});
    }
    public DataTable NhanVienQuy(int nam)
    {
        DateTime tu=new DateTime(nam,1,1), den=tu.AddYears(1);
        return ketNoi.Doc("SELECT n.manv AS ma,n.hoten AS ten,COALESCE(QUARTER(h.ngaythanhtoan),0) AS quy,COALESCE(h.tongtien,0) AS sotien FROM nhanvien n LEFT JOIN hoadon h ON n.manv=h.manv AND h.trangthai='Đã thanh toán' AND h.ngaythanhtoan>=@tu AND h.ngaythanhtoan<@den ORDER BY n.manv,h.mahd",new string[]{"@tu","@den"},new object[]{tu,den});
    }
    public DataTable SanPhamQuy(int nam)
    {
        DateTime tu=new DateTime(nam,1,1), den=tu.AddYears(1);
        return ketNoi.Doc("SELECT m.mamon AS ma,m.tenmon AS ten,COALESCE(x.quy,0) AS quy,COALESCE(x.sotien,0) AS sotien FROM monnuoc m LEFT JOIN (SELECT c.mamon,QUARTER(h.ngaythanhtoan) AS quy,c.soluong*c.dongia AS sotien FROM chitiethoadon c JOIN hoadon h ON c.mahd=h.mahd WHERE h.trangthai='Đã thanh toán' AND h.ngaythanhtoan>=@tu AND h.ngaythanhtoan<@den) x ON m.mamon=x.mamon ORDER BY m.mamon",new string[]{"@tu","@den"},new object[]{tu,den});
    }
}
