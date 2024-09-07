using MealsApi.Models;
using MealsApi.Utility;
using MealsApi.ViewModel;
using System.Data.SqlClient;
using System.Data;
using MealsApi.Translator;

namespace MealsApi.Repository
{
    public class CategoryDbClient
    {

        public List<Category> GetAllCategory(string connString)
        {
            return SqlHelper.ExtecuteProcedureOrQueryReturnData<List<Category>>(connString,
                "GetAllCategories", r => r.TranslateAsCategoriesList());
        }

        public Category SaveCategory(Category model, string connString, bool isEdit)
        {
            SqlParameter Idreturn;
            if (!isEdit)
            {
                Idreturn = new SqlParameter("@new_id", SqlDbType.Int);
                Idreturn.Direction = ParameterDirection.Output;
            }
            else
                Idreturn = new SqlParameter("@Id", model.Id);


            SqlParameter[] param = {
                Idreturn,
                new SqlParameter("@Name",model.Name),
            };
            SqlHelper.ExecuteProcedure(connString, isEdit ? "UpdateCategory" : "InsertCategory", param);

            return new Category()
            {
                Id = isEdit ? model.Id : int.Parse(Idreturn.Value.ToString()),
                Name = model.Name,
            };
        }

        public void DeleteCategory(int Id, string connString)
        {

            SqlParameter[] param = {
                new SqlParameter("@Id",Id)
            };
            SqlHelper.ExecuteProcedure(connString, "DeleteCategory", param);
        }
    }
}
