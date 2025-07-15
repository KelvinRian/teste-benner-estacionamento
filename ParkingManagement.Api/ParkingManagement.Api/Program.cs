
using ParkingManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using ParkingManagement.Domain.RepositoryInterfaces;
using ParkingManagement.Infrastructure.Repositories;
using ParkingManagement.Domain.Facades;
using ParkingManagement.Domain.Services;

namespace ParkingManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ParkingManagementDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPriceRepository, PriceRepository>();
            builder.Services.AddScoped<IParkingSessionRepository, ParkingSessionRepository>();

            builder.Services.AddScoped<IPriceFacade, PriceFacade>();
            builder.Services.AddScoped<IParkingSessionFacade, ParkingSessionFacade>();

            builder.Services.AddScoped<IPaymentCalculatorService, PaymentCalculatorService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
