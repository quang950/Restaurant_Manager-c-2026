using System.Data;

namespace QuanLyNhaHang.WebApi.Helpers;

public static class DataTableHelper
{
    public static List<Dictionary<string, object>> ToList(DataTable table)
    {
        var list = new List<Dictionary<string, object>>(table.Rows.Count);
        foreach (DataRow row in table.Rows)
        {
            var dict = new Dictionary<string, object>(table.Columns.Count);
            foreach (DataColumn col in table.Columns)
            {
                dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
            }
            list.Add(dict);
        }
        return list;
    }

    public static byte[] ExportTempFile(string extension, Action<string> writeAction)
    {
        string tempPath = Path.Combine(Path.GetTempPath(), $"qlnh_{Guid.NewGuid():N}{extension}");
        try
        {
            writeAction(tempPath);
            return File.ReadAllBytes(tempPath);
        }
        finally
        {
            if (File.Exists(tempPath)) File.Delete(tempPath);
        }
    }
}
