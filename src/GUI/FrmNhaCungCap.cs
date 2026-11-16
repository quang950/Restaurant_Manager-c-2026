using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;

public partial class FrmNhaCungCap : Form
{
    private int maDangChon=0;
    public FrmNhaCungCap() { InitializeComponent(); }
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
            dgvDuLieu.DataSource=NhaCungCapBUS.DanhSach(txtTim.Text);
            lblTrangThai.Text=dgvDuLieu.Rows.Count+" bản ghi. Chọn một dòng để sửa hoặc xóa.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void txtTim_TextChanged(object sender, EventArgs e) { TaiDuLieu(); }
    private void LamMoi()
    {
        maDangChon=0;
        dgvDuLieu.ClearSelection();
        txttenncc.Clear();
        txtsdt.Clear();
        txtdiachi.Clear();
    }
    private void btnMoi_Click(object sender, EventArgs e) { LamMoi(); }
    private void dgvDuLieu_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvDuLieu.SelectedRows.Count==0) return;
        DataGridViewRow dong=dgvDuLieu.SelectedRows[0];
        maDangChon=Convert.ToInt32(dong.Cells["mancc"].Value);
        txttenncc.Text=dong.Cells["tenncc"].Value.ToString();
        txtsdt.Text=dong.Cells["sdt"].Value.ToString();
        txtdiachi.Text=dong.Cells["diachi"].Value.ToString();
    }

    private void btnThem_Click(object sender, EventArgs e)
    {
        try
        {
            NhaCungCapBUS.Them(txttenncc.Text,txtsdt.Text,txtdiachi.Text);
            TaiDuLieu(); LamMoi(); lblTrangThai.Text="Đã thêm thành công.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnSua_Click(object sender, EventArgs e)
    {
        try
        {
            NhaCungCapBUS.Sua(maDangChon,txttenncc.Text,txtsdt.Text,txtdiachi.Text);
            TaiDuLieu(); LamMoi(); lblTrangThai.Text="Đã lưu thay đổi.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro != "Quản lý") return;
        if (maDangChon==0) { lblTrangThai.Text="Chọn một dòng cần xóa."; return; }
        if (MessageBox.Show("Bạn có chắc muốn xóa bản ghi đang chọn?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question)!=DialogResult.Yes) return;
        XoaDaChon();
    }
    private void XoaDaChon()
    {
        try
        {
            NhaCungCapBUS.Xoa(maDangChon);
            TaiDuLieu(); LamMoi(); lblTrangThai.Text="Đã xóa thành công.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }

}
