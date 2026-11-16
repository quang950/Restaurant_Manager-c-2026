using System.Data;
using MySql.Data.MySqlClient;
namespace QuanLyNhaHang.DAO;

// Chỉ lớp DAO biết MySQL. BUS dùng đối tượng này để giữ cùng một transaction.
public sealed class MyConnection : IDisposable
{
    public static string ChuoiKetNoi = "Server=localhost;Port=3306;Database=quanlynhahang;Uid=root;Pwd=123456;CharSet=utf8mb4;SslMode=Preferred;";
    private MySqlConnection ketNoi;
    private MySqlTransaction giaoDich;
    public int MaVuaThem { get; private set; }

    public MyConnection()
    {
        ketNoi=new MySqlConnection(ChuoiKetNoi);
        try { ketNoi.Open(); }
        catch (MySqlException ex) { ketNoi.Dispose(); throw ChuyenLoi(ex); }
    }
    public void BatDauGiaoDich()
    {
        try { giaoDich=ketNoi.BeginTransaction(IsolationLevel.ReadCommitted); }
        catch (MySqlException ex) { throw ChuyenLoi(ex); }
    }
    public void ChapNhanGiaoDich()
    {
        try { giaoDich.Commit(); }
        catch (MySqlException ex) { throw ChuyenLoi(ex); }
    }
    public DataTable Doc(string sql,string[] ten=null,object[] giaTri=null)
    {
        using (MySqlCommand lenh=new MySqlCommand(sql,ketNoi,giaoDich))
        {
            if (ten!=null)
                for (int i=0;i<ten.Length;i++) lenh.Parameters.AddWithValue(ten[i],giaTri[i]);
            try
            {
                using (MySqlDataAdapter doc=new MySqlDataAdapter(lenh))
                { DataTable bang=new DataTable(); doc.Fill(bang); return bang; }
            }
            catch (MySqlException ex) { throw ChuyenLoi(ex); }
        }
    }
    public int Ghi(string sql,string[] ten=null,object[] giaTri=null)
    {
        using (MySqlCommand lenh=new MySqlCommand(sql,ketNoi,giaoDich))
        {
            if (ten!=null)
                for (int i=0;i<ten.Length;i++) lenh.Parameters.AddWithValue(ten[i],giaTri[i]);
            try
            {
                int soDong=lenh.ExecuteNonQuery();
                MaVuaThem=Convert.ToInt32(lenh.LastInsertedId);
                return soDong;
            }
            catch (MySqlException ex) { throw ChuyenLoi(ex); }
        }
    }
    private static InvalidOperationException ChuyenLoi(MySqlException ex)
    {
        string thongBao="Không truy cập được MySQL. Kiểm tra dịch vụ và kết nối trong DAO/MyConnection.cs.";
        if (ex.Number==1062) thongBao="Thông tin bị trùng. Hãy kiểm tra tên hoặc tài khoản.";
        else if (ex.Number==1451) thongBao="Không thể xóa vì bản ghi đã được sử dụng trong hóa đơn hoặc phiếu nhập.";
        else if (ex.Number==1452) thongBao="Dữ liệu tham chiếu đã thay đổi. Hãy tải lại và thử lại.";
        else if (ex.Number==1213 || ex.Number==1205) thongBao="Dữ liệu đang được xử lý ở phiên khác. Hãy tải lại rồi thử lại.";
        return new InvalidOperationException(thongBao,ex);
    }
    public void Dispose()
    {
        // Transaction chưa Commit sẽ tự rollback khi Dispose.
        if (giaoDich!=null) giaoDich.Dispose();
        ketNoi.Dispose();
    }
}
