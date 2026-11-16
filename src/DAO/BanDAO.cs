using System.Data;
namespace QuanLyNhaHang.DAO;

public class BanDAO
{
    private MyConnection ketNoi;
    public BanDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public DataTable DanhSach()
    {
        return ketNoi.Doc("SELECT b.maban,b.tenban,b.socho,CASE WHEN EXISTS (SELECT 1 FROM hoadon h WHERE h.maban=b.maban AND h.trangthai='Chưa thanh toán') THEN 'Đang phục vụ' ELSE 'Trống' END AS trangthai FROM ban b ORDER BY b.maban");
    }

    public DataTable GoiMon()
    {
        return ketNoi.Doc("SELECT b.maban,CONCAT(b.tenban,' · ',b.socho,' chỗ · ',CASE WHEN EXISTS (SELECT 1 FROM hoadon h WHERE h.maban=b.maban AND h.trangthai='Chưa thanh toán') THEN 'Đang phục vụ' ELSE 'Trống' END) AS hienthi FROM ban b ORDER BY b.maban");
    }

    public DataTable Khoa(int ma)
    {
        return ketNoi.Doc("SELECT maban FROM ban WHERE maban=@ma FOR UPDATE", new string[] {"@ma"}, new object[] {ma});
    }

    public void Them(string ten,int soCho)
    {
        ketNoi.Ghi("INSERT INTO ban (tenban,socho) VALUES (@ten,@soCho)", new string[] {"@ten","@soCho"}, new object[] {ten,soCho});
    }

    public int Sua(int ma,string ten,int soCho)
    {
        return ketNoi.Ghi("UPDATE ban SET tenban=@ten,socho=@soCho WHERE maban=@ma", new string[] {"@ma","@ten","@soCho"}, new object[] {ma,ten,soCho});
    }

    public int Xoa(int ma)
    {
        return ketNoi.Ghi("DELETE FROM ban WHERE maban=@ma", new string[] {"@ma"}, new object[] {ma});
    }
}
