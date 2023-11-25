using LandManager.Service;
using LandManager.Data;
using Microsoft.EntityFrameworkCore;
using LandManager.Service.Implementations;
using LandManager.Models;
using LandManager.Core.Services;
using LandManager.Core;

namespace LandManager;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<LandManagerContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("LandManagerDatabase")));

        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddScoped<ILandUseService, LandUseService>();
        builder.Services.AddScoped<ILandPropertyService, LandPropertyService>();
        builder.Services.AddScoped<ILandParcelService, LandParcelService>();
        //builder.Services.AddScoped<>

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}