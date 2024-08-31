using MealsApi.Models;
using MealsApi.Services;
using System.Data;
using System.Data.SqlClient;


namespace MealsApi.Controllers
{
    public class CategoryController
    {
        public ResponseModel<List<Category>> GetAll(string sqlQuery, string connectionString)
        {
            List<Category> cats = new List<Category>();
            ResponseModel<List<Category>> res = new ResponseModel<List<Category>>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Category cat = new Category();
                        cat.Id = Convert.ToInt32(reader["Id"]);
                        cat.Name = Convert.ToString(reader["Name"]);
                        cats.Add(cat);
                    }
                    reader.Close();
                    res.data= cats;
                    res.code = 200;
                    res.message= "OK";
                    return res;
                }
                catch (Exception)
                {
                    res.data = null;
                    res.code = 500;
                    res.message= "server error";
                    return res;

                }



            }
        }
        public ResponseModel<int> Post(Category newCat, string procName, string connectionString)
        {
            ResponseModel<int> res=new ResponseModel<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    //cmd.Parameters.AddWithValue("@Id", newCat.Id);
                    cmd.Parameters.AddWithValue("@Name", newCat.Name);

                    SqlDataReader reader = cmd.ExecuteReader();

                    res.message = "Add Successfully";
                    res.code = 200;
                    
                    reader.Close();
                }
                catch (Exception ex)
                {
                    res.message = "Server Error";
                    res.code = 500;
                    if (ex is SqlException sqlException)
                    {
                        //sqlException.InnerException.InnerException is U
                    }
                    throw;
                }


                return  res;
            }
        }

        public ResponseModel<int> Put(Category newCat, string procName, string connectionString)
        {
            ResponseModel<int> res = new ResponseModel<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", newCat.Id);

                    cmd.Parameters.AddWithValue("@Name", newCat.Name);
                    SqlDataReader reader = cmd.ExecuteReader();
                    res.message = "Update Successfully";
                    res.code = 200;

                    reader.Close();
                }
                catch (Exception ex)
                {
                    res.message = "Server Error";
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

        public ResponseModel<int> Delete(int Id, string procName, string connectionString)
        {
            ResponseModel<int> res = new ResponseModel<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id",Id);

                    SqlDataReader reader = cmd.ExecuteReader();
                    res.message = "Delete Successfully";
                    res.code = 200;

                    reader.Close();
                }
                catch (Exception ex)
                {
                    res.message = "Server Error";
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
