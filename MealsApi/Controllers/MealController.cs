using MealsApi.Models;
using MealsApi.Repository;
using MealsApi.Services;
using MealsApi.Utility;
using MealsApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MealsApi.Controllers
{
    public class MealController:Controller
    {
        private readonly string _connectionString;

        public MealController(string connectionString)
        {
            _connectionString = connectionString;
        }
        //[HttpGet]
        //[Route("GetAllMealsss")]
        public Response<List<MealVM>> GetAllMeals()
        {
            try
            {
                List<MealVM> data = DbClientFactory<MealDbClient>.Instance.GetAllMeals(_connectionString);
                return new Response<List<MealVM>>() { data = data };
                //Remind handling exeptions
            }
            catch (Exception ex)
            {
                return new Response<List<MealVM>>() { data = new List<MealVM>(),message=ex.Message,code=500 };
            }
        }

        public IActionResult SaveMeal([FromBody] Meal model)
        {
            var msg = new Response<Meal>();
            var data = DbClientFactory<MealDbClient>.Instance.SaveMeal(model, _connectionString);
            
           
            return Ok(msg);
        }




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
