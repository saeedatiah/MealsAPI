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

        public Response<List<MealVM>> GetAllMeals()
        {
            try
            {
                List<MealVM> data = DbClientFactory<MealDbClient>.Instance.GetAllMeals(_connectionString);
                return new Response<List<MealVM>>() { data = data };
            }
            catch (Exception ex)
            {
                //Remind handling exeptions
                return new Response<List<MealVM>>() { data = new List<MealVM>(),message=ex.Message,code=500 };
            }
        }

        public Response<MealVM> SaveMeal([FromBody] Meal model , bool isEdit=false)
        {
            try
            {
                MealVM data = DbClientFactory<MealDbClient>.Instance.SaveMeal(model, _connectionString, isEdit);
                return new Response<MealVM>() { data=data,message= isEdit ?"Edit Successfully": "Add Successfully" };
            }
            catch (Exception ex)
            {
                //Remind handling exeptions
                return new Response<MealVM>() {  message = ex.Message,code=500 };

            }

        }
        public Response<int> DeleteMeal([FromBody] int Id)
        {
            try
            {
                DbClientFactory<MealDbClient>.Instance.DeleteMeal(Id, _connectionString);
                return new Response<int>() { data=Id,message="Delete Successfully" };
            }
            catch (Exception ex)
            {
                //Remind handling exeptions
                return new Response<int>() {  message = ex.Message,code=500 };

            }

        }

    }
}
