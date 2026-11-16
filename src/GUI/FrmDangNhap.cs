using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;

public partial class FrmDangNhap : Form
{
    public FrmDangNhap() { InitializeComponent(); }
    private void btnDangNhap_Click(object sender, EventArgs e)
    {
        lblLoi.Text="";
        try
        {
            NhanVienBUS.DangNhap(txtTaiKhoan.Text,txtMatKhau.Text);
            DialogResult=DialogResult.OK;
        }
        catch (InvalidOperationException ex) { lblLoi.Text=ex.Message; }
    }
}
