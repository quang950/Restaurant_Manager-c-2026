using System.Collections;
using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class MonNuocBUS
{
    public static DataTable DanhSach(string tim)
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new MonNuocDAO(kn).DanhSach();
            ArrayList danhSach=new ArrayList();
            for (int i=0;i<bang.Rows.Count;i++) danhSach.Add(bang.Rows[i]);
            DataTable ketQua=bang.Clone();
            string tuKhoa=tim.Trim().ToLowerInvariant();
            string[] cotTim=new string[] {"mamon","tenmon","loai","donvi" };
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
        using (MyConnection kn=new MyConnection()) return new MonNuocDAO(kn).GoiMon();
    }
    public static DataTable NhapHang()
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection()) return new MonNuocDAO(kn).NhapHang();
    }
    internal static void KiemTra(string ten,string loai,string donvi,int gia)
    {
        if (ten.Trim()=="" || ten.Trim().Length>100) throw new InvalidOperationException("Vui lòng nhập tên món / nước, tối đa 100 ký tự.");
        if (loai!="Món ăn" && loai!="Nước uống") throw new InvalidOperationException("Loại món/nước không hợp lệ.");
        if (donvi.Trim()=="" || donvi.Trim().Length>30) throw new InvalidOperationException("Vui lòng nhập đơn vị, tối đa 30 ký tự.");
        if (gia<=0 || gia>2000000000) throw new InvalidOperationException("Giá phải từ 1 đến 2.000.000.000 đ.");
    }
    public static void Them(string ten,string loai,string donvi,int gia)
    {
        NhanVienBUS.YeuCauQuanLy(); KiemTra(ten,loai,donvi,gia);
        using (MyConnection kn=new MyConnection()) new MonNuocDAO(kn).Them(ten.Trim(),loai,donvi.Trim(),gia);
    }
    public static void Sua(int ma,string ten,string loai,string donvi,int gia)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần sửa.");
        KiemTra(ten,loai,donvi,gia);
        using (MyConnection kn=new MyConnection())
            if (new MonNuocDAO(kn).Sua(ma,ten.Trim(),loai,donvi.Trim(),gia)==0) throw new InvalidOperationException("Món không còn tồn tại.");
    }
    public static void Xoa(int ma)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần xóa.");
        using (MyConnection kn=new MyConnection())
            if (new MonNuocDAO(kn).Xoa(ma)==0) throw new InvalidOperationException("Món không còn tồn tại.");
    }
}
