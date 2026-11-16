using System.Data;
namespace QuanLyNhaHang.DAO;

public class MonNuocDAO
{
    private MyConnection ketNoi;
    public MonNuocDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public DataTable DanhSach()
    {
        return ketNoi.Doc("SELECT mamon,tenmon,loai,donvi,gia,soluongton FROM monnuoc ORDER BY mamon");
    }

    public DataTable GoiMon()
    {
        return ketNoi.Doc("SELECT mamon,CONCAT(tenmon,' · ',gia,' đ / ',donvi,' · tồn: ',soluongton) AS hienthi FROM monnuoc ORDER BY tenmon");
    }

    public DataTable NhapHang()
    {
        return ketNoi.Doc("SELECT mamon,tenmon,CONCAT(tenmon,' · tồn: ',soluongton,' ',donvi) AS hienthi FROM monnuoc ORDER BY tenmon");
    }

    public DataTable Khoa(int ma)
    {
        return ketNoi.Doc("SELECT mamon,tenmon,gia,soluongton FROM monnuoc WHERE mamon=@ma FOR UPDATE", new string[] {"@ma"}, new object[] {ma});
    }

    public void CapNhatTon(int ma,int ton)
    {
        ketNoi.Ghi("UPDATE monnuoc SET soluongton=@ton WHERE mamon=@ma", new string[] {"@ma","@ton"}, new object[] {ma,ton});
    }

    public void Them(string ten,string loai,string donvi,int gia)
    {
        ketNoi.Ghi("INSERT INTO monnuoc (tenmon,loai,donvi,gia) VALUES (@ten,@loai,@donvi,@gia)", new string[] {"@ten","@loai","@donvi","@gia"}, new object[] {ten,loai,donvi,gia});
    }

    public int Sua(int ma,string ten,string loai,string donvi,int gia)
    {
        return ketNoi.Ghi("UPDATE monnuoc SET tenmon=@ten,loai=@loai,donvi=@donvi,gia=@gia WHERE mamon=@ma", new string[] {"@ma","@ten","@loai","@donvi","@gia"}, new object[] {ma,ten,loai,donvi,gia});
    }

    public int Xoa(int ma)
    {
        return ketNoi.Ghi("DELETE FROM monnuoc WHERE mamon=@ma", new string[] {"@ma"}, new object[] {ma});
    }
}
