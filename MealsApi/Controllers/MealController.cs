using MealsApi.Models;
using MealsApi.Services;
using MealsApi.ViewModel;
using System.Data.SqlClient;

namespace MealsApi.Controllers
{
    public class MealController 
    {
        public ResponseModel<int> Post(Category newCat, string procName, string connectionString)
        {
            ResponseModel<int> res = new ResponseModel<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    var a = typeof(Category).GetProperties().Count();
                    var b = typeof(Category).GetProperties();

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Id", newCat.Id);
                    cmd.Parameters.AddWithValue("@Name", newCat.Name);

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


                return res;
            }
        }
    }
}
