using System.Data;
namespace QuanLyNhaHang.DAO;

public class NhaCungCapDAO
{
    private MyConnection ketNoi;
    public NhaCungCapDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public DataTable DanhSach()
    {
        return ketNoi.Doc("SELECT mancc,tenncc,sdt,diachi FROM nhacungcap ORDER BY mancc");
    }

    public DataTable NhapHang()
    {
        return ketNoi.Doc("SELECT mancc,tenncc FROM nhacungcap ORDER BY tenncc");
    }

    public void Them(string ten,string sdt,string diaChi)
    {
        ketNoi.Ghi("INSERT INTO nhacungcap (tenncc,sdt,diachi) VALUES (@ten,@sdt,@diaChi)", new string[] {"@ten","@sdt","@diaChi"}, new object[] {ten,sdt,diaChi});
    }

    public int Sua(int ma,string ten,string sdt,string diaChi)
    {
        return ketNoi.Ghi("UPDATE nhacungcap SET tenncc=@ten,sdt=@sdt,diachi=@diaChi WHERE mancc=@ma", new string[] {"@ma","@ten","@sdt","@diaChi"}, new object[] {ma,ten,sdt,diaChi});
    }

    public int Xoa(int ma)
    {
        return ketNoi.Ghi("DELETE FROM nhacungcap WHERE mancc=@ma", new string[] {"@ma"}, new object[] {ma});
    }
}
