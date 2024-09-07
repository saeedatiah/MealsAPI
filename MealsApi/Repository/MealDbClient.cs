using MealsApi.Translator;
using MealsApi.Utility;
using MealsApi.ViewModel;
using System.Data.SqlClient;
using System.Data;
using MealsApi.Models;

namespace MealsApi.Repository
{
    public class MealDbClient
    {
        public List<MealVM> GetAllMeals(string connString)
        {
            return SqlHelper.ExtecuteProcedureOrQueryReturnData<List<MealVM>>(connString,
                "GetAllMeals", r => r.TranslateAsMealsList());
        }

        public MealVM SaveMeal(Meal model, string connString, bool isEdit)
        {
            SqlParameter Idreturn;
            if (!isEdit)
            {
                Idreturn = new SqlParameter("@new_id", SqlDbType.Int);
                Idreturn.Direction = ParameterDirection.Output;
            }
            else
                Idreturn = new SqlParameter("@Id", model.Id);

            SqlParameter catNameReturn = new SqlParameter("@catName", SqlDbType.VarChar, 50);
            catNameReturn.Direction = ParameterDirection.Output;

            SqlParameter[] param = {
                Idreturn,
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Descr",model.Descr),
                new SqlParameter("@ImgURL",model.ImgURL),
                new SqlParameter("@Price",model.Price),
                new SqlParameter("@CatID",model.CatID),
                catNameReturn
            };
            SqlHelper.ExecuteProcedure(connString, isEdit ? "UpdateMeal" : "InsertMeal", param);

            return new MealVM()
            {
                Id = isEdit ? model.Id : int.Parse(Idreturn.Value.ToString()),
                Name = model.Name,
                Descr = model.Descr,
                ImgURL = model.ImgURL,
                Price = model.Price,
                CatID = model.CatID,
                CatName = catNameReturn.Value.ToString()
            };
        }

        public void DeleteMeal(int Id, string connString)
        {

            SqlParameter[] param = {
                new SqlParameter("@Id",Id)
            };
            SqlHelper.ExecuteProcedure(connString, "DeleteMeal", param);
        }

    }
}
