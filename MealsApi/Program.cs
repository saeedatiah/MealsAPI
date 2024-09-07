using MealsApi.Controllers;
using MealsApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetSection("ConnectionStrings")["MealsApi_Db"].ToString();

MealController mealController = new MealController(connectionString);
CategoryController categoryController = new CategoryController(connectionString);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGet("/api/GetAllCategories", () => categoryController.GetAllCategoriess());
app.MapPost("/api/PostCategory", (Category category) => categoryController.SaveCategory(category));
app.MapPut("/api/PutCategory", (Category category) => categoryController.SaveCategory(category,true));
app.MapDelete("/api/DeleteCategory", (int Id) => categoryController.DeleteCategory(Id));


app.MapGet("/api/GetAllMeals", () => mealController.GetAllMeals());
app.MapPost("/api/PostMeal", (Meal meal) => mealController.SaveMeal(meal));
app.MapPut("/api/PutMeal", (Meal meal) => mealController.SaveMeal(meal,true));
app.MapDelete("/api/DeleteMeal", (int Id) => mealController.DeleteMeal(Id));

//app.MapPost("/api/PostMeal", () => mealController.SaveMeal());




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
