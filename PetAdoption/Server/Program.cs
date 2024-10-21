using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PetAdoption.Server.Repository;
using LazurdIT.FluentOrm.MsSql;

namespace PetAdoption
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // Set up connection string
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

            // Register your custom repositories as scoped services.
            builder.Services.AddScoped<PetRepository>(provider => new PetRepository(connectionString));
            builder.Services.AddScoped<OwnerRepository>(provider => new OwnerRepository(connectionString));
            builder.Services.AddScoped<AdoptionRepository>(provider => new AdoptionRepository(connectionString));

            // Enable CORS to allow frontend communication.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
            });

            // Register HttpClient with a base address if necessary (replace with your API URL)
            builder.Services.AddScoped(sp =>
            {
                var httpClient = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost:5011/") // Ensure this matches your API URL
                };
                return httpClient;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts(); // Use HSTS for production.
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            // Apply the CORS policy.
            app.UseCors("AllowAll");

            // Map endpoints.
            app.MapRazorPages();
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}
