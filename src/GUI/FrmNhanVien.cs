using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;

public partial class FrmNhanVien : Form
{
    private int maDangChon=0;
    public FrmNhanVien() { InitializeComponent(); }
    private void Form_Shown(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro!="Quản lý") Close();
    }
    private void Form_Load(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro != "Quản lý") return;
        TaiDuLieu();
        LamMoi();
    }
    private void TaiDuLieu()
    {
        try
        {
            dgvDuLieu.DataSource=NhanVienBUS.DanhSach(txtTim.Text);
            lblTrangThai.Text=dgvDuLieu.Rows.Count+" bản ghi. Chọn một dòng để sửa hoặc xóa.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void txtTim_TextChanged(object sender, EventArgs e) { TaiDuLieu(); }
    private void LamMoi()
    {
        maDangChon=0;
        dgvDuLieu.ClearSelection();
        txthoten.Clear();
        txtsdt.Clear();
        txttendangnhap.Clear();
        txtvaitro.SelectedIndex=0;
        txtmatkhau.Clear();
        txtmatkhau.Enabled=true;
    }
    private void btnMoi_Click(object sender, EventArgs e) { LamMoi(); }
    private void dgvDuLieu_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvDuLieu.SelectedRows.Count==0) return;
        DataGridViewRow dong=dgvDuLieu.SelectedRows[0];
        maDangChon=Convert.ToInt32(dong.Cells["manv"].Value);
        txthoten.Text=dong.Cells["hoten"].Value.ToString();
        txtsdt.Text=dong.Cells["sdt"].Value.ToString();
        txttendangnhap.Text=dong.Cells["tendangnhap"].Value.ToString();
        txtvaitro.Text=dong.Cells["vaitro"].Value.ToString();
        txtmatkhau.Clear();
        txtmatkhau.Enabled=false;
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        if (!txtmatkhau.Enabled) { lblTrangThai.Text="Bấm Làm mới trước khi thêm nhân viên mới."; return; }
        try
        {
            NhanVienBUS.Them(txthoten.Text,txtsdt.Text,txttendangnhap.Text,txtvaitro.Text,txtmatkhau.Text);
            TaiDuLieu(); LamMoi(); lblTrangThai.Text="Đã thêm thành công.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnSua_Click(object sender, EventArgs e)
    {
        try
        {
            NhanVienBUS.Sua(maDangChon,txthoten.Text,txtsdt.Text,txttendangnhap.Text,txtvaitro.Text);
            TaiDuLieu(); LamMoi(); lblTrangThai.Text="Đã lưu thay đổi.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro != "Quản lý") return;
        if (maDangChon==0) { lblTrangThai.Text="Chọn một dòng cần xóa."; return; }
        if (maDangChon==NhanVienBUS.MaNhanVien) { lblTrangThai.Text="Không thể xóa tài khoản đang đăng nhập."; return; }
        if (MessageBox.Show("Bạn có chắc muốn xóa bản ghi đang chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes) return;
        XoaDaChon();
    }
    private void XoaDaChon()
    {
        try
        {
            NhanVienBUS.Xoa(maDangChon);
            TaiDuLieu(); LamMoi(); lblTrangThai.Text="Đã xóa thành công.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }

}
