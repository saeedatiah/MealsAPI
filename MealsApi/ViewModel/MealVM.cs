using MealsApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MealsApi.ViewModel
{
    public class MealVM 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descr { get; set; }
        public string ImgURL { get; set; }
        public float Price { get; set; } = 0;
        public int CatID { get; set; }
        public string CatName { get; set; }
    }
}
