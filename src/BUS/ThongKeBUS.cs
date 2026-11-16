using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class ThongKeBUS
{
    public static DataTable TongQuan()
    {
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection())
        {
            ThongKeDAO dao=new ThongKeDAO(kn);
            DataTable bang=new DataTable();
            bang.Columns.Add("bantrong",typeof(int)); bang.Columns.Add("dangphucvu",typeof(int)); bang.Columns.Add("doanhthu",typeof(long));
            long tien=0;
            if (NhanVienBUS.VaiTro=="Quản lý") tien=dao.DoanhThuHomNay();
            bang.Rows.Add(dao.BanTrong(),dao.DangPhucVu(),tien); return bang;
        }
    }
    private static void KiemTraNgay(DateTime tu,DateTime den)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (tu.Date>den.Date) throw new InvalidOperationException("Ngày bắt đầu không được sau ngày kết thúc.");
        if (den.Date==DateTime.MaxValue.Date) throw new InvalidOperationException("Ngày kết thúc không hợp lệ.");
    }
    public static DataTable DoanhThu(DateTime tu,DateTime den,bool theoThang)
    {
        KiemTraNgay(tu,den);
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new ThongKeDAO(kn).DoanhThu(tu.Date,den.Date.AddDays(1),theoThang?"%Y-%m":"%Y-%m-%d");
            bang.Columns.Add("loinhuan",typeof(long));
            for (int i=0;i<bang.Rows.Count;i++) bang.Rows[i]["loinhuan"]=Convert.ToInt64(bang.Rows[i]["doanhthu"])-Convert.ToInt64(bang.Rows[i]["tongchi"]);
            return bang;
        }
    }
    public static DataTable MonBanChay(DateTime tu,DateTime den)
    {
        KiemTraNgay(tu,den);
        using (MyConnection kn=new MyConnection()) return new ThongKeDAO(kn).MonBanChay(tu.Date,den.Date.AddDays(1));
    }
    public static long TongDoanhThu(DataTable bang)
    {
        long tong=0;
        for (int i=0;i<bang.Rows.Count;i++) tong+=Convert.ToInt64(bang.Rows[i]["doanhthu"]);
        return tong;
    }
    public static long SoHoaDon(DataTable bang)
    {
        long tong=0;
        for (int i=0;i<bang.Rows.Count;i++) tong+=Convert.ToInt64(bang.Rows[i]["sohoadon"]);
        return tong;
    }
    public static DataTable NhanVienQuy(int nam)
    {
        KiemTraNam(nam);
        using (MyConnection kn=new MyConnection()) return LapBangQuy(new ThongKeDAO(kn).NhanVienQuy(nam));
    }
    public static DataTable SanPhamQuy(int nam)
    {
        KiemTraNam(nam);
        using (MyConnection kn=new MyConnection()) return LapBangQuy(new ThongKeDAO(kn).SanPhamQuy(nam));
    }
    private static void KiemTraNam(int nam)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (nam<1 || nam>9998) throw new InvalidOperationException("Năm phải từ 1 đến 9998.");
    }
    private static DataTable LapBangQuy(DataTable nguon)
    {
        DataTable bang=new DataTable();
        bang.Columns.Add("ma",typeof(int)); bang.Columns.Add("ten",typeof(string));
        for (int quy=1;quy<=4;quy++) bang.Columns.Add("quy"+quy,typeof(long));
        bang.Columns.Add("tong",typeof(long));
        for (int i=0;i<nguon.Rows.Count;i++)
        {
            int ma=Convert.ToInt32(nguon.Rows[i]["ma"]), viTri=-1;
            for (int j=0;j<bang.Rows.Count;j++)
                if (Convert.ToInt32(bang.Rows[j]["ma"])==ma) { viTri=j; break; }
            if (viTri<0) { bang.Rows.Add(ma,nguon.Rows[i]["ten"].ToString(),0L,0L,0L,0L,0L); viTri=bang.Rows.Count-1; }
            int quy=Convert.ToInt32(nguon.Rows[i]["quy"]);
            if (quy>=1 && quy<=4)
            {
                long tien=Convert.ToInt64(nguon.Rows[i]["sotien"]);
                bang.Rows[viTri]["quy"+quy]=Convert.ToInt64(bang.Rows[viTri]["quy"+quy])+tien;
                bang.Rows[viTri]["tong"]=Convert.ToInt64(bang.Rows[viTri]["tong"])+tien;
            }
        }
        DataRow tong=bang.NewRow(); tong["ma"]=0; tong["ten"]="Tổng"; tong["tong"]=0L;
        for (int quy=1;quy<=4;quy++)
        {
            long tien=0;
            for (int i=0;i<bang.Rows.Count;i++) tien+=Convert.ToInt64(bang.Rows[i]["quy"+quy]);
            tong["quy"+quy]=tien; tong["tong"]=Convert.ToInt64(tong["tong"])+tien;
        }
        bang.Rows.Add(tong); return bang;
    }
    public static long TongChi(DataTable bang)
    {
        long tong=0;
        for (int i=0;i<bang.Rows.Count;i++) tong+=Convert.ToInt64(bang.Rows[i]["tongchi"]);
        return tong;
    }
}
