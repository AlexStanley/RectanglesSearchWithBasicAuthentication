using Microsoft.EntityFrameworkCore;
using RectanglesSearch.Api.Data;
using RectanglesSearch.Api.Middlewares;
using RectanglesSearch.Api.Models;
using RectanglesSearch.Api.Repositories;
using RectanglesSearch.Api.Services;

namespace RectanglesSearch.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container
            builder.Services.AddDbContext<RectangleDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IRectangleRepository, RectangleRepository>();
            builder.Services.AddScoped<IRectangleService, RectangleService>();
            builder.Services.AddScoped<UserRepository>();

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

            InitializationDbWithData.PrepPopulation(app);
            InitializationDbWithUserData.PrepPopulation(app);

            app.UseMiddleware<BasicAuthenticationMiddleware>();


            app.MapControllers();

            app.Run();
        }
    }
}