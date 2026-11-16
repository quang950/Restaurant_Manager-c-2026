using QuanLyNhaHang.BUS;
using System.Data;
namespace QuanLyNhaHang;
public partial class FrmThongKe : Form
{
    public FrmThongKe()
    {
        InitializeComponent();
        numNam.Value=DateTime.Today.Year;
        dtTu.Value=DateTime.Today.AddDays(-29);
        dtDen.Value=DateTime.Today;
    }
    private void Form_Shown(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro!="Quản lý") Close();
    }
    private void Form_Load(object sender, EventArgs e)
    {
        if (NhanVienBUS.VaiTro!="Quản lý") return;
        btnXem_Click(sender,e);
    }
    private void btnXem_Click(object sender, EventArgs e)
    {
        try
        {
            if (tabBaoCao.SelectedIndex==1 || tabBaoCao.SelectedIndex==2)
            {
                DataGridView grid=tabBaoCao.SelectedIndex==1?dgvNhanVienQuy:dgvSanPhamQuy;
                DataTable quy=tabBaoCao.SelectedIndex==1?ThongKeBUS.NhanVienQuy(Convert.ToInt32(numNam.Value)):ThongKeBUS.SanPhamQuy(Convert.ToInt32(numNam.Value));
                grid.DataSource=quy;
                if (grid.Rows.Count>0)
                {
                    grid.Rows[grid.Rows.Count-1].DefaultCellStyle.BackColor=Color.FromArgb(226,237,233);
                    grid.Rows[grid.Rows.Count-1].DefaultCellStyle.Font=new Font(grid.Font,FontStyle.Bold);
                }
                lblTong.Text="Tổng năm "+numNam.Value+": "+Convert.ToInt64(quy.Rows[quy.Rows.Count-1]["tong"]).ToString("N0")+" đ";
                lblTrangThai.Text="Chỉ tính hóa đơn đã thanh toán, theo năm chọn và ngày thanh toán.";
                return;
            }
            DataTable bang=ThongKeBUS.DoanhThu(dtTu.Value,dtDen.Value,cboNhom.SelectedIndex==1);
            dgvDoanhThu.DataSource=bang;
            dgvMon.DataSource=ThongKeBUS.MonBanChay(dtTu.Value,dtDen.Value);
            long thu=ThongKeBUS.TongDoanhThu(bang), chi=ThongKeBUS.TongChi(bang);
            lblTong.Text="Thu: "+thu.ToString("N0")+" đ    |    Chi: "+chi.ToString("N0")+" đ    |    Lợi nhuận: "+(thu-chi).ToString("N0")+" đ\n"+ThongKeBUS.SoHoaDon(bang)+" hóa đơn đã thanh toán. Chi = tiền nhập hàng.";
            lblTrangThai.Text=bang.Rows.Count==0?"Không có thu / chi trong khoảng ngày này.":"Doanh thu tính theo ngày thanh toán, bao gồm cả ngày kết thúc.";
            lblTrangThai.ForeColor=Color.DimGray;
        }
        catch (InvalidOperationException ex) { lblTrangThai.Text=ex.Message; }
    }
    private void tabBaoCao_SelectedIndexChanged(object sender,EventArgs e)
    {
        dtTu.Enabled=tabBaoCao.SelectedIndex==0 || tabBaoCao.SelectedIndex==3;
        dtDen.Enabled=dtTu.Enabled;
        numNam.Enabled=!dtTu.Enabled;
        cboNhom.Enabled=tabBaoCao.SelectedIndex==0;
        btnXem_Click(sender,e);
    }

    private void Xuat(bool pdf)
    {
        try
        {
            int loai=tabBaoCao.SelectedIndex;
            DataTable b=BaoCaoBUS.Bang(loai,dtTu.Value,dtDen.Value,cboNhom.SelectedIndex==1,Convert.ToInt32(numNam.Value));
            btnXem_Click(this,EventArgs.Empty);
            using(SaveFileDialog f=new SaveFileDialog())
            {
                f.Filter=pdf?"Báo cáo PDF|*.pdf":"Excel 97-2003|*.xls";
                f.FileName="BaoCao_"+DateTime.Now.ToString("yyyyMMdd_HHmmss")+(pdf?".pdf":".xls");
                if(f.ShowDialog(this)!=DialogResult.OK) return;
                string ky=loai==1 || loai==2?"Năm "+numNam.Value:"Từ "+dtTu.Value.ToString("dd/MM/yyyy")+" đến "+dtDen.Value.ToString("dd/MM/yyyy");
                if(pdf) BaoCaoBUS.XuatPdf(f.FileName,BaoCaoBUS.Ten(loai),ky+" | Đơn vị tiền: VND",b,loai==0?"Tổng thu = hóa đơn đã thanh toán. Tổng chi = tiền nhập hàng. Lợi nhuận = tổng thu - tổng chi.":"Chỉ tính hóa đơn đã thanh toán; thời gian dựa trên ngày thanh toán.");
                else TepXlsBUS.Xuat(f.FileName,b);
                lblTrangThai.Text="Đã xuất: "+f.FileName;
            }
        }
        catch(InvalidOperationException ex) { MessageBox.Show(this,ex.Message,"Xuất báo cáo"); }
    }
    private void btnXls_Click(object sender,EventArgs e) { Xuat(false); }
    private void btnPdf_Click(object sender,EventArgs e) { Xuat(true); }
}
