using System.Data;
using QuanLyNhaHang.BUS;
namespace QuanLyNhaHang;
public partial class FrmTepXls : Form
{
    private DataTable xemTruoc;
    public FrmTepXls() { NhanVienBUS.YeuCauQuanLy(); InitializeComponent(); cboLoai.SelectedIndex=0; }
    private void cboLoai_SelectedIndexChanged(object sender,EventArgs e)
    {
        xemTruoc=null; dgv.DataSource=null; btnNhap.Enabled=false;
        btnDoc.Enabled=cboLoai.SelectedIndex<3; btnMau.Enabled=btnDoc.Enabled;
        lblTrangThai.Text=cboLoai.SelectedIndex==3?"Chỉ xuất thông tin nhân viên, không có mật khẩu.":"Nhập thêm danh mục mới, không cập nhật bản ghi cũ. Món mới có tồn = 0; tăng tồn bằng phiếu nhập hàng.";
    }
    private void btnDoc_Click(object sender,EventArgs e)
    {
        xemTruoc=null; dgv.DataSource=null; btnNhap.Enabled=false;
        using(OpenFileDialog f=new OpenFileDialog())
        {
            f.Filter="Excel 97-2003|*.xls";
            if(f.ShowDialog(this)!=DialogResult.OK) return;
            try { xemTruoc=TepXlsBUS.Doc(f.FileName,cboLoai.SelectedIndex); dgv.DataSource=xemTruoc; btnNhap.Enabled=xemTruoc.Rows.Count>0; lblTrangThai.Text=xemTruoc.Rows.Count+" dòng xem trước. Chọn Nhập dữ liệu để lưu."; }
            catch(InvalidOperationException ex) { MessageBox.Show(this,ex.Message,"Đọc Excel"); }
        }
    }
    private void btnNhap_Click(object sender,EventArgs e)
    {
        if(xemTruoc==null) return;
        if(MessageBox.Show(this,"Thêm "+xemTruoc.Rows.Count+" bản ghi vào danh mục? Nếu một dòng lỗi, toàn bộ lần nhập sẽ hủy.","Xác nhận nhập",MessageBoxButtons.YesNo)!=DialogResult.Yes) return;
        try { int so=TepXlsBUS.Nhap(cboLoai.SelectedIndex,xemTruoc); btnNhap.Enabled=false; xemTruoc=null; lblTrangThai.Text="Đã thêm "+so+" bản ghi. Mở lại danh mục để xem dữ liệu mới."; }
        catch(InvalidOperationException ex) { MessageBox.Show(this,ex.Message,"Nhập Excel"); }
    }
    private void Luu(bool mau)
    {
        using(SaveFileDialog f=new SaveFileDialog())
        {
            f.Filter="Excel 97-2003|*.xls"; f.FileName=(mau?"MauNhap_":"DanhMuc_")+cboLoai.SelectedIndex+".xls";
            if(f.ShowDialog(this)!=DialogResult.OK) return;
            try { TepXlsBUS.Xuat(f.FileName,mau?TepXlsBUS.MauNhap(cboLoai.SelectedIndex):TepXlsBUS.DanhMuc(cboLoai.SelectedIndex)); lblTrangThai.Text="Đã lưu: "+f.FileName; }
            catch(InvalidOperationException ex) { MessageBox.Show(this,ex.Message,"Xuất Excel"); }
        }
    }
    private void btnMau_Click(object sender,EventArgs e) { Luu(true); }
    private void btnXuat_Click(object sender,EventArgs e) { Luu(false); }
}
