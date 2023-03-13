using FatecLibrary.BookAPI.Context;
using FatecLibrary.BookAPI.Repositories.Entities;
using FatecLibrary.BookAPI.Repositories.Interfaces;
using FatecLibrary.BookAPI.Services.Entities;
using FatecLibrary.BookAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//pegando a string de conexão
var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

//Usar para que o Entity Framework crie nossas tabelas no banco de dados
builder.Services.AddDbContext<AppDBContext>(options =>
    options.UseMySql(mySqlConnection,
    ServerVersion.AutoDetect(mySqlConnection))
    );

//Garantir que todos os assemblies do domain sejam injetados
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Criando a injeção de dependência
builder.Services.AddScoped<IPublishingRepository, PublishingRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IPublishingService, PublishingService>();
builder.Services.AddScoped<IBookService, BookService>();

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
