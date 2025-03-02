
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using JohPlaxLibraryAPI.Models;
using JohPlaxLibraryAPI.Interfaces;
using JohPlaxLibraryAPI.Services;

namespace JohPlaxLibraryAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Add CORS
            builder.Services.AddCors();
            // Adding json serialization 
            builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Bind Database settings from the appSettings.json file to the model
            builder.Services.Configure<JohPlaxLibraryDatabaseSettings>(
                builder.Configuration.GetSection("JohPlaxLibraryDatabaseSettings"));

            // Create mongoDB client
            builder.Services.AddSingleton<IMongoClient>(_ =>
            {
                var connectionString = builder.Configuration.GetSection("JohPlaxLibraryDatabaseSettings")?.Value;
                return new MongoClient(connectionString);
            });

            // Register IServices and services
            builder.Services.AddSingleton<IUsersService, UsersService>();
            builder.Services.AddSingleton<IOrdersService, OrdersService>();
            builder.Services.AddSingleton<IBooksService, BooksService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(options => options
                .WithOrigins(
                    new string[]{
                        "http://localhost:3000"
                       }
                )
            );

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
