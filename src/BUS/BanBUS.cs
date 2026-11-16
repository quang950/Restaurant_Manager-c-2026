using System.Collections;
using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class BanBUS
{
    public static DataTable DanhSach(string tim)
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new BanDAO(kn).DanhSach();
            ArrayList danhSach=new ArrayList();
            for (int i=0;i<bang.Rows.Count;i++) danhSach.Add(bang.Rows[i]);
            DataTable ketQua=bang.Clone();
            string tuKhoa=tim.Trim().ToLowerInvariant();
            string[] cotTim=new string[] {"maban","tenban" };
            for (int i=0;i<danhSach.Count;i++)
            {
                DataRow dong=(DataRow)danhSach[i];
                bool timThay=false;
                for (int j=0;j<cotTim.Length;j++)
                    if (dong[cotTim[j]].ToString().ToLowerInvariant().Contains(tuKhoa)) { timThay=true; break; }
                if (timThay) ketQua.ImportRow(dong);
            }
            return ketQua;
        }
    }
    public static DataTable GoiMon()
    {
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection()) return new BanDAO(kn).GoiMon();
    }
    internal static void KiemTra(string ten,int soCho)
    {
        if (ten.Trim()=="" || ten.Trim().Length>50) throw new InvalidOperationException("Vui lòng nhập tên bàn, tối đa 50 ký tự.");
        if (soCho<1 || soCho>100) throw new InvalidOperationException("Số chỗ cần từ 1 đến 100.");
    }
    public static void Them(string ten,int soCho)
    {
        NhanVienBUS.YeuCauQuanLy(); KiemTra(ten,soCho);
        using (MyConnection kn=new MyConnection()) new BanDAO(kn).Them(ten.Trim(),soCho);
    }
    public static void Sua(int ma,string ten,int soCho)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần sửa.");
        KiemTra(ten,soCho);
        using (MyConnection kn=new MyConnection())
            if (new BanDAO(kn).Sua(ma,ten.Trim(),soCho)==0) throw new InvalidOperationException("Bàn không còn tồn tại.");
    }
    public static void Xoa(int ma)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần xóa.");
        using (MyConnection kn=new MyConnection())
            if (new BanDAO(kn).Xoa(ma)==0) throw new InvalidOperationException("Bàn không còn tồn tại.");
    }
}
