using MealsApi.Models;
using System.Data.SqlClient;


namespace MealsApi.Controllers
{
    public class CategoryController
    {
        public List<Category> GetAll(string sqlQuery, string connectionString)
        {
            List<Category> cats = new List<Category>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category cat = new Category();
                    cat.Id = Convert.ToInt32(reader["Id"]);
                    cat.Name = Convert.ToString(reader["Name"]);
                    cats.Add(cat);
                }
                reader.Close();
                return cats;
            }
        }
    }
}
