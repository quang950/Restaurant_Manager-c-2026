using System.Collections;
using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class NhaCungCapBUS
{
    public static DataTable DanhSach(string tim)
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new NhaCungCapDAO(kn).DanhSach();
            ArrayList danhSach=new ArrayList();
            for (int i=0;i<bang.Rows.Count;i++) danhSach.Add(bang.Rows[i]);
            DataTable ketQua=bang.Clone();
            string tuKhoa=tim.Trim().ToLowerInvariant();
            string[] cotTim=new string[] {"mancc","tenncc","sdt","diachi" };
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
    public static DataTable NhapHang()
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection()) return new NhaCungCapDAO(kn).NhapHang();
    }
    internal static void KiemTra(string ten,string sdt,string diaChi)
    {
        if (ten.Trim()=="" || ten.Trim().Length>100) throw new InvalidOperationException("Vui lòng nhập tên nhà cung cấp, tối đa 100 ký tự.");
        NhanVienBUS.KiemTraDienThoai(sdt);
        if (diaChi.Trim()=="" || diaChi.Trim().Length>250) throw new InvalidOperationException("Vui lòng nhập địa chỉ, tối đa 250 ký tự.");
    }
    public static void Them(string ten,string sdt,string diaChi)
    {
        NhanVienBUS.YeuCauQuanLy(); KiemTra(ten,sdt,diaChi);
        using (MyConnection kn=new MyConnection()) new NhaCungCapDAO(kn).Them(ten.Trim(),sdt.Trim(),diaChi.Trim());
    }
    public static void Sua(int ma,string ten,string sdt,string diaChi)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần sửa.");
        KiemTra(ten,sdt,diaChi);
        using (MyConnection kn=new MyConnection())
            if (new NhaCungCapDAO(kn).Sua(ma,ten.Trim(),sdt.Trim(),diaChi.Trim())==0) throw new InvalidOperationException("Nhà cung cấp không còn tồn tại.");
    }
    public static void Xoa(int ma)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần xóa.");
        using (MyConnection kn=new MyConnection())
            if (new NhaCungCapDAO(kn).Xoa(ma)==0) throw new InvalidOperationException("Nhà cung cấp không còn tồn tại.");
    }
}
