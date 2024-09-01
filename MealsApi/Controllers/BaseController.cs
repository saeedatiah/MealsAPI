using MealsApi.Models;
using MealsApi.Services;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Reflection;

namespace MealsApi.Controllers
{
    public static class BaseController<T> where T : new()
    {
        public static ResponseModel<List<T>> GetAll(string sqlQuery, string connectionString ,bool isProc=false)
        {
            List<T> items = new List<T>();
            ResponseModel<List<T>> res = new ResponseModel<List<T>>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if(isProc)
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    while (reader.Read())
                    {
                        T t = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Type type = t.GetType();
                            PropertyInfo prop = type.GetProperty(reader.GetName(i));
                            if (prop.PropertyType.Name == "Single")
                            {
                                prop.SetValue(t, float.Parse(reader.GetValue(i).ToString()), null);
                            }
                            else
                                prop.SetValue(t, reader.GetValue(i), null);
                        }
                        items.Add(t);
                    }
                    reader.Close();
                    res.data = items;
                    res.code = 200;
                    res.message = "OK";
                    return res;
                }
                catch (Exception ex)
                {
                    res.data = null;
                    res.code = 500;
                    res.message = ex.Message;
                    return res;
                }
            }
        }

    }
}
