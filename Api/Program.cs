using Api.Services.Implementations;
using Api.Services.Interfaces;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            Log.Information("Api is up");
            try
            {
                var builder = WebApplication.CreateBuilder();
                builder.Host.UseSerilog((hbc, conf) =>
                {
                    conf.MinimumLevel.Information()
                        .WriteTo.Console()
                        .MinimumLevel.Information();
                });
                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                var connStr = "Server = (localdb)\\Teledok; Database = TeleDoc; User Id = admin; Password = 12345;";
                builder.Services.AddDbContext<SqlExpressDbContext>(
                    options => options.UseSqlServer(
                        connStr,
                        b => b.MigrationsAssembly("Api")));
                builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlExpressRepository<>));
                builder.Services.AddScoped<IClientRepository, ClientRepository>();
                builder.Services.AddScoped<IFounderRepository, FounderRepository>();
                builder.Services.AddScoped<IClientService, ClientService>();
                builder.Services.AddScoped<IFounderService, FounderService>();
                builder.Services.AddCors();
                builder.Services.AddHttpLogging(options =>
                {
                    options.LoggingFields = HttpLoggingFields.RequestHeaders
                                            | HttpLoggingFields.ResponseHeaders
                                            | HttpLoggingFields.RequestBody
                                            | HttpLoggingFields.ResponseBody;
                });

                var app = builder.Build();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;
                });
                app.UseCors(policy =>
                {
                    policy
                        .AllowCredentials()
                        .AllowAnyMethod()
                        .WithOrigins("https://localhost:7007", "http://localhost:5050")
                        .AllowAnyHeader();
                });
                app.UseHttpLogging();
                app.MapControllers();
                await app.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Unexpected error");
            }
            finally
            {
                Log.Information("Server shutting down");
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
