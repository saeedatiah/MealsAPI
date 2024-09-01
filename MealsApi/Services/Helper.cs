using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace MealsApi.Services
{
    public static class Helper<T>
    {
        //public static List<T> Query<T>(string query, SqlConnection conn) where T : new()
        //{
        //    List<T> res = new List<T>();
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(query, conn);
        //    SqlDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        T t = new T();

        //        for (int inc = 0; inc < reader.FieldCount; inc++)
        //        {
        //            Type type = t.GetType();
        //            PropertyInfo prop = type.GetProperty(reader.GetName(inc));
        //            prop.SetValue(t, reader.GetValue(inc), null);
        //        }

        //        res.Add(t);
        //    }
        //    r.Close();

        //    return res;

        //}
    }
}
