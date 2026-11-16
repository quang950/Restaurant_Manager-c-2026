using QuanLyNhaHang.BUS;
namespace QuanLyNhaHang;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("vi-VN");
        ApplicationConfiguration.Initialize();
        while (true)
        {
            using (FrmDangNhap dangNhap = new FrmDangNhap())
            {
                if (dangNhap.ShowDialog() != DialogResult.OK) break;
            }
            using (FrmChinh chinh = new FrmChinh())
            {
                Application.Run(chinh);
                if (!chinh.DangXuat) break;
            }
            NhanVienBUS.DangXuat();
        }
    }
}
