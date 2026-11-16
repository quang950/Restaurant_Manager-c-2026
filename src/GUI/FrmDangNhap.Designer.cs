namespace QuanLyNhaHang;

partial class FrmDangNhap
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtTaiKhoan, txtMatKhau;
    private Button btnDangNhap;
    private Label lblLoi;
    private Panel trai;
    private Label controlTuDong1;
    private Label tieuDe;
    private Label controlTuDong4;
    private Label controlTuDong5;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        trai = new Panel();
        pictureBox1 = new PictureBox();
        controlTuDong1 = new Label();
        tieuDe = new Label();
        controlTuDong4 = new Label();
        txtTaiKhoan = new TextBox();
        controlTuDong5 = new Label();
        txtMatKhau = new TextBox();
        btnDangNhap = new Button();
        lblLoi = new Label();
        trai.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
        SuspendLayout();
        // 
        // trai
        // 
        trai.BackColor = Color.FromArgb(18, 65, 68);
        trai.Controls.Add(pictureBox1);
        trai.Controls.Add(controlTuDong1);
        trai.Dock = DockStyle.Left;
        trai.Location = new Point(0, 0);
        trai.Name = "trai";
        trai.Size = new Size(350, 490);
        trai.TabIndex = 4;
        // 
        // pictureBox1
        // 
        pictureBox1.Image = Properties.Resources.pngtree_ornate_royal_crown_adorned_with_precious_jewels_isolated_on_transparent_background_png_image_17786847_removebg_preview;
        pictureBox1.Location = new Point(39, 64);
        pictureBox1.Name = "pictureBox1";
        pictureBox1.Size = new Size(261, 180);
        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox1.TabIndex = 1;
        pictureBox1.TabStop = false;
        // 
        // controlTuDong1
        // 
        controlTuDong1.AutoSize = true;
        controlTuDong1.Font = new Font(".VnArabiaH", 29.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
        controlTuDong1.ForeColor = Color.DarkKhaki;
        controlTuDong1.Location = new Point(27, 192);
        controlTuDong1.Name = "controlTuDong1";
        controlTuDong1.Size = new Size(295, 162);
        controlTuDong1.TabIndex = 0;
        controlTuDong1.Text = "\r\n NKDTrung\r\nRestaurant\r\n";
        controlTuDong1.UseMnemonic = false;
        // 
        // tieuDe
        // 
        tieuDe.AutoSize = true;
        tieuDe.Font = new Font("Segoe Script", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
        tieuDe.Location = new Point(434, 96);
        tieuDe.Name = "tieuDe";
        tieuDe.Size = new Size(299, 53);
        tieuDe.TabIndex = 0;
        tieuDe.Text = "Have a nice day";
        tieuDe.UseMnemonic = false;
        // 
        // controlTuDong4
        // 
        controlTuDong4.AutoSize = true;
        controlTuDong4.Location = new Point(393, 165);
        controlTuDong4.Name = "controlTuDong4";
        controlTuDong4.Size = new Size(100, 19);
        controlTuDong4.TabIndex = 2;
        controlTuDong4.Text = "Tên đăng nhập";
        controlTuDong4.UseMnemonic = false;
        // 
        // txtTaiKhoan
        // 
        txtTaiKhoan.Location = new Point(393, 192);
        txtTaiKhoan.MaxLength = 50;
        txtTaiKhoan.Name = "txtTaiKhoan";
        txtTaiKhoan.Size = new Size(360, 25);
        txtTaiKhoan.TabIndex = 5;
        // 
        // controlTuDong5
        // 
        controlTuDong5.AutoSize = true;
        controlTuDong5.Location = new Point(393, 240);
        controlTuDong5.Name = "controlTuDong5";
        controlTuDong5.Size = new Size(68, 19);
        controlTuDong5.TabIndex = 3;
        controlTuDong5.Text = "Mật khẩu";
        controlTuDong5.UseMnemonic = false;
        // 
        // txtMatKhau
        // 
        txtMatKhau.Location = new Point(393, 267);
        txtMatKhau.MaxLength = 100;
        txtMatKhau.Name = "txtMatKhau";
        txtMatKhau.Size = new Size(360, 25);
        txtMatKhau.TabIndex = 6;
        txtMatKhau.UseSystemPasswordChar = true;
        // 
        // btnDangNhap
        // 
        btnDangNhap.BackColor = Color.FromArgb(18, 105, 96);
        btnDangNhap.FlatStyle = FlatStyle.Flat;
        btnDangNhap.ForeColor = Color.White;
        btnDangNhap.Location = new Point(393, 325);
        btnDangNhap.Name = "btnDangNhap";
        btnDangNhap.Size = new Size(360, 45);
        btnDangNhap.TabIndex = 7;
        btnDangNhap.Text = "ĐĂNG NHẬP";
        btnDangNhap.UseMnemonic = false;
        btnDangNhap.UseVisualStyleBackColor = false;
        btnDangNhap.Click += btnDangNhap_Click;
        // 
        // lblLoi
        // 
        lblLoi.ForeColor = Color.Firebrick;
        lblLoi.Location = new Point(393, 385);
        lblLoi.Name = "lblLoi";
        lblLoi.Size = new Size(365, 70);
        lblLoi.TabIndex = 8;
        lblLoi.UseMnemonic = false;
        // 
        // FrmDangNhap
        // 
        AcceptButton = btnDangNhap;
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(820, 490);
        Controls.Add(tieuDe);
        Controls.Add(controlTuDong4);
        Controls.Add(controlTuDong5);
        Controls.Add(trai);
        Controls.Add(txtTaiKhoan);
        Controls.Add(txtMatKhau);
        Controls.Add(btnDangNhap);
        Controls.Add(lblLoi);
        Font = new Font("Segoe UI", 10F);
        ForeColor = Color.FromArgb(30, 45, 55);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "FrmDangNhap";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Đăng nhập • Quản lý nhà hàng";
        trai.ResumeLayout(false);
        trai.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }
    private PictureBox pictureBox1;
}
