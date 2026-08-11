using SmartStore.API.Repository.Interfaces;
using SmartStore.API.Repository.Implementation;
using Microsoft.EntityFrameworkCore;
using SmartStore.API.Data;
using SmartStore.API.NHibernate;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<SmartStoreDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("SmartStoreConnectionString"));
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IDapperProductRepository, DapperProductRepository>();
builder.Services.AddScoped<
    INHibernateProductRepository,
    NHibernateProductRepository>();

builder.Services.AddSingleton<NHibernateSessionFactory>();


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
