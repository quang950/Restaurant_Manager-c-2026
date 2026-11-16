using System.Collections;
using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class NhapHangBUS
{
    public static DataTable DanhSach(string tim)
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new NhapHangDAO(kn).DanhSach();
            ArrayList danhSach=new ArrayList();
            for (int i=0;i<bang.Rows.Count;i++) danhSach.Add(bang.Rows[i]);
            DataTable ketQua=bang.Clone();
            string tuKhoa=tim.Trim().ToLowerInvariant();
            string[] cotTim=new string[] {"mapn","tenncc" };
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
    public static DataTable ThongTin(int ma)
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection()) return new NhapHangDAO(kn).ThongTin(ma);
    }
    public static DataTable ChiTiet(int ma)
    {
        NhanVienBUS.YeuCauQuanLy();
        using (MyConnection kn=new MyConnection()) return new NhapHangDAO(kn).ChiTiet(ma);
    }
    public static DataTable TaoBangNhap()
    {
        DataTable bang=new DataTable();
        bang.Columns.Add("mamon",typeof(int)); bang.Columns.Add("tenmon",typeof(string));
        bang.Columns.Add("soluong",typeof(int)); bang.Columns.Add("dongianhap",typeof(int)); bang.Columns.Add("thanhtien",typeof(long));
        return bang;
    }
    public static long TongTien(DataTable bang)
    {
        long tong=0;
        for (int i=0;i<bang.Rows.Count;i++) tong+=(long)Convert.ToInt32(bang.Rows[i]["soluong"])*Convert.ToInt32(bang.Rows[i]["dongianhap"]);
        return tong;
    }
    public static void ThemSuaDong(DataTable bang,int ma,string ten,int sl,int gia,bool them)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn món cần nhập.");
        if (sl<1 || sl>99999 || gia<1 || gia>2000000000) throw new InvalidOperationException("Số lượng hoặc đơn giá nhập không hợp lệ.");
        int viTri=-1;
        for (int i=0;i<bang.Rows.Count;i++) if (Convert.ToInt32(bang.Rows[i]["mamon"])==ma) { viTri=i; break; }
        if (them && viTri>=0) throw new InvalidOperationException("Món đã có trong phiếu. Chọn dòng rồi bấm Sửa dòng.");
        if (!them && viTri<0) throw new InvalidOperationException("Chọn dòng cần sửa số lượng hoặc đơn giá.");
        long tien=(long)sl*gia, tong=TongTien(bang)+tien;
        if (viTri>=0) tong-=(long)Convert.ToInt32(bang.Rows[viTri]["soluong"])*Convert.ToInt32(bang.Rows[viTri]["dongianhap"]);
        if (tong>int.MaxValue) throw new InvalidOperationException("Tổng phiếu nhập vượt giới hạn int (2.147.483.647 đ).");
        if (them) bang.Rows.Add(ma,ten,sl,gia,tien);
        else { bang.Rows[viTri]["soluong"]=sl; bang.Rows[viTri]["dongianhap"]=gia; bang.Rows[viTri]["thanhtien"]=tien; }
    }
    public static void XoaDong(DataTable bang,int ma)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn dòng cần xóa khỏi phiếu chưa ghi nhận.");
        for (int i=0;i<bang.Rows.Count;i++) if (Convert.ToInt32(bang.Rows[i]["mamon"])==ma) { bang.Rows.RemoveAt(i); return; }
        throw new InvalidOperationException("Dòng không còn tồn tại.");
    }
    public static int GhiNhan(int ncc,DateTime ngay,string xacNhan,DataTable bang)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (ncc<=0) throw new InvalidOperationException("Chọn nhà cung cấp trước.");
        if (bang.Rows.Count==0) throw new InvalidOperationException("Cần thêm ít nhất một dòng món/nước.");
        if (ngay>DateTime.Now.AddMinutes(1)) throw new InvalidOperationException("Ngày nhập không được ở tương lai.");
        if (xacNhan.Length!=32) throw new InvalidOperationException("Mã xác nhận phiếu không hợp lệ.");
        long tong=TongTien(bang);
        if (tong<=0 || tong>int.MaxValue) throw new InvalidOperationException("Tổng phiếu nhập không hợp lệ hoặc vượt giới hạn int.");
        for (int i=0;i<bang.Rows.Count;i++)
        {
            int sl=Convert.ToInt32(bang.Rows[i]["soluong"]), gia=Convert.ToInt32(bang.Rows[i]["dongianhap"]);
            if (sl<1 || sl>99999 || gia<1 || gia>2000000000) throw new InvalidOperationException("Dòng nhập có số lượng hoặc đơn giá không hợp lệ.");
        }
        try
        {
            using (MyConnection kn=new MyConnection())
            {
                kn.BatDauGiaoDich(); NhapHangDAO phieu=new NhapHangDAO(kn); MonNuocDAO mon=new MonNuocDAO(kn);
                DataTable cu=phieu.TheoXacNhan(xacNhan);
                if (cu.Rows.Count>0) return Convert.ToInt32(cu.Rows[0]["mapn"]);
                int ma=phieu.Them(ncc,NhanVienBUS.MaNhanVien,ngay,(int)tong,xacNhan);
                for (int i=0;i<bang.Rows.Count;i++)
                {
                    int maMon=Convert.ToInt32(bang.Rows[i]["mamon"]), sl=Convert.ToInt32(bang.Rows[i]["soluong"]);
                    DataTable thongTin=mon.Khoa(maMon);
                    if (thongTin.Rows.Count==0) throw new InvalidOperationException("Một món không còn tồn tại. Hãy tạo lại phiếu.");
                    long tonMoi=Convert.ToInt64(thongTin.Rows[0]["soluongton"])+sl;
                    if (tonMoi>int.MaxValue) throw new InvalidOperationException("Tồn kho vượt giới hạn int; phiếu chưa được lưu.");
                    mon.CapNhatTon(maMon,(int)tonMoi);
                    phieu.ThemChiTiet(ma,maMon,sl,Convert.ToInt32(bang.Rows[i]["dongianhap"]));
                }
                kn.ChapNhanGiaoDich(); return ma;
            }
        }
        catch (InvalidOperationException)
        {
            // Nếu lần lưu trước đã commit nhưng mất phản hồi, lấy lại phiếu cũ, không nhập lặp.
            using (MyConnection kn=new MyConnection())
            {
                DataTable cu=new NhapHangDAO(kn).TheoXacNhan(xacNhan);
                if (cu.Rows.Count>0) return Convert.ToInt32(cu.Rows[0]["mapn"]);
            }
            throw;
        }
    }
}
