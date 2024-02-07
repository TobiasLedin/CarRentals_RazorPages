using FribergCarRentals.DataAccess.Database_Contexts;
using FribergCarRentals.DataAccess.Repositories;
using FribergCarRentals.Interfaces;
using FribergCarRentals.Services;
using Microsoft.EntityFrameworkCore;

namespace FribergCarRentals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();

            // Local memory for session state
            builder.Services.AddMemoryCache();

            // Session state functions
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "FribergCarRentals";
                options.IdleTimeout = TimeSpan.FromSeconds(15);
                options.Cookie.IsEssential = true;
                options.Cookie.HttpOnly = true;
            });

            // Database context
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(new ConfigurationBuilder()
            .AddJsonFile("appsettings.Development.json")
            .Build()
            .GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddTransient<IAdminRepository, AdminRepository>();
            builder.Services.AddTransient<IBookingRepository, BookingRepository>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IVehicleRepository, VehicleRepository>();

            // Auth service
            builder.Services.AddScoped<IAuthService, AuthService>();     // TODO: Utvärdera.

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}
