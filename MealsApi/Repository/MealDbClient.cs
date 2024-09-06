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

        public string SaveMeal(Meal model, string connString)
        {
           
            //var outParam1 = new SqlParameter("@Id", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //var outParam2 = new SqlParameter("@Name", SqlDbType.VarChar,50)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //var outParam3 = new SqlParameter("@Descr", SqlDbType.VarChar, 150)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //var outParam4 = new SqlParameter("@ImgURL", SqlDbType.VarChar,-1)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //var outParam5 = new SqlParameter("@Price", SqlDbType.Float)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //var outParam6 = new SqlParameter("@CatID", SqlDbType.Int)
            //{
            //    Direction = ParameterDirection.Output
            //};
            //var outParam7 = new SqlParameter("@CatName", SqlDbType.VarChar,50)
            //{
            //    Direction = ParameterDirection.Output
            //};

            SqlParameter[] param = {
                //new SqlParameter("@Id",model.Id),
                new SqlParameter("@Name",model.Name),
                new SqlParameter("@Descr",model.Descr),
                new SqlParameter("@ImgURL",model.ImgURL),
                new SqlParameter("@Price",model.Price),
                new SqlParameter("@CatID",model.CatID),
                //outParam1,
                //outParam2,
                //outParam3,
                //outParam4,
                //outParam5,
                //outParam6,
                //outParam7,
            };
            SqlHelper.ExecuteProcedureReturnString(connString, "InsertMeal", param);
            return "";
            //return (string)outParam.Value;
        }
    }
}
