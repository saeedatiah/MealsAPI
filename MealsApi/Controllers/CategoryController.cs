using MealsApi.Models;
using MealsApi.Services;
using System.Data;
using System.Data.SqlClient;


namespace MealsApi.Controllers
{
    public class CategoryController 
    {
        public ResponseModel<int> Post(Category newCat, string procName, string connectionString)
        {
            ResponseModel<int> res=new ResponseModel<int>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(procName, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    foreach (var item in typeof(Category).GetProperties())
                    {
                        if (!(item.Name == "Id" || item.Name == "ID" || item.Name == "id"))
                        {
                            cmd.Parameters.AddWithValue("@"+ item.Name+ "", newCat.Name);

                        }
                    }
                     
                    var a= typeof(Category).GetProperties().Count();
                    var b= typeof(Category).GetProperties();

                    conn.Open();

                    //SqlCommand cmd = new SqlCommand(procName, conn);

                    //cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

        public ResponseModel<int> Delete(int Id, string procName, string connectionString)
        {
            ResponseModel<int> res = new ResponseModel<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Categoris WHERE Id=@Id", conn);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    int count= cmd.ExecuteNonQuery();
                    if (count > 0) 
                    {
                        res.message = "Delete Successfully";
                        res.code = 200;
                    }
                    else
                    {
                        res.message = "Not Found";
                        res.code = 404;

                    }
                }
                catch (Exception ex)
                {
                    res.message = ex.Message;
                    res.code = 500;
                }

            }


            //ResponseModel<int> res = new ResponseModel<int>();

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    try
            //    {
            //        conn.Open();
            //        SqlCommand cmd = new SqlCommand(procName, conn);
            //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
            //        cmd.Parameters.AddWithValue("@Id",Id);

            //        SqlDataReader reader = cmd.ExecuteReader();
            //        res.message = "Delete Successfully";
            //        res.code = 200;

            //        reader.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        res.message = "Server Error";
            //        res.code = 500;
            //        if (ex is SqlException sqlException)
            //        {
            //            //sqlException.InnerException.InnerException is U
            //        }
            //        throw;
            //    }


                return res;
            }
        }

    
}
