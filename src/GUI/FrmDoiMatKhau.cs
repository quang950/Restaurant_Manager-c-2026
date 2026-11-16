using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;
public partial class FrmDoiMatKhau : Form
{
    public FrmDoiMatKhau() { InitializeComponent(); }
    private void btnLuu_Click(object sender, EventArgs e)
    {
        try
        {
            NhanVienBUS.DoiMatKhau(txtCu.Text,txtMoi.Text,txtXacNhan.Text);
            txtCu.Clear(); txtMoi.Clear(); txtXacNhan.Clear();
            lblTrangThai.ForeColor=Color.FromArgb(18,105,96);
            lblTrangThai.Text="Đã đổi mật khẩu thành công.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
}
