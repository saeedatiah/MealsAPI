using System.ComponentModel.DataAnnotations;

namespace MealsApi.Models
{
    public class Meal
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descr { get; set; }
        public string ImgURL { get; set; }
        public float Price { get; set; } = 0;
        public int CatID { get; set; }
    }
}
