using bettersociety.Data;
using Microsoft.EntityFrameworkCore;
//using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
/*
 * builder.Services.AddControllersWithViews();
    - Registers both controller and view-related services.
    - Enables rendering of Razor Views, used in MVC applications where you want to return HTML pages.
    - Use when you're building a traditional MVC application or a hybrid application (mixing Web API and Razor Views).
 */
builder.Services.AddControllersWithViews();

//To use this app for API
/*
 * builder.Services.AddControllers();
    - Registers only the controller services necessary to build a Web API.
    - Does not include support for Razor Views or View rendering (used in traditional MVC applications).
    - Use when you're building a Web API that only returns data (e.g., JSON or XML) and doesn't render HTML views.
 */
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
