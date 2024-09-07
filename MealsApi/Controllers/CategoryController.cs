using MealsApi.Models;
using MealsApi.Repository;
using MealsApi.Services;
using MealsApi.Utility;
using MealsApi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace MealsApi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly string _connectionString;

        public CategoryController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Response<List<Category>> GetAllCategoriess()
        {
            try
            {
                List<Category> data = DbClientFactory<CategoryDbClient>.Instance.GetAllCategory(_connectionString);
                return new Response<List<Category>>() { data = data };
            }
            catch (Exception ex)
            {
                //Remind handling exeptions
                return new Response<List<Category>>() { data = new List<Category>(), message = ex.Message, code = 500 };
            }
        }

        public Response<Category> SaveCategory([FromBody] Category model, bool isEdit = false)
        {
            try
            {
                Category data = DbClientFactory<CategoryDbClient>.Instance.SaveCategory(model, _connectionString, isEdit);
                return new Response<Category>() { data = data, message = isEdit ? "Edit Successfully" : "Add Successfully" };
            }
            catch (Exception ex)
            {
                //Remind handling exeptions
                return new Response<Category>() { message = ex.Message, code = 500 };

            }

        }
        public Response<int> DeleteCategory([FromBody] int Id)
        {
            try
            {
                DbClientFactory<CategoryDbClient>.Instance.DeleteCategory(Id, _connectionString);
                return new Response<int>() { data = Id, message = "Delete Successfully" };
            }
            catch (Exception ex)
            {
                //Remind handling exeptions
                return new Response<int>() { message = ex.Message, code = 500 };

            }

        }
    }

    
}
