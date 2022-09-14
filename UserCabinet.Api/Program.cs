using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using UserCabinet.Api.Extensions;
using UserCabinet.Api.Middlewares;
using UserCabinet.Data.DbContexts;
using UserCabinet.Service.Helpers;
using UserCabinet.Service.Mappers;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserCabinetDbContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("UserCabinetDb")));

builder.Services.AddCustomServices();
builder.Services.AddJwtService(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MapperProfile));

var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Logging.AddLog4Net("log4net.config");
builder.Logging.SetMinimumLevel(LogLevel.Error);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

EnvironmentHelper.WebRootPath = app.Services.GetService<IWebHostEnvironment>()?.WebRootPath;

app.UseMiddleware<UserExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();