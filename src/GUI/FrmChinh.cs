using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;

public partial class FrmChinh : Form
{
    public bool DangXuat = false;
    public FrmChinh()
    {
        InitializeComponent();
        nguoiDung.Text=NhanVienBUS.TenNhanVien+"  •  "+NhanVienBUS.VaiTro;
        bool quanLy = NhanVienBUS.VaiTro == "Quản lý";
        btnMon.Enabled = quanLy;
        btnNhanVien.Enabled = quanLy;
        btnBan.Enabled = quanLy;
        btnNcc.Enabled = quanLy;
        btnNhapHang.Enabled = quanLy;
        btnThongKe.Enabled = quanLy;
        btnTepXls.Enabled = quanLy;
    }
    private void FrmChinh_Activated(object sender, EventArgs e)
    {
        try
        {
            DataTable bang=ThongKeBUS.TongQuan();
            lblBan.Text="BÀN TRỐNG\n"+bang.Rows[0]["bantrong"];
            lblHoaDon.Text="ĐANG PHỤC VỤ\n"+bang.Rows[0]["dangphucvu"];
            if (NhanVienBUS.VaiTro=="Quản lý") lblDoanhThu.Text="THU HÔM NAY\n"+Convert.ToInt64(bang.Rows[0]["doanhthu"]).ToString("N0")+" đ";
            else lblDoanhThu.Text="CA LÀM VIỆC\nSẵn sàng phục vụ";
        }
        catch (InvalidOperationException ex) { lblBan.Text="Chưa tải được dữ liệu"; lblHoaDon.Text="Kiểm tra MySQL"; lblDoanhThu.Text=ex.Message; }
    }
    private void btnTepXls_Click(object sender,EventArgs e) { using(FrmTepXls f=new FrmTepXls()) f.ShowDialog(this); }
    private void btnHoaDon_Click(object sender, EventArgs e) { using (FrmHoaDon f = new FrmHoaDon()) f.ShowDialog(this); }
    private void btnMon_Click(object sender, EventArgs e) { using (FrmMonNuoc f = new FrmMonNuoc()) f.ShowDialog(this); }
    private void btnBan_Click(object sender, EventArgs e) { using (FrmBan f = new FrmBan()) f.ShowDialog(this); }
    private void btnNhanVien_Click(object sender, EventArgs e) { using (FrmNhanVien f = new FrmNhanVien()) f.ShowDialog(this); }
    private void btnNcc_Click(object sender, EventArgs e) { using (FrmNhaCungCap f = new FrmNhaCungCap()) f.ShowDialog(this); }
    private void btnNhapHang_Click(object sender, EventArgs e) { using (FrmNhapHang f = new FrmNhapHang()) f.ShowDialog(this); }
    private void btnThongKe_Click(object sender, EventArgs e) { using (FrmThongKe f = new FrmThongKe()) f.ShowDialog(this); }
    private void btnDoiMatKhau_Click(object sender, EventArgs e) { using (FrmDoiMatKhau f = new FrmDoiMatKhau()) f.ShowDialog(this); }
    private void btnDangXuat_Click(object sender, EventArgs e) { DangXuat=true; Close(); }
}
