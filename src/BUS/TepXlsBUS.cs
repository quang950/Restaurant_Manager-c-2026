using System.Data;
using System.Globalization;
using QuanLyNhaHang.DAO;
namespace QuanLyNhaHang.BUS;

public static class TepXlsBUS
{
    private static void KiemTraLoai(int loai) { NhanVienBUS.YeuCauQuanLy(); if (loai<0 || loai>3) throw new InvalidOperationException("Danh mục không hợp lệ."); }
    public static DataTable MauNhap(int loai)
    {
        KiemTraLoai(loai);
        DataTable b=new DataTable();
        string[] cot=loai==0?new string[]{"Tên món","Loại","Đơn vị","Giá bán"}:loai==1?new string[]{"Tên bàn","Số chỗ"}:loai==2?new string[]{"Tên nhà cung cấp","Số điện thoại","Địa chỉ"}:new string[]{"Mã nhân viên","Họ tên","Số điện thoại","Tên đăng nhập","Vai trò"};
        for(int i=0;i<cot.Length;i++) b.Columns.Add(cot[i]);
        return b;
    }
    public static DataTable DanhMuc(int loai)
    {
        KiemTraLoai(loai);
        DataTable nguon=loai==0?MonNuocBUS.DanhSach(""):loai==1?BanBUS.DanhSach(""):loai==2?NhaCungCapBUS.DanhSach(""):NhanVienBUS.DanhSach("");
        DataTable b=MauNhap(loai);
        string[] cot=loai==0?new string[]{"tenmon","loai","donvi","gia"}:loai==1?new string[]{"tenban","socho"}:loai==2?new string[]{"tenncc","sdt","diachi"}:new string[]{"manv","hoten","sdt","tendangnhap","vaitro"};
        for(int i=0;i<nguon.Rows.Count;i++) { DataRow r=b.NewRow(); for(int j=0;j<cot.Length;j++) r[j]=nguon.Rows[i][cot[j]]; b.Rows.Add(r); }
        return b;
    }
    public static void Xuat(string path,DataTable b)
    {
        NhanVienBUS.YeuCauQuanLy();
        if (!string.Equals(Path.GetExtension(path),".xls",StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Hãy chọn định dạng .xls.");
        try { TepXlsDAO.Ghi(path,b); }
        catch(IOException ex) { throw new InvalidOperationException("Không ghi được file. Hãy đóng file đang mở và thử lại.",ex); }
        catch(UnauthorizedAccessException ex) { throw new InvalidOperationException("Không có quyền ghi vào thư mục này.",ex); }
    }
    public static DataTable Doc(string path,int loai)
    {
        KiemTraLoai(loai);
        if(loai==3) throw new InvalidOperationException("Nhân viên chỉ cho phép xuất thông tin.");
        if(!string.Equals(Path.GetExtension(path),".xls",StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Chỉ nhận file .xls.");
        DataTable b;
        try { b=TepXlsDAO.Doc(path); }
        catch(Exception ex) when(ex is not InvalidOperationException) { throw new InvalidOperationException("Không đọc được file XLS. Hãy dùng file mẫu của phần mềm.",ex); }
        KiemTraCot(b,loai); return b;
    }
    private static void KiemTraCot(DataTable b,int loai)
    {
        DataTable mau=MauNhap(loai);
        if(b.Columns.Count!=mau.Columns.Count) throw new InvalidOperationException("Số cột không đúng file mẫu. Không nhập cột tồn kho hay chứng từ.");
        for(int i=0;i<mau.Columns.Count;i++) if(b.Columns[i].ColumnName!=mau.Columns[i].ColumnName) throw new InvalidOperationException("Cột "+(i+1)+" phải là: "+mau.Columns[i].ColumnName);
    }
    public static int Nhap(int loai,DataTable b)
    {
        KiemTraLoai(loai); if(loai==3) throw new InvalidOperationException("Không nhập nhân viên từ Excel.");
        KiemTraCot(b,loai); if(b.Rows.Count==0 || b.Rows.Count>5000) throw new InvalidOperationException("File cần có từ 1 đến 5000 dòng dữ liệu.");
        using(MyConnection kn=new MyConnection())
        {
            kn.BatDauGiaoDich();
            DataTable cu=loai==0?new MonNuocDAO(kn).DanhSach():loai==1?new BanDAO(kn).DanhSach():new NhaCungCapDAO(kn).DanhSach();
            string cotTen=loai==0?"tenmon":loai==1?"tenban":"tenncc";
            for(int i=0;i<b.Rows.Count;i++)
            {
                try
                {
                    string ten=b.Rows[i][0].ToString().Trim();
                    for(int j=0;j<cu.Rows.Count;j++) if(string.Equals(ten,cu.Rows[j][cotTen].ToString().Trim(),StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Tên đã tồn tại: "+ten);
                    for(int j=0;j<i;j++) if(string.Equals(ten,b.Rows[j][0].ToString().Trim(),StringComparison.OrdinalIgnoreCase)) throw new InvalidOperationException("Tên bị trùng trong file: "+ten);
                    if(loai==0)
                    {
                        int gia; if(!int.TryParse(b.Rows[i][3].ToString(),NumberStyles.Integer,CultureInfo.InvariantCulture,out gia)) throw new InvalidOperationException("Giá bán phải là số nguyên, ví dụ 50000.");
                        string l=b.Rows[i][1].ToString().Trim(), dv=b.Rows[i][2].ToString().Trim();
                        MonNuocBUS.KiemTra(ten,l,dv,gia); new MonNuocDAO(kn).Them(ten,l,dv,gia);
                    }
                    else if(loai==1)
                    {
                        int so; if(!int.TryParse(b.Rows[i][1].ToString(),out so)) throw new InvalidOperationException("Số chỗ phải là số nguyên.");
                        BanBUS.KiemTra(ten,so); new BanDAO(kn).Them(ten,so);
                    }
                    else
                    {
                        string sdt=b.Rows[i][1].ToString().Trim(),dc=b.Rows[i][2].ToString().Trim();
                        NhaCungCapBUS.KiemTra(ten,sdt,dc); new NhaCungCapDAO(kn).Them(ten,sdt,dc);
                    }
                }
                catch(InvalidOperationException ex) { throw new InvalidOperationException("Dòng Excel "+(i+2)+": "+ex.Message,ex); }
            }
            kn.ChapNhanGiaoDich(); return b.Rows.Count;
        }
    }
}
