namespace MealsApi.Controllers
{
    public class Base
    {
        private readonly string _connectionString;

        public Base(string connectionString)
        {
            _connectionString = connectionString;
        }

    }
}
