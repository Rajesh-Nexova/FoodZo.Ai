
using FoodZOAI.UserManagement.Configuration;
using FoodZOAI.UserManagement.Configuration.Contracts;
using FoodZOAI.UserManagement.Configuration.Mappers;
using FoodZOAI.UserManagement.Contracts;
using FoodZOAI.UserManagement.Models;
using FoodZOAI.UserManagement.Repositories.Contracts;
using FoodZOAI.UserManagement.Repositories;
using FoodZOAI.UserManagement.Repository;
using FoodZOAI.UserManagement.Services.Contracts;
using FoodZOAI.UserManagement.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FoodZoaiContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// Database Configuration
builder.Services.AddDbContext<FoodZoaiContext>(options =>
{
	// For development, use SQL Server LocalDB or In-Memory database
	if (builder.Environment.IsDevelopment())
	{
		

		// Option 1: SQL Server (uncomment to use)
		 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	}
	else
	{
		// Production database configuration
		options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
	}

	// Enable sensitive data logging in development
	if (builder.Environment.IsDevelopment())
	{
		options.EnableSensitiveDataLogging();
	}
});




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Title = "User Management API",
		Version = "v1",
		Description = "A comprehensive API for managing User and Role Management",
		Contact = new Microsoft.OpenApi.Models.OpenApiContact
		{
			Name = "FoodZOAI Development Team",
			Email = "prady.r@nexovaglobaltechnology.com"
		}
	});

	// Include XML comments
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	if (File.Exists(xmlPath))
	{
		c.IncludeXmlComments(xmlPath);
	}
});

//Dependency Injection for Mappers
builder.Services.AddMappers();
//Dependency Injection for Services
builder.Services.AddConfigServices();
//Dependency Injection for Repository
builder.Services.AddRepositoryServices();
//Dependency Injection for FileStorage
builder.Services.AddFileStorage(builder.Configuration);



// Add CORS policy
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin()
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});

// Configure JSON options
builder.Services.ConfigureHttpJsonOptions(options =>
{
	options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
	options.SerializerOptions.WriteIndented = true;
});



builder.Services.AddScoped<FoodZOAI.UserManagement.Contracts.IEmailSettingRepository, FoodZOAI.UserManagement.Repository.EmailSettingRepository>();
builder.Services.AddScoped<FoodZOAI.UserManagement.Contracts.IEmailTemplateRepository, FoodZOAI.UserManagement.Repository.EmailTemplateRepository>();

builder.Services.AddScoped<FoodZOAI.UserManagement.Services.Contract.IEmailSMTPSettingService, FoodZOAI.UserManagement.Services.Implementation.EmailSMTPSettingService>();
builder.Services.AddScoped<FoodZOAI.UserManagement.Services.Contract.IEmailTemplateService, FoodZOAI.UserManagement.Services.Implementation.EmailTemplateService>();

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
