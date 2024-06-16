using Microsoft.EntityFrameworkCore;
using NorthwindModelClassLibrary;
using ProductsAPI.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connStr = builder.Configuration.GetConnectionString("ProductsDbConnection");

builder.Services
    .Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"))
    .AddDbContext<ProductsDbContextcs>(options => options.UseSqlServer(connStr))
    .AddScoped<IRepository<Product>, ProductEFRepository>()
    .AddControllers();

builder.Services.AddEndpointsApiExplorer()
    .AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin())
    .UseMiddleware<CustomAuthMiddleware>();

app.MapControllers();
app.Run();
