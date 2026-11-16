namespace QuanLyNhaHang;

partial class FrmChinh
{
    private System.ComponentModel.IContainer components = null;
    private Label lblBan, lblHoaDon, lblDoanhThu;
    private Button btnMon, btnNhanVien, btnBan, btnNcc, btnHoaDon, btnThongKe, btnDoiMatKhau, btnDangXuat;
    private Button btnNhapHang;
    private Panel menu;
    private Label controlTuDong1;
    private Panel noiDung;
    private Label chao;
    private Label phu;
    private Label nguoiDung;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private Button btnTepXls;
    private void InitializeComponent()
    {
        menu = new Panel();
        controlTuDong1 = new Label();
        btnHoaDon = new Button();
        btnMon = new Button();
        btnBan = new Button();
        btnNhanVien = new Button();
        btnNcc = new Button();
        btnNhapHang = new Button();
        btnThongKe = new Button();
        btnDoiMatKhau = new Button();
        btnDangXuat = new Button();
        noiDung = new Panel();
        chao = new Label();
        phu = new Label();
        nguoiDung = new Label();
        lblBan = new Label();
        lblHoaDon = new Label();
        lblDoanhThu = new Label();
        menu.SuspendLayout();
        noiDung.SuspendLayout();
        SuspendLayout();
        // 
        // menu
        // 
        menu.BackColor = Color.FromArgb(18, 65, 68);
        menu.Controls.Add(controlTuDong1);
        menu.Controls.Add(btnHoaDon);
        menu.Controls.Add(btnMon);
        menu.Controls.Add(btnBan);
        menu.Controls.Add(btnNhanVien);
        menu.Controls.Add(btnNcc);
        menu.Controls.Add(btnNhapHang);
        menu.Controls.Add(btnThongKe);
        menu.Controls.Add(btnDoiMatKhau);
        menu.Controls.Add(btnDangXuat);
        menu.Dock = DockStyle.Left;
        menu.ForeColor = Color.FromArgb(30, 45, 55);
        menu.Location = new Point(0, 0);
        menu.Name = "menu";
        menu.Padding = new Padding(18);
        menu.Size = new Size(230, 720);
        menu.TabIndex = 1;
        // 
        // controlTuDong1
        // 
        controlTuDong1.AutoSize = true;
        controlTuDong1.Font = new Font(".VnArabia", 24.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        controlTuDong1.ForeColor = Color.DarkKhaki;
        controlTuDong1.Location = new Point(14, 27);
        controlTuDong1.Name = "controlTuDong1";
        controlTuDong1.Size = new Size(198, 80);
        controlTuDong1.TabIndex = 0;
        controlTuDong1.Text = " NKDTrung\r\nRestaurant";
        controlTuDong1.UseMnemonic = false;
        // 
        // btnHoaDon
        // 
        btnHoaDon.BackColor = Color.FromArgb(28, 82, 82);
        btnHoaDon.FlatStyle = FlatStyle.Flat;
        btnHoaDon.ForeColor = Color.White;
        btnHoaDon.Location = new Point(18, 145);
        btnHoaDon.Name = "btnHoaDon";
        btnHoaDon.Padding = new Padding(10, 0, 0, 0);
        btnHoaDon.Size = new Size(194, 46);
        btnHoaDon.TabIndex = 2;
        btnHoaDon.Text = "Gọi món & Hóa đơn";
        btnHoaDon.TextAlign = ContentAlignment.MiddleLeft;
        btnHoaDon.UseMnemonic = false;
        btnHoaDon.UseVisualStyleBackColor = false;
        btnHoaDon.Click += btnHoaDon_Click;
        // 
        // btnMon
        // 
        btnMon.BackColor = Color.FromArgb(28, 82, 82);
        btnMon.FlatStyle = FlatStyle.Flat;
        btnMon.ForeColor = Color.White;
        btnMon.Location = new Point(18, 204);
        btnMon.Name = "btnMon";
        btnMon.Padding = new Padding(10, 0, 0, 0);
        btnMon.Size = new Size(194, 46);
        btnMon.TabIndex = 3;
        btnMon.Text = "Món ăn / Nước uống";
        btnMon.TextAlign = ContentAlignment.MiddleLeft;
        btnMon.UseMnemonic = false;
        btnMon.UseVisualStyleBackColor = false;
        btnMon.Click += btnMon_Click;
        // 
        // btnBan
        // 
        btnBan.BackColor = Color.FromArgb(28, 82, 82);
        btnBan.FlatStyle = FlatStyle.Flat;
        btnBan.ForeColor = Color.White;
        btnBan.Location = new Point(18, 263);
        btnBan.Name = "btnBan";
        btnBan.Padding = new Padding(10, 0, 0, 0);
        btnBan.Size = new Size(194, 46);
        btnBan.TabIndex = 4;
        btnBan.Text = "Quản lý bàn";
        btnBan.TextAlign = ContentAlignment.MiddleLeft;
        btnBan.UseMnemonic = false;
        btnBan.UseVisualStyleBackColor = false;
        btnBan.Click += btnBan_Click;
        // 
        // btnNhanVien
        // 
        btnNhanVien.BackColor = Color.FromArgb(28, 82, 82);
        btnNhanVien.FlatStyle = FlatStyle.Flat;
        btnNhanVien.ForeColor = Color.White;
        btnNhanVien.Location = new Point(18, 322);
        btnNhanVien.Name = "btnNhanVien";
        btnNhanVien.Padding = new Padding(10, 0, 0, 0);
        btnNhanVien.Size = new Size(194, 46);
        btnNhanVien.TabIndex = 5;
        btnNhanVien.Text = "Nhân viên";
        btnNhanVien.TextAlign = ContentAlignment.MiddleLeft;
        btnNhanVien.UseMnemonic = false;
        btnNhanVien.UseVisualStyleBackColor = false;
        btnNhanVien.Click += btnNhanVien_Click;
        // 
        // btnNcc
        // 
        btnNcc.BackColor = Color.FromArgb(28, 82, 82);
        btnNcc.FlatStyle = FlatStyle.Flat;
        btnNcc.ForeColor = Color.White;
        btnNcc.Location = new Point(18, 381);
        btnNcc.Name = "btnNcc";
        btnNcc.Padding = new Padding(10, 0, 0, 0);
        btnNcc.Size = new Size(194, 46);
        btnNcc.TabIndex = 6;
        btnNcc.Text = "Nhà cung cấp";
        btnNcc.TextAlign = ContentAlignment.MiddleLeft;
        btnNcc.UseMnemonic = false;
        btnNcc.UseVisualStyleBackColor = false;
        btnNcc.Click += btnNcc_Click;
        // 
        // btnNhapHang
        // 
        btnNhapHang.BackColor = Color.FromArgb(28, 82, 82);
        btnNhapHang.FlatStyle = FlatStyle.Flat;
        btnNhapHang.ForeColor = Color.White;
        btnNhapHang.Location = new Point(18, 440);
        btnNhapHang.Name = "btnNhapHang";
        btnNhapHang.Padding = new Padding(10, 0, 0, 0);
        btnNhapHang.Size = new Size(194, 46);
        btnNhapHang.TabIndex = 7;
        btnNhapHang.Text = "Nhập hàng";
        btnNhapHang.TextAlign = ContentAlignment.MiddleLeft;
        btnNhapHang.UseMnemonic = false;
        btnNhapHang.UseVisualStyleBackColor = false;
        btnNhapHang.Click += btnNhapHang_Click;
        // 
        // btnThongKe
        // 
        btnThongKe.BackColor = Color.FromArgb(28, 82, 82);
        btnThongKe.FlatStyle = FlatStyle.Flat;
        btnThongKe.ForeColor = Color.White;
        btnThongKe.Location = new Point(18, 499);
        btnThongKe.Name = "btnThongKe";
        btnThongKe.Padding = new Padding(10, 0, 0, 0);
        btnThongKe.Size = new Size(194, 46);
        btnThongKe.TabIndex = 7;
        btnThongKe.Text = "Thống kê";
        btnThongKe.TextAlign = ContentAlignment.MiddleLeft;
        btnThongKe.UseMnemonic = false;
        btnThongKe.UseVisualStyleBackColor = false;
        btnThongKe.Click += btnThongKe_Click;
        // 
        // btnDoiMatKhau
        // 
        btnDoiMatKhau.BackColor = Color.FromArgb(28, 82, 82);
        btnDoiMatKhau.FlatStyle = FlatStyle.Flat;
        btnDoiMatKhau.ForeColor = Color.White;
        btnDoiMatKhau.Location = new Point(18, 558);
        btnDoiMatKhau.Name = "btnDoiMatKhau";
        btnDoiMatKhau.Padding = new Padding(10, 0, 0, 0);
        btnDoiMatKhau.Size = new Size(194, 46);
        btnDoiMatKhau.TabIndex = 8;
        btnDoiMatKhau.Text = "Đổi mật khẩu";
        btnDoiMatKhau.TextAlign = ContentAlignment.MiddleLeft;
        btnDoiMatKhau.UseMnemonic = false;
        btnDoiMatKhau.UseVisualStyleBackColor = false;
        btnDoiMatKhau.Click += btnDoiMatKhau_Click;
        // 
        // btnDangXuat
        // 
        btnDangXuat.BackColor = Color.FromArgb(28, 82, 82);
        btnDangXuat.FlatStyle = FlatStyle.Flat;
        btnDangXuat.ForeColor = Color.White;
        btnDangXuat.Location = new Point(18, 617);
        btnDangXuat.Name = "btnDangXuat";
        btnDangXuat.Padding = new Padding(10, 0, 0, 0);
        btnDangXuat.Size = new Size(194, 46);
        btnDangXuat.TabIndex = 9;
        btnDangXuat.Text = "Đăng xuất";
        btnDangXuat.TextAlign = ContentAlignment.MiddleLeft;
        btnDangXuat.UseMnemonic = false;
        btnDangXuat.UseVisualStyleBackColor = false;
        btnDangXuat.Click += btnDangXuat_Click;
        // 
        // noiDung
        // 
        noiDung.Controls.Add(chao);
        noiDung.Controls.Add(phu);
        noiDung.Controls.Add(nguoiDung);
        noiDung.Controls.Add(lblBan);
        noiDung.Controls.Add(lblHoaDon);
        noiDung.Controls.Add(lblDoanhThu);
        noiDung.Dock = DockStyle.Fill;
        noiDung.Location = new Point(230, 0);
        noiDung.Name = "noiDung";
        noiDung.Padding = new Padding(35);
        noiDung.Size = new Size(930, 720);
        noiDung.TabIndex = 0;
        // 
        // chao
        // 
        chao.AutoSize = true;
        chao.Font = new Font("Segoe UI", 26F, FontStyle.Bold);
        chao.Location = new Point(35, 40);
        chao.Name = "chao";
        chao.Size = new Size(362, 47);
        chao.TabIndex = 0;
        chao.Text = "Tổng quan nhà hàng";
        chao.UseMnemonic = false;
        // 
        // phu
        // 
        phu.AutoSize = true;
        phu.ForeColor = Color.DimGray;
        phu.Location = new Point(38, 100);
        phu.Name = "phu";
        phu.Size = new Size(358, 19);
        phu.TabIndex = 1;
        phu.Text = "Một nơi để quản lý bàn, món ăn và các hóa đơn của bạn.";
        phu.UseMnemonic = false;
        // 
        // nguoiDung
        // 
        nguoiDung.AutoSize = true;
        nguoiDung.Location = new Point(38, 140);
        nguoiDung.Name = "nguoiDung";
        nguoiDung.Size = new Size(176, 19);
        nguoiDung.TabIndex = 2;
        nguoiDung.Text = "Nhân viên đang đăng nhập";
        nguoiDung.UseMnemonic = false;
        // 
        // lblBan
        // 
        lblBan.BackColor = Color.White;
        lblBan.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblBan.Location = new Point(38, 220);
        lblBan.Name = "lblBan";
        lblBan.Padding = new Padding(20);
        lblBan.Size = new Size(240, 135);
        lblBan.TabIndex = 3;
        lblBan.UseMnemonic = false;
        // 
        // lblHoaDon
        // 
        lblHoaDon.BackColor = Color.White;
        lblHoaDon.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblHoaDon.ForeColor = Color.FromArgb(30, 45, 55);
        lblHoaDon.Location = new Point(297, 220);
        lblHoaDon.Name = "lblHoaDon";
        lblHoaDon.Padding = new Padding(20);
        lblHoaDon.Size = new Size(240, 135);
        lblHoaDon.TabIndex = 4;
        lblHoaDon.UseMnemonic = false;
        // 
        // lblDoanhThu
        // 
        lblDoanhThu.BackColor = Color.White;
        lblDoanhThu.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblDoanhThu.Location = new Point(556, 220);
        lblDoanhThu.Name = "lblDoanhThu";
        lblDoanhThu.Padding = new Padding(20);
        lblDoanhThu.Size = new Size(300, 135);
        lblDoanhThu.TabIndex = 5;
        lblDoanhThu.UseMnemonic = false;
        // 
        // FrmChinh
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(1160, 720);
        Controls.Add(noiDung);
        Controls.Add(menu);
        Font = new Font("Segoe UI", 10F);
        ForeColor = Color.FromArgb(30, 45, 55);
        MinimumSize = new Size(1176, 750);
        Name = "FrmChinh";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Bếp Nhà • Quản lý nhà hàng";
        Activated += FrmChinh_Activated;
        menu.ResumeLayout(false);
        menu.PerformLayout();
        btnTepXls=new Button(); btnTepXls.Name="btnTepXls"; btnTepXls.Text="Nhập / xuất danh mục Excel";
        btnTepXls.Location=new Point(38,410); btnTepXls.Size=new Size(280,46); btnTepXls.Click+=btnTepXls_Click;
        noiDung.Controls.Add(btnTepXls);
        noiDung.ResumeLayout(false);
        noiDung.PerformLayout();
        ResumeLayout(false);
    }
}
