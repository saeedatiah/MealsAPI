namespace MealsApi.Utility
{
    public class Response<T> 
    {
        public T data { get; set; } 
        public string message { get; set; } = "Successfully";
        public int code { get; set; } = 200;
    }
}
