using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;

public partial class FrmHoaDon : Form
{
    private int maHoaDon=0;
    private int maMonDangChon=0;
    private bool dangTai=false;
    public FrmHoaDon() { InitializeComponent(); }
    private void Form_Load(object sender, EventArgs e) { TaiDanhMuc(); TaiHoaDon(); }

    private void TaiDanhMuc()
    {
        try
        {
            cboBan.DataSource=BanBUS.GoiMon();
            cboBan.DisplayMember="hienthi"; cboBan.ValueMember="maban";
            cboMon.DataSource=MonNuocBUS.GoiMon();
            cboMon.DisplayMember="hienthi"; cboMon.ValueMember="mamon";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void TaiHoaDon(int chonMa=0)
    {
        dangTai=true;
        try
        {
            dgvHoaDon.DataSource=HoaDonBUS.DanhSach(txtTim.Text);
            dgvHoaDon.ClearSelection();
            maHoaDon=0;
            for (int i=0;i<dgvHoaDon.Rows.Count;i++)
            {
                if (Convert.ToInt32(dgvHoaDon.Rows[i].Cells["mahd"].Value)==chonMa)
                { dgvHoaDon.Rows[i].Selected=true; maHoaDon=chonMa; break; }
            }
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
        finally { dangTai=false; }
        TaiChiTiet();
    }
    private void txtTim_TextChanged(object sender, EventArgs e) { TaiHoaDon(); }
    private void btnTaiLai_Click(object sender, EventArgs e) { TaiDanhMuc(); TaiHoaDon(maHoaDon); }
    private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
    {
        if (dangTai || dgvHoaDon.SelectedRows.Count==0) return;
        maHoaDon=Convert.ToInt32(dgvHoaDon.SelectedRows[0].Cells["mahd"].Value);
        TaiChiTiet();
    }
    private void TaiChiTiet()
    {
        maMonDangChon=0;
        try
        {
            DataTable hd=HoaDonBUS.ThongTin(maHoaDon);
            bool duocSua=hd.Rows.Count>0 && hd.Rows[0]["trangthai"].ToString()=="Chưa thanh toán";
            btnThemMon.Enabled=duocSua; btnSuaMon.Enabled=duocSua; btnXoaMon.Enabled=duocSua;
            btnThanhToan.Enabled=duocSua; btnXoaHoaDon.Enabled=duocSua; btnSuaBan.Enabled=duocSua;
            cboMon.Enabled=duocSua; numSoLuong.Enabled=duocSua;
            if (hd.Rows.Count==0)
            { lblHoaDon.Text="Chọn một hóa đơn bên trái hoặc chọn bàn để mở hóa đơn."; lblTong.Text="TỔNG: 0 đ"; }
            else
            {
                DataRow dong=hd.Rows[0];
                cboBan.SelectedValue=Convert.ToInt32(dong["maban"]);
                lblHoaDon.Text="HĐ #"+maHoaDon+"  •  "+dong["tenban"]+"  •  "+dong["trangthai"]+"\nNhân viên: "+dong["hoten"]+"  |  Lập: "+Convert.ToDateTime(dong["ngaylap"]).ToString("dd/MM/yyyy HH:mm");
                if (dong["ngaythanhtoan"]!=DBNull.Value) lblHoaDon.Text+="\nThanh toán: "+Convert.ToDateTime(dong["ngaythanhtoan"]).ToString("dd/MM/yyyy HH:mm");
                lblTong.Text="TỔNG: "+Convert.ToInt32(dong["tongtien"]).ToString("N0")+" đ";
            }
            dgvChiTiet.DataSource=HoaDonBUS.ChiTiet(maHoaDon);
            dgvChiTiet.ClearSelection();
            maMonDangChon=0;
            numSoLuong.Value=1;
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
    }
    private void btnMoBan_Click(object sender, EventArgs e)
    {
        if (cboBan.SelectedValue==null) { lblTrangThai.Text="Chưa có bàn. Quản lý cần thêm bàn trước."; return; }
        try
        {
            int maMoi=HoaDonBUS.MoBan(Convert.ToInt32(cboBan.SelectedValue));
            txtTim.Clear(); TaiDanhMuc(); TaiHoaDon(maMoi);
            lblTrangThai.Text="Đã mở hóa đơn. Chọn món và số lượng để thêm.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnSuaBan_Click(object sender, EventArgs e)
    {
        if (cboBan.SelectedValue==null) { lblTrangThai.Text="Chọn bàn muốn sửa."; return; }
        try
        {
            HoaDonBUS.SuaBan(maHoaDon,Convert.ToInt32(cboBan.SelectedValue));
            TaiDanhMuc(); TaiHoaDon(maHoaDon); lblTrangThai.Text="Đã sửa bàn của hóa đơn.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnThemMon_Click(object sender, EventArgs e) { LuuChiTiet("Them"); }
    private void btnSuaMon_Click(object sender, EventArgs e) { LuuChiTiet("Sua"); }
    private void btnXoaMon_Click(object sender, EventArgs e)
    {
        if (maMonDangChon==0) { lblTrangThai.Text="Chọn dòng món cần xóa trong bảng chi tiết."; return; }
        if (MessageBox.Show("Xóa món đang chọn khỏi hóa đơn?","Xác nhận",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        LuuChiTiet("Xoa");
    }
    private void LuuChiTiet(string thaoTac)
    {
        if (thaoTac!="Them" && maMonDangChon==0) { lblTrangThai.Text="Chọn dòng món trong bảng chi tiết trước."; return; }
        if (thaoTac=="Them" && cboMon.SelectedValue==null) { lblTrangThai.Text="Chưa có món để thêm."; return; }
        int mon=thaoTac=="Them"?Convert.ToInt32(cboMon.SelectedValue):maMonDangChon;
        try
        {
            HoaDonBUS.LuuChiTiet(maHoaDon,mon,Convert.ToInt32(numSoLuong.Value),thaoTac);
            TaiDanhMuc(); TaiHoaDon(maHoaDon); lblTrangThai.Text="Đã cập nhật chi tiết, tổng tiền và số lượng tồn.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnThanhToan_Click(object sender, EventArgs e)
    {
        if (maHoaDon==0) return;
        if (MessageBox.Show("Xác nhận thanh toán toàn bộ hóa đơn? Sau khi thanh toán không được sửa hoặc xóa.","Thanh toán",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        ThanhToan();
    }
    private void ThanhToan()
    {
        try
        {
            HoaDonBUS.ThanhToan(maHoaDon);
            TaiDanhMuc(); TaiHoaDon(maHoaDon); lblTrangThai.Text="Đã thanh toán. Bàn đã trở về trạng thái trống.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void btnXoaHoaDon_Click(object sender, EventArgs e)
    {
        if (maHoaDon==0) return;
        if (MessageBox.Show("Xóa hóa đơn chưa thanh toán và toàn bộ chi tiết của nó?", "Xác nhận xóa",MessageBoxButtons.YesNo,MessageBoxIcon.Question)!=DialogResult.Yes) return;
        XoaHoaDon();
    }
    private void XoaHoaDon()
    {
        try
        {
            HoaDonBUS.Xoa(maHoaDon);
            TaiDanhMuc(); TaiHoaDon(); lblTrangThai.Text="Đã xóa hóa đơn, hoàn lại tồn và trả bàn trống.";
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }

    private void btnPdf_Click(object sender,EventArgs e)
    {
        if(maHoaDon<=0) { MessageBox.Show(this,"Chọn hóa đơn trước."); return; }
        using(SaveFileDialog f=new SaveFileDialog())
        {
            f.Filter="Hóa đơn PDF|*.pdf"; f.FileName="HoaDon_"+maHoaDon+".pdf";
            if(f.ShowDialog(this)!=DialogResult.OK) return;
            try { BaoCaoBUS.HoaDonPdf(f.FileName,maHoaDon); lblTrangThai.Text="Đã xuất: "+f.FileName; }
            catch(InvalidOperationException ex) { MessageBox.Show(this,ex.Message,"Xuất PDF"); }
        }
    }
}
