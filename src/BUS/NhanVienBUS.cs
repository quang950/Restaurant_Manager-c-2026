using System.Collections;
using System.Data;
using QuanLyNhaHang.DAO;
using System.Security.Cryptography;
namespace QuanLyNhaHang.BUS;

public static class NhanVienBUS
{
    public static bool SuDungPhienAsync { get; set; } = false;
    private static int _maNhanVien;
    private static string _tenNhanVien="";
    private static string _vaiTro="";
    private static readonly AsyncLocal<int> _asyncMaNhanVien = new();
    private static readonly AsyncLocal<string> _asyncTenNhanVien = new();
    private static readonly AsyncLocal<string> _asyncVaiTro = new();

    public static int MaNhanVien
    {
        get => SuDungPhienAsync ? _asyncMaNhanVien.Value : _maNhanVien;
        set { if (SuDungPhienAsync) _asyncMaNhanVien.Value = value; else _maNhanVien = value; }
    }
    public static string TenNhanVien
    {
        get => SuDungPhienAsync ? (_asyncTenNhanVien.Value ?? "") : _tenNhanVien;
        set { if (SuDungPhienAsync) _asyncTenNhanVien.Value = value ?? ""; else _tenNhanVien = value ?? ""; }
    }
    public static string VaiTro
    {
        get => SuDungPhienAsync ? (_asyncVaiTro.Value ?? "") : _vaiTro;
        set { if (SuDungPhienAsync) _asyncVaiTro.Value = value ?? ""; else _vaiTro = value ?? ""; }
    }
    public static void DatPhien(int ma, string ten, string vaiTro)
    {
        MaNhanVien = ma; TenNhanVien = ten; VaiTro = vaiTro;
    }
    public static void YeuCauDangNhap()
    {
        if (MaNhanVien<=0) throw new InvalidOperationException("Bạn cần đăng nhập trước.");
    }
    public static void YeuCauQuanLy()
    {
        YeuCauDangNhap();
        if (VaiTro!="Quản lý") throw new InvalidOperationException("Chức năng này chỉ dành cho quản lý.");
    }
    public static void DangXuat() { MaNhanVien=0; TenNhanVien=""; VaiTro=""; }
    public static void DangNhap(string ten,string matKhau)
    {
        DangXuat();
        if (ten.Trim()=="" || matKhau=="") throw new InvalidOperationException("Vui lòng nhập tài khoản và mật khẩu.");
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new NhanVienDAO(kn).TheoTen(ten.Trim());
            if (bang.Rows.Count==0 || !KiemTraMatKhau(matKhau,bang.Rows[0]["matkhau"].ToString()))
                throw new InvalidOperationException("Tài khoản hoặc mật khẩu không đúng.");
            MaNhanVien=Convert.ToInt32(bang.Rows[0]["manv"]);
            TenNhanVien=bang.Rows[0]["hoten"].ToString();
            VaiTro=bang.Rows[0]["vaitro"].ToString();
        }
    }
    public static DataTable DanhSach(string tim)
    {
        YeuCauQuanLy();
        using (MyConnection kn=new MyConnection())
        {
            DataTable bang=new NhanVienDAO(kn).DanhSach();
            ArrayList danhSach=new ArrayList();
            for (int i=0;i<bang.Rows.Count;i++) danhSach.Add(bang.Rows[i]);
            DataTable ketQua=bang.Clone();
            string tuKhoa=tim.Trim().ToLowerInvariant();
            string[] cotTim=new string[] {"manv","hoten","sdt","tendangnhap","vaitro" };
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
    private static void KiemTra(string hoten,string sdt,string ten,string vaiTro)
    {
        if (hoten.Trim()=="" || hoten.Trim().Length>100) throw new InvalidOperationException("Vui lòng nhập họ và tên, tối đa 100 ký tự.");
        KiemTraDienThoai(sdt);
        if (ten.Trim()=="" || ten.Trim().Length>50) throw new InvalidOperationException("Vui lòng nhập tên đăng nhập, tối đa 50 ký tự.");
        for (int i=0;i<ten.Trim().Length;i++)
        {
            char c=ten.Trim()[i];
            if (!((c>='a'&&c<='z')||(c>='A'&&c<='Z')||(c>='0'&&c<='9')||c=='_'||c=='.'))
                throw new InvalidOperationException("Tên đăng nhập chỉ gồm chữ không dấu, số, dấu chấm và gạch dưới.");
        }
        if (vaiTro!="Quản lý" && vaiTro!="Nhân viên") throw new InvalidOperationException("Vai trò không hợp lệ.");
    }
    public static void KiemTraDienThoai(string sdt)
    {
        string so=sdt.Trim();
        if (so.Length<9 || so.Length>15) throw new InvalidOperationException("Số điện thoại cần từ 9 đến 15 chữ số.");
        for (int i=0;i<so.Length;i++)
            if (so[i]<'0'||so[i]>'9') throw new InvalidOperationException("Số điện thoại chỉ gồm chữ số.");
    }
    public static void Them(string hoten,string sdt,string ten,string vaiTro,string matKhau)
    {
        YeuCauQuanLy(); KiemTra(hoten,sdt,ten,vaiTro);
        if (matKhau.Length<6 || matKhau.Length>100) throw new InvalidOperationException("Mật khẩu phải có từ 6 đến 100 ký tự.");
        using (MyConnection kn=new MyConnection()) new NhanVienDAO(kn).Them(hoten.Trim(),sdt.Trim(),ten.Trim(),vaiTro,BamMatKhau(matKhau));
    }
    public static void Sua(int ma,string hoten,string sdt,string ten,string vaiTro)
    {
        YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần sửa.");
        KiemTra(hoten,sdt,ten,vaiTro);
        if (ma==MaNhanVien && vaiTro!="Quản lý") throw new InvalidOperationException("Không được tự hạ vai trò tài khoản đang đăng nhập.");
        using (MyConnection kn=new MyConnection())
            if (new NhanVienDAO(kn).Sua(ma,hoten.Trim(),sdt.Trim(),ten.Trim(),vaiTro)==0)
                throw new InvalidOperationException("Nhân viên không còn tồn tại. Hãy tải lại.");
    }
    public static void Xoa(int ma)
    {
        YeuCauQuanLy();
        if (ma<=0) throw new InvalidOperationException("Chọn một dòng cần xóa.");
        if (ma==MaNhanVien) throw new InvalidOperationException("Không thể xóa tài khoản đang đăng nhập.");
        using (MyConnection kn=new MyConnection())
            if (new NhanVienDAO(kn).Xoa(ma)==0) throw new InvalidOperationException("Nhân viên không còn tồn tại.");
    }
    public static void DoiMatKhau(string cu,string moi,string xacNhan)
    {
        YeuCauDangNhap();
        if (moi.Length<6 || moi.Length>100) throw new InvalidOperationException("Mật khẩu mới cần từ 6 đến 100 ký tự.");
        if (moi!=xacNhan) throw new InvalidOperationException("Mật khẩu nhập lại chưa khớp.");
        using (MyConnection kn=new MyConnection())
        {
            NhanVienDAO dao=new NhanVienDAO(kn);
            DataTable bang=dao.TheoMa(MaNhanVien);
            if (bang.Rows.Count==0 || !KiemTraMatKhau(cu,bang.Rows[0]["matkhau"].ToString()))
                throw new InvalidOperationException("Mật khẩu hiện tại không đúng.");
            if (cu==moi) throw new InvalidOperationException("Mật khẩu mới cần khác mật khẩu hiện tại.");
            if (dao.DoiMatKhau(MaNhanVien,bang.Rows[0]["matkhau"].ToString(),BamMatKhau(moi))==0)
                throw new InvalidOperationException("Mật khẩu đã thay đổi ở phiên khác. Hãy thử lại.");
        }
    }
    public static string BamMatKhau(string matKhau)
    {
        byte[] muoi=RandomNumberGenerator.GetBytes(16);
        byte[] bam=Rfc2898DeriveBytes.Pbkdf2(matKhau,muoi,100000,HashAlgorithmName.SHA256,32);
        return Convert.ToBase64String(muoi)+":"+Convert.ToBase64String(bam);
    }
    public static bool KiemTraMatKhau(string matKhau,string daLuu)
    {
        string[] phan=daLuu.Split(':');
        if (phan.Length!=2) return false;
        try
        {
            byte[] muoi=Convert.FromBase64String(phan[0]);
            byte[] cu=Convert.FromBase64String(phan[1]);
            byte[] moi=Rfc2898DeriveBytes.Pbkdf2(matKhau,muoi,100000,HashAlgorithmName.SHA256,32);
            return CryptographicOperations.FixedTimeEquals(cu,moi);
        }
        catch (FormatException) { return false; }
    }
}
