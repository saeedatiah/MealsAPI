using MealsApi.Utility;
using MealsApi.ViewModel;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Data.SqlClient;

namespace MealsApi.Translator
{
    public static class MealVMTrans
    {
        public static MealVM TranslateAsMealVM( SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new MealVM();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("Name"))
                item.Name = SqlHelper.GetNullableString(reader, "Name");

            if (reader.IsColumnExists("Descr"))
                item.Descr = SqlHelper.GetNullableString(reader, "Descr");

            if (reader.IsColumnExists("ImgURL"))
                item.ImgURL = SqlHelper.GetNullableString(reader, "ImgURL");

            if (reader.IsColumnExists("Price"))
                item.Price = SqlHelper.GetNullableFloat(reader, "Price");

            if (reader.IsColumnExists("CatID"))
                item.CatID = SqlHelper.GetNullableInt32(reader, "CatID");

            if (reader.IsColumnExists("CatName"))
                item.CatName = SqlHelper.GetNullableString(reader, "CatName");

            return item;
        }
        public static List<MealVM> TranslateAsMealsList(this SqlDataReader reader)
        {
            var list = new List<MealVM>();
            while (reader.Read())
            {
                list.Add(TranslateAsMealVM(reader, true));
            }
            return list;
        }

    }
        
}
