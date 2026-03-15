using System.Reflection;
using InvoiceAPI.CQRS.Base.Command;
using InvoiceAPI.CQRS.Base.Query;
using InvoiceAPI.CQRS.Command;
using InvoiceAPI.Data;
using InvoiceAPI.Data.BaseRepo;
using InvoiceAPI.Entity;
using InvoiceAPI.Repositories;
using InvoiceAPI.Repositories.Interfaces;
using InvoiceAPI.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(typeof(AddItemCommand).Assembly));

// Repositories
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddScoped<IBaseRepository<Customer>, BaseRepository<Customer>>();
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Generic CQRS Handlers
builder.Services.AddScoped<IRequestHandler<AddCommand<Customer>, Customer>, AddCommandHandler<Customer>>();
builder.Services.AddScoped<IRequestHandler<GetAllQuery<Customer>, IEnumerable<Customer>>, GetAllQueryHandler<Customer>>();
builder.Services.AddTransient(typeof(IRequestHandler<DeleteCommand<Customer>, bool>), typeof(DeleteCommandHandler<Customer>));
builder.Services.AddTransient(typeof(IRequestHandler<UpdateCommand<Customer>, Customer>), typeof(UpdateCommandHandler<Customer>));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


// ⭐ Updated Swagger Configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "🧾 InvoiceHub API – Invoice Management System",
        Version = "v1",
        Description = "Invoice Management Backend API built with ASP.NET Core (.NET 8) using CQRS architecture."
    });
});

// Database
builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
));

var app = builder.Build();

// Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "🧾 InvoiceHub API v1");
    });


app.UseCors("AllowReactApp");

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();