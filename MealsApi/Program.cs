using MealsApi.Controllers;
using MealsApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetSection("ConnectionStrings")["MealsApi_Db"].ToString();

Main main = new Main(connectionString);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.MapGet("/api/GetAllCategories", () => main.GetAllCategories());
app.MapPost("/api/InsertCategory", (Category cat) => main.PostCategory(cat));
app.MapPut("/api/UpdateCategory", (Category cat) => main.PutCategory(cat));
app.MapDelete("/api/DeleteCategory", (int Id) => main.DeleteCategory(Id));



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
