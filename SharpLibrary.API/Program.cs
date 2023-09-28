using Microsoft.Data.SqlClient;
using SharpLibrary.BusinessLayer.IServices;
using SharpLibrary.BusinessLayer.Services;
using SharpLibrary.DataAccess.Interfaces;
using SharpLibrary.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton(new SqlConnection(connectionString));
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IBookService, BookService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

