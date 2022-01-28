using DL;
using BL;
using Serilog;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(@"..\DL\SeriLog.txt")
    .CreateLogger();

// Add services to the container.

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddLogging(); //Ilogger?
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//To use ef repo or something and point to new connection string
builder.Services.AddDbContext<DbContext, CSDBContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("PostgreCSDB")
));


//Registering our deps here for dependency injection
//Different ways to add: Add, AddScoped, Singleton (for whole life of app), Transient (every time called creates new instance)

//Azure/AWS code
builder.Services.AddScoped<IRepo>(ctx => new DBRepo
(builder.Configuration.GetConnectionString("CSDB")));
builder.Services.AddScoped<IBL, CSBL>();

//ORM code
builder.Services.AddScoped<IStoreRepo, EFRepo>();
builder.Services.AddScoped<IStoreBL, ClayStoreBL>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
