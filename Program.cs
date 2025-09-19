using ActividadPractica3.Controllers;
using ActividadPractica3.Data.Repositories;
using ActividadPractica3.DTOs;
using ActividadPractica3.Models;
using ActividadPractica3.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore; // Asegúrate de tener este using

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryFactura, RepositoryFactura>();
builder.Services.AddScoped<IServiceFactura, ServiceFactura>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
    var config = (mapper.ConfigurationProvider as MapperConfiguration);
    config.AssertConfigurationIsValid(); // lanza excepción si falta algún CreateMap
}
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
