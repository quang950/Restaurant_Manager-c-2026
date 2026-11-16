namespace QuanLyNhaHang;

partial class FrmBan
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvDuLieu;
    private TextBox txtTim;
    private Label lblTrangThai;
    private Button btnThem, btnSua, btnXoa, btnMoi;
    private TextBox txttenban;
    private NumericUpDown txtsocho;

    private Label tieuDe;
    private Panel trai;
    private Label controlTuDong1;
    private Label controlTuDong2;
    private Panel phai;
    private Panel timKiem;
    private Label controlTuDong3;
    private DataGridViewTextBoxColumn controlTuDong4;
    private DataGridViewTextBoxColumn controlTuDong5;
    private DataGridViewTextBoxColumn controlTuDong6;
    private DataGridViewTextBoxColumn controlTuDong7;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        tieuDe = new Label();
        trai = new Panel();
        controlTuDong1 = new Label();
        txttenban = new TextBox();
        controlTuDong2 = new Label();
        txtsocho = new NumericUpDown();
        btnThem = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnMoi = new Button();
        phai = new Panel();
        dgvDuLieu = new DataGridView();
        controlTuDong4 = new DataGridViewTextBoxColumn();
        controlTuDong5 = new DataGridViewTextBoxColumn();
        controlTuDong6 = new DataGridViewTextBoxColumn();
        controlTuDong7 = new DataGridViewTextBoxColumn();
        lblTrangThai = new Label();
        timKiem = new Panel();
        controlTuDong3 = new Label();
        txtTim = new TextBox();
        trai.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)txtsocho).BeginInit();
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
        tieuDe.Text = "Quản lý bàn";
        tieuDe.UseMnemonic = false;
        // 
        // trai
        // 
        trai.BackColor = Color.White;
        trai.Controls.Add(controlTuDong1);
        trai.Controls.Add(txttenban);
        trai.Controls.Add(controlTuDong2);
        trai.Controls.Add(txtsocho);
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
        controlTuDong1.Size = new Size(57, 19);
        controlTuDong1.TabIndex = 0;
        controlTuDong1.Text = "Tên bàn";
        controlTuDong1.UseMnemonic = false;
        // 
        // txttenban
        // 
        txttenban.Location = new Point(22, 44);
        txttenban.MaxLength = 50;
        txttenban.Name = "txttenban";
        txttenban.Size = new Size(284, 25);
        txttenban.TabIndex = 1;
        // 
        // controlTuDong2
        // 
        controlTuDong2.AutoSize = true;
        controlTuDong2.Location = new Point(22, 94);
        controlTuDong2.Name = "controlTuDong2";
        controlTuDong2.Size = new Size(50, 19);
        controlTuDong2.TabIndex = 2;
        controlTuDong2.Text = "Số chỗ";
        controlTuDong2.UseMnemonic = false;
        // 
        // txtsocho
        // 
        txtsocho.Location = new Point(22, 120);
        txtsocho.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        txtsocho.Name = "txtsocho";
        txtsocho.Size = new Size(284, 25);
        txtsocho.TabIndex = 3;
        txtsocho.ThousandsSeparator = true;
        txtsocho.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // btnThem
        // 
        btnThem.BackColor = Color.SandyBrown;
        btnThem.FlatStyle = FlatStyle.Flat;
        btnThem.ForeColor = Color.Black;
        btnThem.Location = new Point(22, 182);
        btnThem.Name = "btnThem";
        btnThem.Size = new Size(138, 40);
        btnThem.TabIndex = 4;
        btnThem.Text = "Thêm";
        btnThem.UseMnemonic = false;
        btnThem.UseVisualStyleBackColor = false;
        btnThem.Click += btnThem_Click;
        // 
        // btnSua
        // 
        btnSua.BackColor = Color.Coral;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.ForeColor = Color.White;
        btnSua.Location = new Point(168, 182);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(138, 40);
        btnSua.TabIndex = 5;
        btnSua.Text = "Lưu sửa";
        btnSua.UseMnemonic = false;
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.BackColor = Color.Red;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(22, 230);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(138, 40);
        btnXoa.TabIndex = 6;
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
        btnMoi.Location = new Point(168, 230);
        btnMoi.Name = "btnMoi";
        btnMoi.Size = new Size(138, 40);
        btnMoi.TabIndex = 7;
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
        controlTuDong4.Name = "maban";
        controlTuDong4.DataPropertyName = "maban";
        controlTuDong4.HeaderText = "Mã bàn";
        controlTuDong5.Name = "tenban";
        controlTuDong5.DataPropertyName = "tenban";
        controlTuDong5.HeaderText = "Tên bàn";
        controlTuDong6.Name = "socho";
        controlTuDong6.DataPropertyName = "socho";
        controlTuDong6.HeaderText = "Số chỗ";
        controlTuDong7.Name = "trangthai";
        controlTuDong7.DataPropertyName = "trangthai";
        controlTuDong7.HeaderText = "Trạng thái";
        dgvDuLieu.Columns.AddRange(new DataGridViewColumn[] { controlTuDong4, controlTuDong5, controlTuDong6, controlTuDong7 });
        dgvDuLieu.AllowUserToAddRows = false;
        dgvDuLieu.AllowUserToDeleteRows = false;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(247, 250, 249);
        dgvDuLieu.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle3;
        dgvDuLieu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDuLieu.BackgroundColor = Color.White;
        dgvDuLieu.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
        dataGridViewCellStyle4.BackColor = Color.FromArgb(226, 237, 233);
        dataGridViewCellStyle4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
        dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
        dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
        dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
        dgvDuLieu.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
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
        timKiem.Controls.Add(controlTuDong3);
        timKiem.Controls.Add(txtTim);
        timKiem.Dock = DockStyle.Top;
        timKiem.Location = new Point(20, 20);
        timKiem.Name = "timKiem";
        timKiem.Size = new Size(750, 65);
        timKiem.TabIndex = 2;
        // 
        // controlTuDong3
        // 
        controlTuDong3.AutoSize = true;
        controlTuDong3.Location = new Point(0, 0);
        controlTuDong3.Name = "controlTuDong3";
        controlTuDong3.Size = new Size(213, 19);
        controlTuDong3.TabIndex = 0;
        controlTuDong3.Text = "Tìm kiếm theo mã hoặc thông tin";
        controlTuDong3.UseMnemonic = false;
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
        // FrmBan
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
        Name = "FrmBan";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Quản lý bàn • Bếp Nhà";
        Load += Form_Load;
        Shown += Form_Shown;
        trai.ResumeLayout(false);
        trai.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)txtsocho).EndInit();
        phai.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDuLieu).EndInit();
        timKiem.ResumeLayout(false);
        timKiem.PerformLayout();
        ResumeLayout(false);
    }
}
