namespace QuanLyNhaHang;

partial class FrmDoiMatKhau
{
    private System.ComponentModel.IContainer components = null;
    private TextBox txtCu, txtMoi, txtXacNhan;
    private Button btnLuu;
    private Label lblTrangThai;
    private Label controlTuDong1;
    private Label controlTuDong2;
    private Label controlTuDong3;
    private Label controlTuDong4;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        controlTuDong1 = new Label();
        controlTuDong2 = new Label();
        txtCu = new TextBox();
        controlTuDong3 = new Label();
        txtMoi = new TextBox();
        controlTuDong4 = new Label();
        txtXacNhan = new TextBox();
        btnLuu = new Button();
        lblTrangThai = new Label();
        SuspendLayout();
        // 
        // controlTuDong1
        // 
        controlTuDong1.AutoSize = true;
        controlTuDong1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        controlTuDong1.Location = new Point(32, 25);
        controlTuDong1.Name = "controlTuDong1";
        controlTuDong1.Size = new Size(294, 37);
        controlTuDong1.TabIndex = 0;
        controlTuDong1.Text = "Đổi mật khẩu cá nhân";
        controlTuDong1.UseMnemonic = false;
        // 
        // controlTuDong2
        // 
        controlTuDong2.AutoSize = true;
        controlTuDong2.Location = new Point(35, 88);
        controlTuDong2.Name = "controlTuDong2";
        controlTuDong2.Size = new Size(117, 19);
        controlTuDong2.TabIndex = 1;
        controlTuDong2.Text = "Mật khẩu hiện tại";
        controlTuDong2.UseMnemonic = false;
        // 
        // txtCu
        // 
        txtCu.Location = new Point(35, 115);
        txtCu.MaxLength = 100;
        txtCu.Name = "txtCu";
        txtCu.Size = new Size(440, 25);
        txtCu.TabIndex = 4;
        txtCu.UseSystemPasswordChar = true;
        // 
        // controlTuDong3
        // 
        controlTuDong3.AutoSize = true;
        controlTuDong3.Location = new Point(35, 155);
        controlTuDong3.Name = "controlTuDong3";
        controlTuDong3.Size = new Size(95, 19);
        controlTuDong3.TabIndex = 2;
        controlTuDong3.Text = "Mật khẩu mới";
        controlTuDong3.UseMnemonic = false;
        // 
        // txtMoi
        // 
        txtMoi.Location = new Point(35, 182);
        txtMoi.MaxLength = 100;
        txtMoi.Name = "txtMoi";
        txtMoi.Size = new Size(440, 25);
        txtMoi.TabIndex = 5;
        txtMoi.UseSystemPasswordChar = true;
        // 
        // controlTuDong4
        // 
        controlTuDong4.AutoSize = true;
        controlTuDong4.Location = new Point(35, 222);
        controlTuDong4.Name = "controlTuDong4";
        controlTuDong4.Size = new Size(148, 19);
        controlTuDong4.TabIndex = 3;
        controlTuDong4.Text = "Nhập lại mật khẩu mới";
        controlTuDong4.UseMnemonic = false;
        // 
        // txtXacNhan
        // 
        txtXacNhan.Location = new Point(35, 249);
        txtXacNhan.MaxLength = 100;
        txtXacNhan.Name = "txtXacNhan";
        txtXacNhan.Size = new Size(440, 25);
        txtXacNhan.TabIndex = 6;
        txtXacNhan.UseSystemPasswordChar = true;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(18, 105, 96);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(35, 302);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(440, 42);
        btnLuu.TabIndex = 7;
        btnLuu.Text = "Lưu mật khẩu";
        btnLuu.UseMnemonic = false;
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // lblTrangThai
        // 
        lblTrangThai.ForeColor = Color.Firebrick;
        lblTrangThai.Location = new Point(35, 362);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(440, 65);
        lblTrangThai.TabIndex = 8;
        lblTrangThai.UseMnemonic = false;
        // 
        // FrmDoiMatKhau
        // 
        AcceptButton = btnLuu;
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(520, 440);
        Controls.Add(controlTuDong1);
        Controls.Add(controlTuDong2);
        Controls.Add(controlTuDong3);
        Controls.Add(controlTuDong4);
        Controls.Add(txtCu);
        Controls.Add(txtMoi);
        Controls.Add(txtXacNhan);
        Controls.Add(btnLuu);
        Controls.Add(lblTrangThai);
        Font = new Font("Segoe UI", 10F);
        ForeColor = Color.FromArgb(30, 45, 55);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        Name = "FrmDoiMatKhau";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Đổi mật khẩu";
        ResumeLayout(false);
        PerformLayout();
    }
}
