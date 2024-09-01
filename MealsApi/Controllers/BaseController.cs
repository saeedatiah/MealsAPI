using MealsApi.Models;
using MealsApi.Services;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace MealsApi.Controllers
{
    public class BaseController<T> 
    {
        public  ResponseModel<List<T>> GetAll(string sqlProc, string connectionString)
        {
            List<T> items = new List<T>();
            ResponseModel<List<T>> res = new ResponseModel<List<T>>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlProc, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dynamic item = new System.Dynamic.ExpandoObject(); 

                        foreach (var prop in typeof(T).GetProperties())
                        {
                            if(prop.PropertyType.Name=="Int32")
                            {
                                item.Id = Convert.ToInt32(reader[prop.Name]);
                            }
                            else if(prop.PropertyType.Name == "String")
                            {
                                item.Name = Convert.ToString(reader[prop.Name]);
                            }
                        }

                        T finalItem = item;
                        //cat.Id = Convert.ToInt32(reader["Id"]);
                        //cat.Name = Convert.ToString(reader["Name"]);
                        items.Add(item);
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
