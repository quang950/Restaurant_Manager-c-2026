namespace QuanLyNhaHang;
partial class FrmTepXls
{
    private System.ComponentModel.IContainer components=null;
    private ComboBox cboLoai;
    private Button btnDoc,btnNhap,btnMau,btnXuat;
    private DataGridView dgv;
    private Label header,lblTrangThai;
    private Panel thanh;
    protected override void Dispose(bool disposing) { if(disposing && components!=null) components.Dispose(); base.Dispose(disposing); }
    private void InitializeComponent()
    {
        components=new System.ComponentModel.Container();
        SuspendLayout();
        Font=new Font("Segoe UI",10); ClientSize=new Size(1050,650); MinimumSize=new Size(1066,689); StartPosition=FormStartPosition.CenterParent; Text="Nhập / xuất Excel";
        header=new Label(); header.Name="header"; header.Text="Nhập / xuất danh mục Excel"; header.Font=new Font("Segoe UI",22,FontStyle.Bold); header.BackColor=Color.FromArgb(18,65,68); header.ForeColor=Color.White; header.Dock=DockStyle.Top; header.Height=80; header.Padding=new Padding(20);
        thanh=new Panel(); thanh.Name="thanh"; thanh.Dock=DockStyle.Top; thanh.Height=80;
        cboLoai=new ComboBox(); cboLoai.Name="cboLoai"; cboLoai.DropDownStyle=ComboBoxStyle.DropDownList; cboLoai.Items.AddRange(new object[]{"Món / nước","Bàn","Nhà cung cấp","Nhân viên (chỉ xuất)"}); cboLoai.Location=new Point(20,25); cboLoai.Width=230; cboLoai.SelectedIndexChanged+=cboLoai_SelectedIndexChanged;
        btnXuat=new Button(); btnXuat.Name="btnXuat"; btnXuat.Text="Xuất danh mục"; btnXuat.Location=new Point(270,20); btnXuat.Size=new Size(160,38); btnXuat.Click+=btnXuat_Click;
        btnMau=new Button(); btnMau.Name="btnMau"; btnMau.Text="Tải mẫu nhập"; btnMau.Location=new Point(440,20); btnMau.Size=new Size(160,38); btnMau.Click+=btnMau_Click;
        btnDoc=new Button(); btnDoc.Name="btnDoc"; btnDoc.Text="Chọn file / Xem trước"; btnDoc.Location=new Point(610,20); btnDoc.Size=new Size(220,38); btnDoc.Click+=btnDoc_Click;
        btnNhap=new Button(); btnNhap.Name="btnNhap"; btnNhap.Text="Nhập dữ liệu"; btnNhap.Location=new Point(840,20); btnNhap.Size=new Size(180,38); btnNhap.Click+=btnNhap_Click;
        thanh.Controls.AddRange(new Control[]{cboLoai,btnXuat,btnMau,btnDoc,btnNhap});
        dgv=new DataGridView(); dgv.Name="dgv"; dgv.Dock=DockStyle.Fill; dgv.ReadOnly=true; dgv.AllowUserToAddRows=false; dgv.AllowUserToDeleteRows=false; dgv.RowHeadersVisible=false; dgv.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill; dgv.BackgroundColor=Color.White;
        lblTrangThai=new Label(); lblTrangThai.Name="lblTrangThai"; lblTrangThai.Dock=DockStyle.Bottom; lblTrangThai.Height=65; lblTrangThai.Padding=new Padding(20,10,20,0);
        Controls.Add(dgv); Controls.Add(lblTrangThai); Controls.Add(thanh); Controls.Add(header); ResumeLayout(false);
    }
}
