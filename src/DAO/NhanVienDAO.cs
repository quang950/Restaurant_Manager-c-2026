using System.Data;
namespace QuanLyNhaHang.DAO;

public class NhanVienDAO
{
    private MyConnection ketNoi;
    public NhanVienDAO(MyConnection ketNoi) { this.ketNoi=ketNoi; }

    public DataTable DanhSach()
    {
        return ketNoi.Doc("SELECT manv,hoten,sdt,tendangnhap,vaitro FROM nhanvien ORDER BY manv");
    }

    public DataTable TheoTen(string ten)
    {
        return ketNoi.Doc("SELECT * FROM nhanvien WHERE tendangnhap=@ten", new string[] {"@ten"}, new object[] {ten});
    }

    public DataTable TheoMa(int ma)
    {
        return ketNoi.Doc("SELECT * FROM nhanvien WHERE manv=@ma", new string[] {"@ma"}, new object[] {ma});
    }

    public void Them(string hoten,string sdt,string ten,string vaiTro,string matKhau)
    {
        ketNoi.Ghi("INSERT INTO nhanvien (hoten,sdt,tendangnhap,vaitro,matkhau) VALUES (@hoten,@sdt,@ten,@vaiTro,@matKhau)", new string[] {"@hoten","@sdt","@ten","@vaiTro","@matKhau"}, new object[] {hoten,sdt,ten,vaiTro,matKhau});
    }

    public int Sua(int ma,string hoten,string sdt,string ten,string vaiTro)
    {
        return ketNoi.Ghi("UPDATE nhanvien SET hoten=@hoten,sdt=@sdt,tendangnhap=@ten,vaitro=@vaiTro WHERE manv=@ma", new string[] {"@ma","@hoten","@sdt","@ten","@vaiTro"}, new object[] {ma,hoten,sdt,ten,vaiTro});
    }

    public int Xoa(int ma)
    {
        return ketNoi.Ghi("DELETE FROM nhanvien WHERE manv=@ma", new string[] {"@ma"}, new object[] {ma});
    }

    public int DoiMatKhau(int ma,string cu,string moi)
    {
        return ketNoi.Ghi("UPDATE nhanvien SET matkhau=@moi WHERE manv=@ma AND matkhau=@cu", new string[] {"@ma","@cu","@moi"}, new object[] {ma,cu,moi});
    }
}
