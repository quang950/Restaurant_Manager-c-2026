using System.Collections;
using System.Data;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class HoaDonBUS
{
    public static DataTable DanhSach(string tim)
    {
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new HoaDonDAO(kn).DanhSach();
            ArrayList danhSach=new ArrayList();
            for (int i=0;i<bang.Rows.Count;i++) danhSach.Add(bang.Rows[i]);
            DataTable ketQua=bang.Clone();
            string tuKhoa=tim.Trim().ToLowerInvariant();
            string[] cotTim=new string[] {"mahd","tenban","hoten","trangthai" };
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
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection()) return new HoaDonDAO(kn).ThongTin(ma);
    }
    public static DataTable ChiTiet(int ma)
    {
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection()) return new HoaDonDAO(kn).ChiTiet(ma);
    }
    private static void KiemTraDangMo(HoaDonDAO dao,int ma)
    {
        DataTable hd=dao.Khoa(ma);
        if (hd.Rows.Count==0 || hd.Rows[0]["trangthai"].ToString()!="Chưa thanh toán")
            throw new InvalidOperationException("Hóa đơn đã thanh toán hoặc không còn tồn tại.");
    }
    public static int MoBan(int ban)
    {
        NhanVienBUS.YeuCauDangNhap();
        if (ban<=0) throw new InvalidOperationException("Chọn bàn trước khi mở hóa đơn.");
        using (MyConnection kn=new MyConnection())
        {
            kn.BatDauGiaoDich();
            HoaDonDAO hd=new HoaDonDAO(kn);
            if (new BanDAO(kn).Khoa(ban).Rows.Count==0) throw new InvalidOperationException("Bàn không còn tồn tại.");
            if (hd.DemDangMo(ban,0)>0) throw new InvalidOperationException("Bàn đang phục vụ. Hãy chọn hóa đơn hiện có.");
            int ma=hd.Tao(ban,NhanVienBUS.MaNhanVien);
            kn.ChapNhanGiaoDich(); return ma;
        }
    }
    public static void SuaBan(int ma,int ban)
    {
        NhanVienBUS.YeuCauDangNhap();
        if (ma<=0 || ban<=0) throw new InvalidOperationException("Chọn hóa đơn và bàn muốn sửa.");
        using (MyConnection kn=new MyConnection())
        {
            kn.BatDauGiaoDich();
            HoaDonDAO hd=new HoaDonDAO(kn);
            KiemTraDangMo(hd,ma);
            if (new BanDAO(kn).Khoa(ban).Rows.Count==0) throw new InvalidOperationException("Bàn không còn tồn tại.");
            if (hd.DemDangMo(ban,ma)>0) throw new InvalidOperationException("Bàn được chọn đang phục vụ hóa đơn khác.");
            hd.SuaBan(ma,ban); kn.ChapNhanGiaoDich();
        }
    }
    public static void LuuChiTiet(int ma,int mon,int soLuong,string thaoTac)
    {
        NhanVienBUS.YeuCauDangNhap();
        if (ma<=0) throw new InvalidOperationException("Chọn hóa đơn trước.");
        if (mon<=0) throw new InvalidOperationException("Chọn món trong bảng chi tiết hoặc danh mục trước.");
        if (thaoTac!="Them" && thaoTac!="Sua" && thaoTac!="Xoa") throw new InvalidOperationException("Thao tác không hợp lệ.");
        if (thaoTac!="Xoa" && (soLuong<1 || soLuong>99999)) throw new InvalidOperationException("Số lượng phải từ 1 đến 99.999.");
        using (MyConnection kn=new MyConnection())
        {
            kn.BatDauGiaoDich();
            HoaDonDAO hd=new HoaDonDAO(kn);
            MonNuocDAO monDao=new MonNuocDAO(kn);
            KiemTraDangMo(hd,ma);
            DataTable bang=monDao.Khoa(mon);
            if (bang.Rows.Count==0) throw new InvalidOperationException("Món không còn tồn tại.");
            int gia=Convert.ToInt32(bang.Rows[0]["gia"]), ton=Convert.ToInt32(bang.Rows[0]["soluongton"]);
            int slCu=hd.SoLuong(ma,mon), slMoi=0;
            if (thaoTac!="Them" && slCu==0) throw new InvalidOperationException("Dòng món đã thay đổi. Hãy tải lại.");
            if (thaoTac=="Them") slMoi=slCu+soLuong;
            else if (thaoTac=="Sua") slMoi=soLuong;
            if (slMoi>99999) throw new InvalidOperationException("Một món có tối đa 99.999 phần trên hóa đơn.");
            int chenhLech=slMoi-slCu;
            if (chenhLech>ton) throw new InvalidOperationException(bang.Rows[0]["tenmon"]+": số lượng thêm vượt quá tồn. Còn "+ton+", cần thêm "+chenhLech+".");
            long tonMoi=(long)ton-chenhLech;
            if (tonMoi>int.MaxValue) throw new InvalidOperationException("Không thể hoàn hàng vì số lượng tồn vượt giới hạn int.");
            monDao.CapNhatTon(mon,(int)tonMoi);
            if (thaoTac=="Them") hd.ThemMon(ma,mon,soLuong,gia);
            else if (thaoTac=="Sua") hd.SuaMon(ma,mon,soLuong);
            else hd.XoaMon(ma,mon);
            long tong=hd.TongChiTiet(ma);
            if (tong>int.MaxValue) throw new InvalidOperationException("Tổng tiền vượt giới hạn int (2.147.483.647 đ). Hãy giảm số lượng.");
            hd.CapNhatTong(ma,(int)tong);
            kn.ChapNhanGiaoDich();
        }
    }
    public static void ThanhToan(int ma)
    {
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection())
        {
            kn.BatDauGiaoDich(); HoaDonDAO hd=new HoaDonDAO(kn);
            KiemTraDangMo(hd,ma);
            long tong=hd.TongChiTiet(ma);
            if (tong<=0) throw new InvalidOperationException("Không thể thanh toán hóa đơn chưa có món.");
            if (tong>int.MaxValue) throw new InvalidOperationException("Tổng tiền vượt giới hạn int.");
            // Tồn đã trừ lúc gọi món, ở đây chỉ chốt hóa đơn.
            hd.ThanhToan(ma,(int)tong); kn.ChapNhanGiaoDich();
        }
    }
    public static void Xoa(int ma)
    {
        NhanVienBUS.YeuCauDangNhap();
        using (MyConnection kn=new MyConnection())
        {
            kn.BatDauGiaoDich(); HoaDonDAO hd=new HoaDonDAO(kn); MonNuocDAO mon=new MonNuocDAO(kn);
            KiemTraDangMo(hd,ma);
            DataTable chiTiet=hd.ChiTiet(ma);
            for (int i=0;i<chiTiet.Rows.Count;i++)
            {
                int maMon=Convert.ToInt32(chiTiet.Rows[i]["mamon"]);
                DataTable bang=mon.Khoa(maMon);
                long tonMoi=Convert.ToInt64(bang.Rows[0]["soluongton"])+Convert.ToInt32(chiTiet.Rows[i]["soluong"]);
                if (tonMoi>int.MaxValue) throw new InvalidOperationException("Không thể hoàn hàng vì số lượng tồn vượt giới hạn int.");
                mon.CapNhatTon(maMon,(int)tonMoi);
            }
            hd.XoaChiTiet(ma); hd.Xoa(ma); kn.ChapNhanGiaoDich();
        }
    }
}
