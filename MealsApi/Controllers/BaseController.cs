using MealsApi.Models;
using MealsApi.Services;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.PortableExecutable;

namespace MealsApi.Controllers
{
    public static class BaseController<T> where T : new()
    {


        public static ResponseModel<List<T>> GetAll(string queryOrProc, string connectionString ,bool isProc=false)
        {
            List<T> items = new List<T>();
            ResponseModel<List<T>> res = new ResponseModel<List<T>>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(queryOrProc, conn);
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

        public static ResponseModel<T> Post(T newCat, string procName, string connectionString)
        {
            ResponseModel<int> res = new ResponseModel<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    conn.Open();
                    var item = typeof(T).GetProperties();

                    for (int i = 0; i < typeof(T).GetProperties().Count(); i++)
                    {
                        var rrr = item.GetValue(i); ;
                       
                        if (!(item[i].Name == "Id" || item[i].Name == "ID" || item[i].Name == "id"))
                        {
                            cmd.Parameters.AddWithValue("@"+item[i].Name+"", "TEEEEEEEEESt");

                        }
                    }
                    //    foreach (var item in typeof(Category).GetProperties())
                    //{
                    //    var aawsaa = item;
                    //    if (!(item.Name == "Id" || item.Name == "ID" || item.Name == "id"))
                    //    {
                    //        cmd.Parameters.AddWithValue("@" + item.Name + "", "item.GetValue()");

                    //    }
                    //}

                    //var a = typeof(Category).GetProperties().Count();
                    //var b = typeof(Category).GetProperties();

                    //conn.Open();

                    //SqlCommand cmd = new SqlCommand(procName, conn);

                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Id", newCat.Id);
                    //cmd.Parameters.AddWithValue("@Name", "newCat.Name");

                    //SqlDataReader reader = cmd.ExecuteReader();
                    var aaa = cmd.ExecuteScalar();

                    res.message = "Add Successfully";
                    res.code = 200;

                    //reader.Close();
                }
                catch (Exception ex)
                {
                    res.message = ex.Message;
                    res.code = 500;
                    if (ex is SqlException sqlException)
                    {
                        //sqlException.InnerException.InnerException is U
                    }
                    throw;
                }

                T t = new T();

                return null;
            }
        }

    }
}
