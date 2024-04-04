using GroceryProductAPI.Models;
using GroceryProductAPI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CORS configuration
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "AllowOrigin", builder =>
    {
        builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

//Connect to databse
var connectionString = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Connection2RDSLocal"));
var userId = builder.Configuration.GetSection("MySettings").GetSection("Username").Value;
var password = builder.Configuration.GetSection("MySettings").GetSection("Password").Value;
connectionString.UserID = userId;
connectionString.Password = password;
builder.Services.AddDbContext<GroceryProductContext>(opt => opt.UseSqlServer(connectionString.ConnectionString));

//AutoMapper configuration
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//AddScope
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowOrigin");

app.MapControllers();

app.Run();
