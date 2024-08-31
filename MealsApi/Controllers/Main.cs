using MealsApi.Models;
using MealsApi.Services;

namespace MealsApi.Controllers
{
    public class Main
    {
        private readonly string _connectionString;
        CategoryController categoryController = new CategoryController();

        public Main(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Category> GetAllCategories() =>
            categoryController.GetAll(GeneralQuery.selectAllRecords("Categoris"), _connectionString);


        
    }
}
