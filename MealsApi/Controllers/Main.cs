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

        public ResponseModel<List<Category>> GetAllCategories() =>
            categoryController.GetAll(GeneralQuery.selectAllRecords("Categoris"), _connectionString);

        //public ResponseModel<int> PostCategories() =>
        //    categoryController.GetAll(GeneralQuery.selectAllRecords("Categoris"), _connectionString);

        public async Task<ResponseModel<int>> PostCategory(Category newCat)=>
            await Task.FromResult(categoryController.Post(newCat, GeneralData.InsertCategoryWithReturnProc, _connectionString));

        public async Task<ResponseModel<int>> PutCategory(Category newCat) =>
            await Task.FromResult(categoryController.Put(newCat, GeneralData.UpdateCategoryProc, _connectionString));

        public async Task<ResponseModel<int>> DeleteCategory(int Id) =>
            await Task.FromResult(categoryController.Delete(Id, GeneralData.DeleteCategoryProc, _connectionString));

    }
}
