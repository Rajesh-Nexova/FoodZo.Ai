
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FoodZoaiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAppsettingRepository, AppsettingRepository>();
builder.Services.AddScoped<IAppsetting, AppsettingMapper>();


var app = builder.Build();




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
