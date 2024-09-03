using FastMember;
using System.Data;
using System.Data.SqlClient;

namespace MealsApi.Services
{
    public static class GeneralData
    {
        public static string InsertCategoryProc { get; } = "InsertCategory";
        public static string InsertCategoryWithReturnProc { get; } = "InsertCategoryWithReturn";
        public static string UpdateCategoryProc { get; } = "UpdateCategory";
        public static string DeleteCategoryProc { get; } = "DeleteCategory";

        

        public static T ConvertToObject<T>(SqlDataReader rd) where T : class, new()
        {
            Type type = typeof(T);
            var accessor = TypeAccessor.Create(type);
            var members = accessor.GetMembers();
            var t = new T();

            for (int i = 0; i < rd.FieldCount; i++)
            {
                if (!rd.IsDBNull(i))
                {
                    string fieldName = rd.GetName(i);

                    if (members.Any(m => string.Equals(m.Name, fieldName, StringComparison.OrdinalIgnoreCase)))
                    {
                        accessor[t, fieldName] = rd.GetValue(i);
                    }
                }
            }

            return t;
        }


    }
}
