using MealsApi.Models;
using MealsApi.Services;
using MealsApi.ViewModel;

namespace MealsApi.Controllers
{
    public class Main
    {
        private readonly string _connectionString;
        CategoryController categoryController = new CategoryController();
        MealController mealController = new MealController();

        public Main(string connectionString)
        {
            _connectionString = connectionString;
        }
        //categories
        public ResponseModel<List<Category>> GetAllCategories() =>
            BaseController<Category>.GetAll(GeneralQuery.selectAllRecords("Categoris"), _connectionString);

        //public ResponseModel<int> PostCategories() =>
        //    categoryController.GetAll(GeneralQuery.selectAllRecords("Categoris"), _connectionString);

        //public async Task<ResponseModel<int>> PostCategory(Category newCat)=>
        //    await Task.FromResult(categoryController.Post(newCat, GeneralData.InsertCategoryWithReturnProc, _connectionString));

        public async Task<ResponseModel<Category>> PostCategory(Category newCat) =>
            await Task.FromResult(BaseController<Category>.Post(newCat, GeneralData.InsertCategoryWithReturnProc, _connectionString));


        public async Task<ResponseModel<int>> PutCategory(Category newCat) =>
            await Task.FromResult(categoryController.Put(newCat, GeneralData.UpdateCategoryProc, _connectionString));

        public async Task<ResponseModel<int>> DeleteCategory(int Id) =>
            await Task.FromResult(categoryController.Delete(Id, GeneralData.DeleteCategoryProc, _connectionString));


        //Meals

        public ResponseModel<List<MealView>> GetAllMeals() =>
            BaseController<MealView>.GetAll(GeneralData.GetAllMealsVMProc, _connectionString,true);


    }
}
