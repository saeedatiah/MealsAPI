﻿namespace MealsApi.Services
{
    public class ResponseModel<T>
    {
        public T data { get; set; }
        public int code { get; set; }
        public string message { get; set; }
    }
}
