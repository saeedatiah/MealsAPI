using MealsApi.Models;
using MealsApi.Utility;
using MealsApi.ViewModel;
using System.Data.SqlClient;

namespace MealsApi.Translator
{
    public static class CategoryTrans
    {
        public static Category TranslateAsCategory(SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new Category();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("Name"))
                item.Name = SqlHelper.GetNullableString(reader, "Name");

            return item;
        }
        public static List<Category> TranslateAsCategoriesList(this SqlDataReader reader)
        {
            var list = new List<Category>();
            while (reader.Read())
            {
                list.Add(TranslateAsCategory(reader, true));
            }
            return list;
        }
    }
}
