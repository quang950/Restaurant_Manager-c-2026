using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;

public partial class FrmNhapHang : Form
{
    private DataTable bangNhap=new DataTable();
    private int maPhieu=0;
    private int maMonDangChon=0;
    private bool daGhiNhan=false;
    private bool dangTai=false;
    private string maXacNhan="";

    public FrmNhapHang()
    {
        InitializeComponent();
        bangNhap=NhapHangBUS.TaoBangNhap();
    }
    private void Form_Load(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro!="Quản lý") return;
        TaiDanhMuc(); TaiPhieu(); TaoMoi();
    }
    private void Form_Shown(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro!="Quản lý") Close();
    }
    private void TaiDanhMuc()
    {
        try
        {
            cboNcc.DisplayMember="tenncc"; cboNcc.ValueMember="mancc";
            cboNcc.DataSource=NhaCungCapBUS.NhapHang();
            cboMon.DisplayMember="hienthi"; cboMon.ValueMember="mamon";
            cboMon.DataSource=MonNuocBUS.NhapHang();
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void TaiPhieu(int chonMa=0)
    {
        dangTai=true;
        try
        {
            dgvPhieu.DataSource=NhapHangBUS.DanhSach(txtTim.Text);
            dgvPhieu.ClearSelection();
            for (int i=0;i<dgvPhieu.Rows.Count;i++)
                if (Convert.ToInt32(dgvPhieu.Rows[i].Cells["mapn"].Value)==chonMa)
                { dgvPhieu.Rows[i].Selected=true; break; }
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
        finally { dangTai=false; }
        if (chonMa>0) XemPhieu(chonMa);
    }
    private void ChoNhap(bool choPhep)
    {
        cboNcc.Enabled=choPhep; dtNgayNhap.Enabled=choPhep;
        cboMon.Enabled=choPhep; numSoLuong.Enabled=choPhep; numGiaNhap.Enabled=choPhep;
        btnThem.Enabled=choPhep; btnSua.Enabled=choPhep; btnXoa.Enabled=choPhep; btnGhiNhan.Enabled=choPhep;
    }
    private void TaoMoi()
    {
        maPhieu=0; maMonDangChon=0; daGhiNhan=false;
        // Mã cố định cho lần nhập này, dùng để ngăn lưu lặp nếu bấm lại hoặc mất mạng sau khi commit.
        maXacNhan=Guid.NewGuid().ToString("N");
        bangNhap.Clear(); dgvChiTiet.DataSource=bangNhap; dgvChiTiet.ClearSelection();
        dtNgayNhap.Value=DateTime.Now; numSoLuong.Value=1; numGiaNhap.Value=10000;
        lblPhieu.Text="PHIẾU NHẬP MỚI\nChọn nhà cung cấp, thêm các dòng hàng rồi ghi nhận.";
        lblTong.Text="TỔNG NHẬP: 0 đ";
        lblTrangThai.Text="Chưa ghi nhận: tồn kho chưa thay đổi.";
        ChoNhap(true);
    }
    private void btnMoi_Click(object sender, EventArgs e)
    {
        if (!daGhiNhan && bangNhap.Rows.Count>0 && MessageBox.Show("Bỏ các dòng chưa ghi nhận để tạo phiếu mới?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        TaiDanhMuc(); TaoMoi(); dgvPhieu.ClearSelection();
    }
    private void txtTim_TextChanged(object sender, EventArgs e) { TaiPhieu(); }
    private void dgvPhieu_SelectionChanged(object sender, EventArgs e)
    {
        if (dangTai || dgvPhieu.SelectedRows.Count==0) return;
        if (!daGhiNhan && bangNhap.Rows.Count>0 && MessageBox.Show("Bỏ các dòng chưa ghi nhận để xem phiếu đã nhập?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes)
        { dangTai=true; dgvPhieu.ClearSelection(); dangTai=false; return; }
        XemPhieu(Convert.ToInt32(dgvPhieu.SelectedRows[0].Cells["mapn"].Value));
    }
    private void XemPhieu(int ma)
    {
        try
        {
            DataTable phieu=NhapHangBUS.ThongTin(ma);
            if (phieu.Rows.Count==0) { lblTrangThai.Text="Phiếu không còn tồn tại."; return; }
            DataTable chiTiet=NhapHangBUS.ChiTiet(ma);
            maPhieu=ma; daGhiNhan=true; maMonDangChon=0;
            bangNhap.Clear();
            cboNcc.SelectedValue=Convert.ToInt32(phieu.Rows[0]["mancc"]);
            dtNgayNhap.Value=Convert.ToDateTime(phieu.Rows[0]["ngaynhap"]);
            dgvChiTiet.DataSource=chiTiet;
            lblPhieu.Text="PHIẾU NHẬP #"+ma+" · ĐÃ GHI NHẬN\nNhân viên: "+phieu.Rows[0]["hoten"];
            lblTong.Text="TỔNG NHẬP: "+Convert.ToInt32(phieu.Rows[0]["tongtien"]).ToString("N0")+" đ";
            lblTrangThai.Text="Phiếu đã tăng tồn. Chỉ xem; không sửa, xóa hoặc ghi nhận lại.";
            ChoNhap(false);
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void dgvChiTiet_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvChiTiet.SelectedRows.Count==0) return;
        DataGridViewRow dong=dgvChiTiet.SelectedRows[0];
        maMonDangChon=Convert.ToInt32(dong.Cells["mamon"].Value);
        cboMon.SelectedValue=maMonDangChon;
        numSoLuong.Value=Convert.ToInt32(dong.Cells["soluong"].Value);
        numGiaNhap.Value=Convert.ToInt32(dong.Cells["dongianhap"].Value);
    }
    private long TongTien()
    {
        return NhapHangBUS.TongTien(bangNhap);
    }
    private void btnThem_Click(object sender, EventArgs e) { ThemSuaDong(true); }
    private void btnSua_Click(object sender, EventArgs e) { ThemSuaDong(false); }
    private void ThemSuaDong(bool them)
    {
        if (daGhiNhan) return;
        if (cboMon.SelectedValue==null) { lblTrangThai.Text="Cần có món/nước trước khi nhập hàng."; return; }
        int mon=them?Convert.ToInt32(cboMon.SelectedValue):maMonDangChon;
        try
        {
            string ten=((DataRowView)cboMon.SelectedItem)["tenmon"].ToString();
            NhapHangBUS.ThemSuaDong(bangNhap,mon,ten,Convert.ToInt32(numSoLuong.Value),Convert.ToInt32(numGiaNhap.Value),them);
            lblTong.Text="TỔNG NHẬP: "+TongTien().ToString("N0")+" đ";
            lblTrangThai.Text="Đã cập nhật dòng. Bấm Ghi nhận để tăng tồn.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (daGhiNhan) return;
        try
        {
            NhapHangBUS.XoaDong(bangNhap,maMonDangChon);
            maMonDangChon=0; dgvChiTiet.ClearSelection();
            lblTong.Text="TỔNG NHẬP: "+TongTien().ToString("N0")+" đ";
            lblTrangThai.Text="Đã bỏ dòng chưa ghi nhận. Tồn kho chưa thay đổi.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnGhiNhan_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("Ghi nhận phiếu và tăng tồn kho? Phiếu đã ghi nhận sẽ không được sửa/xóa.","Xác nhận nhập hàng",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        GhiNhan();
    }
    private void GhiNhan()
    {
        if (daGhiNhan) { lblTrangThai.Text="Phiếu này đã ghi nhận; không tăng tồn lần nữa."; return; }
        if (cboNcc.SelectedValue==null) { lblTrangThai.Text="Chọn nhà cung cấp trước."; return; }
        try
        {
            int daLuu=NhapHangBUS.GhiNhan(Convert.ToInt32(cboNcc.SelectedValue),dtNgayNhap.Value,maXacNhan,bangNhap);
            maPhieu=daLuu; daGhiNhan=true; ChoNhap(false);
            txtTim.Clear(); TaiDanhMuc(); TaiPhieu(daLuu);
            lblTrangThai.Text="Đã ghi nhận phiếu và tăng tồn kho.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
}
