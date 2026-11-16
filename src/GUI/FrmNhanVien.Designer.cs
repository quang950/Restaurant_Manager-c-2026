namespace QuanLyNhaHang;

partial class FrmNhanVien
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvDuLieu;
    private TextBox txtTim;
    private Label lblTrangThai;
    private Button btnThem, btnSua, btnXoa, btnMoi;
    private TextBox txthoten;
    private TextBox txtsdt;
    private TextBox txttendangnhap;
    private ComboBox txtvaitro;
    private TextBox txtmatkhau;

    private Label tieuDe;
    private Panel trai;
    private Label controlTuDong1;
    private Label controlTuDong2;
    private Label controlTuDong3;
    private Label controlTuDong4;
    private Label controlTuDong5;
    private Panel phai;
    private Panel timKiem;
    private Label controlTuDong6;
    private DataGridViewTextBoxColumn controlTuDong7;
    private DataGridViewTextBoxColumn controlTuDong8;
    private DataGridViewTextBoxColumn controlTuDong9;
    private DataGridViewTextBoxColumn controlTuDong10;
    private DataGridViewTextBoxColumn controlTuDong11;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        tieuDe = new Label();
        trai = new Panel();
        controlTuDong1 = new Label();
        txthoten = new TextBox();
        controlTuDong2 = new Label();
        txtsdt = new TextBox();
        controlTuDong3 = new Label();
        txttendangnhap = new TextBox();
        controlTuDong4 = new Label();
        txtvaitro = new ComboBox();
        controlTuDong5 = new Label();
        txtmatkhau = new TextBox();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnMoi = new Button();
        phai = new Panel();
        dgvDuLieu = new DataGridView();
        controlTuDong7 = new DataGridViewTextBoxColumn();
        controlTuDong8 = new DataGridViewTextBoxColumn();
        controlTuDong9 = new DataGridViewTextBoxColumn();
        controlTuDong10 = new DataGridViewTextBoxColumn();
        controlTuDong11 = new DataGridViewTextBoxColumn();
        lblTrangThai = new Label();
        timKiem = new Panel();
        controlTuDong6 = new Label();
        txtTim = new TextBox();
        trai.SuspendLayout();
        phai.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).BeginInit();
        timKiem.SuspendLayout();
        SuspendLayout();
        // 
        // tieuDe
        // 
        tieuDe.BackColor = Color.FromArgb(18, 65, 68);
        tieuDe.Dock = DockStyle.Top;
        tieuDe.Font = new Font("Segoe UI", 23F, FontStyle.Bold);
        tieuDe.ForeColor = Color.White;
        tieuDe.Location = new Point(0, 0);
        tieuDe.Name = "tieuDe";
        tieuDe.Padding = new Padding(28, 20, 0, 0);
        tieuDe.Size = new Size(1120, 85);
        tieuDe.TabIndex = 2;
        tieuDe.Text = "Nhân viên";
        tieuDe.UseMnemonic = false;
        // 
        // trai
        // 
        trai.BackColor = Color.White;
        trai.Controls.Add(controlTuDong1);
        trai.Controls.Add(txthoten);
        trai.Controls.Add(controlTuDong2);
        trai.Controls.Add(txtsdt);
        trai.Controls.Add(controlTuDong3);
        trai.Controls.Add(txttendangnhap);
        trai.Controls.Add(controlTuDong4);
        trai.Controls.Add(txtvaitro);
        trai.Controls.Add(controlTuDong5);
        trai.Controls.Add(txtmatkhau);
        trai.Controls.Add(btnThem);
        trai.Controls.Add(btnSua);
        trai.Controls.Add(btnXoa);
        trai.Controls.Add(btnMoi);
        trai.Dock = DockStyle.Left;
        trai.Location = new Point(0, 85);
        trai.Name = "trai";
        trai.Padding = new Padding(20);
        trai.Size = new Size(330, 625);
        trai.TabIndex = 1;
        // 
        // controlTuDong1
        // 
        controlTuDong1.AutoSize = true;
        controlTuDong1.Location = new Point(22, 18);
        controlTuDong1.Name = "controlTuDong1";
        controlTuDong1.Size = new Size(69, 19);
        controlTuDong1.TabIndex = 0;
        controlTuDong1.Text = "Họ và tên";
        controlTuDong1.UseMnemonic = false;
        // 
        // txthoten
        // 
        txthoten.Location = new Point(22, 44);
        txthoten.MaxLength = 100;
        txthoten.Name = "txthoten";
        txthoten.Size = new Size(284, 25);
        txthoten.TabIndex = 1;
        // 
        // controlTuDong2
        // 
        controlTuDong2.AutoSize = true;
        controlTuDong2.Location = new Point(22, 94);
        controlTuDong2.Name = "controlTuDong2";
        controlTuDong2.Size = new Size(89, 19);
        controlTuDong2.TabIndex = 2;
        controlTuDong2.Text = "Số điện thoại";
        controlTuDong2.UseMnemonic = false;
        // 
        // txtsdt
        // 
        txtsdt.Location = new Point(22, 120);
        txtsdt.MaxLength = 20;
        txtsdt.Name = "txtsdt";
        txtsdt.Size = new Size(284, 25);
        txtsdt.TabIndex = 3;
        // 
        // controlTuDong3
        // 
        controlTuDong3.AutoSize = true;
        controlTuDong3.Location = new Point(22, 170);
        controlTuDong3.Name = "controlTuDong3";
        controlTuDong3.Size = new Size(100, 19);
        controlTuDong3.TabIndex = 4;
        controlTuDong3.Text = "Tên đăng nhập";
        controlTuDong3.UseMnemonic = false;
        // 
        // txttendangnhap
        // 
        txttendangnhap.Location = new Point(22, 196);
        txttendangnhap.MaxLength = 50;
        txttendangnhap.Name = "txttendangnhap";
        txttendangnhap.Size = new Size(284, 25);
        txttendangnhap.TabIndex = 5;
        // 
        // controlTuDong4
        // 
        controlTuDong4.AutoSize = true;
        controlTuDong4.Location = new Point(22, 246);
        controlTuDong4.Name = "controlTuDong4";
        controlTuDong4.Size = new Size(49, 19);
        controlTuDong4.TabIndex = 6;
        controlTuDong4.Text = "Vai trò";
        controlTuDong4.UseMnemonic = false;
        // 
        // txtvaitro
        // 
        txtvaitro.DropDownStyle = ComboBoxStyle.DropDownList;
        txtvaitro.Items.AddRange(new object[] { "Nhân viên", "Quản lý" });
        txtvaitro.Location = new Point(22, 272);
        txtvaitro.Name = "txtvaitro";
        txtvaitro.Size = new Size(284, 25);
        txtvaitro.TabIndex = 7;
        // 
        // controlTuDong5
        // 
        controlTuDong5.AutoSize = true;
        controlTuDong5.Location = new Point(22, 322);
        controlTuDong5.Name = "controlTuDong5";
        controlTuDong5.Size = new Size(155, 19);
        controlTuDong5.TabIndex = 8;
        controlTuDong5.Text = "Mật khẩu (chỉ khi thêm)";
        controlTuDong5.UseMnemonic = false;
        // 
        // txtmatkhau
        // 
        txtmatkhau.Location = new Point(22, 348);
        txtmatkhau.MaxLength = 100;
        txtmatkhau.Name = "txtmatkhau";
        txtmatkhau.Size = new Size(284, 25);
        txtmatkhau.TabIndex = 9;
        txtmatkhau.UseSystemPasswordChar = true;
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.FromArgb(18, 105, 96);
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.ForeColor = Color.White;
        btnThem.Location = new Point(22, 410);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(138, 40);
        btnThem.TabIndex = 10;
        btnThem.Text = "Thêm";
        btnThem.UseMnemonic = false;
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.FromArgb(18, 105, 96);
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(168, 410);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(138, 40);
        btnSua.TabIndex = 11;
        btnSua.Text = "Lưu sửa";
        btnSua.UseMnemonic = false;
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.FromArgb(239, 243, 245);
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.ForeColor = Color.FromArgb(30, 45, 55);
        btnXoa.Location = new Point(22, 458);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(138, 40);
        btnXoa.TabIndex = 12;
        btnXoa.Text = "Xóa";
        btnXoa.UseMnemonic = false;
        btnXoa.UseVisualStyleBackColor = false;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnMoi
        // 
        btnMoi.BackColor = Color.FromArgb(239, 243, 245);
        btnMoi.FlatStyle = FlatStyle.Flat;
        btnMoi.ForeColor = Color.FromArgb(30, 45, 55);
        btnMoi.Location = new Point(168, 458);
        btnMoi.Name = "btnMoi";
        btnMoi.Size = new Size(138, 40);
        btnMoi.TabIndex = 13;
        btnMoi.Text = "Làm mới";
        btnMoi.UseMnemonic = false;
        btnMoi.UseVisualStyleBackColor = false;
        btnMoi.Click += btnMoi_Click;
        // 
        // phai
        // 
        phai.Controls.Add(dgvDuLieu);
        phai.Controls.Add(lblTrangThai);
        phai.Controls.Add(timKiem);
        phai.Dock = DockStyle.Fill;
        phai.Location = new Point(330, 85);
        phai.Name = "phai";
        phai.Padding = new Padding(20);
        phai.Size = new Size(790, 625);
        phai.TabIndex = 0;
        // 
        // dgvDuLieu
        // 
        dgvDuLieu.AutoGenerateColumns = false;
        controlTuDong7.Name = "manv";
        controlTuDong7.DataPropertyName = "manv";
        controlTuDong7.HeaderText = "Mã nhân viên";
        controlTuDong8.Name = "hoten";
        controlTuDong8.DataPropertyName = "hoten";
        controlTuDong8.HeaderText = "Họ và tên";
        controlTuDong9.Name = "sdt";
        controlTuDong9.DataPropertyName = "sdt";
        controlTuDong9.HeaderText = "Số điện thoại";
        controlTuDong10.Name = "tendangnhap";
        controlTuDong10.DataPropertyName = "tendangnhap";
        controlTuDong10.HeaderText = "Tên đăng nhập";
        controlTuDong11.Name = "vaitro";
        controlTuDong11.DataPropertyName = "vaitro";
        controlTuDong11.HeaderText = "Vai trò";
        dgvDuLieu.Columns.AddRange(new DataGridViewColumn[] { controlTuDong7, controlTuDong8, controlTuDong9, controlTuDong10, controlTuDong11 });
        dgvDuLieu.AllowUserToAddRows = false;
        dgvDuLieu.AllowUserToDeleteRows = false;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(247, 250, 249);
        dgvDuLieu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
        dgvDuLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        dgvDuLieu.BackgroundColor = Color.White;
        dgvDuLieu.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(226, 237, 233);
        dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
        dgvDuLieu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        dgvDuLieu.ColumnHeadersHeight = 42;
        dgvDuLieu.Dock = DockStyle.Fill;
        dgvDuLieu.EnableHeadersVisualStyles = false;
        dgvDuLieu.Location = new Point(20, 85);
        dgvDuLieu.MultiSelect = false;
        dgvDuLieu.Name = "dgvDuLieu";
        dgvDuLieu.ReadOnly = true;
        dgvDuLieu.RowHeadersVisible = false;
        dgvDuLieu.RowTemplate.Height = 36;
        dgvDuLieu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDuLieu.Size = new Size(750, 465);
        dgvDuLieu.TabIndex = 0;
        dgvDuLieu.SelectionChanged += dgvDuLieu_SelectionChanged;
        // 
        // lblTrangThai
        // 
        lblTrangThai.Dock = DockStyle.Bottom;
        lblTrangThai.ForeColor = Color.FromArgb(18, 105, 96);
        lblTrangThai.Location = new Point(20, 550);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Padding = new Padding(0, 12, 0, 0);
        lblTrangThai.Size = new Size(750, 55);
        lblTrangThai.TabIndex = 1;
        lblTrangThai.UseMnemonic = false;
        // 
        // timKiem
        // 
        timKiem.Controls.Add(controlTuDong6);
        timKiem.Controls.Add(txtTim);
        timKiem.Dock = DockStyle.Top;
        timKiem.Location = new Point(20, 20);
        timKiem.Name = "timKiem";
        timKiem.Size = new Size(750, 65);
        timKiem.TabIndex = 2;
        // 
        // controlTuDong6
        // 
        controlTuDong6.AutoSize = true;
        controlTuDong6.Location = new Point(0, 0);
        controlTuDong6.Name = "controlTuDong6";
        controlTuDong6.Size = new Size(213, 19);
        controlTuDong6.TabIndex = 0;
        controlTuDong6.Text = "Tìm kiếm theo mã hoặc thông tin";
        controlTuDong6.UseMnemonic = false;
        // 
        // txtTim
        // 
        txtTim.Location = new Point(0, 27);
        txtTim.MaxLength = 100;
        txtTim.Name = "txtTim";
        txtTim.Size = new Size(440, 25);
        txtTim.TabIndex = 1;
        txtTim.TextChanged += txtTim_TextChanged;
        // 
        // FrmNhanVien
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1120, 710);
        Controls.Add(phai);
        Controls.Add(trai);
        Controls.Add(tieuDe);
        Font = new Font("Segoe UI", 10F);
        ForeColor = Color.FromArgb(30, 45, 55);
        MinimumSize = new Size(1000, 730);
        Name = "FrmNhanVien";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Nhân viên • Bếp Nhà";
        Load += Form_Load;
        Shown += Form_Shown;
        trai.ResumeLayout(false);
        trai.PerformLayout();
        phai.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).EndInit();
        timKiem.ResumeLayout(false);
        timKiem.PerformLayout();
        ResumeLayout(false);
    }
}
