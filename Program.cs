using bettersociety.Areas.User.Interfaces;
using bettersociety.Areas.User.Repository;
using bettersociety.Data;
using bettersociety.Interfaces;
using bettersociety.Models;
using bettersociety.Service;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
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

//Add Identity
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
}).AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
    options.DefaultChallengeScheme =
    options.DefaultForbidScheme =
    options.DefaultScheme =
    options.DefaultSignInScheme =
    options.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]))
    };
}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Login";
    options.LogoutPath = "/Logout";
    options.AccessDeniedPath = "/Home/Error"; // Path to the access denied page
    options.Cookie.HttpOnly = true; // Secure cookie against XSS
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // HTTPS only
    options.Cookie.SameSite = SameSiteMode.Strict; // Prevent CSRF
});

//Dependency injection
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<IQuestionXrefTagsRepository, QuestionXrefTagsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/Index");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

// Add custom middleware to extract token from cookie
app.Use(async (context, next) =>
{
    // Extract token from cookie
    var token = context.Request.Cookies["XSRF-TOKEN"];

    if (!string.IsNullOrEmpty(token))
    {
        // Add token to Authorization header for middleware to validate
        context.Request.Headers["Authorization"] = $"Bearer {token}";
    }

    await next();
});

// Map error pages
app.UseStatusCodePagesWithReExecute("/Error/Index", "?statusCode={0}");

app.UseAuthentication();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//      name: "areas",
//      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
//    );
//});

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Error handling route
app.MapControllerRoute(
    name: "error",
    pattern: "Error/{statusCode}",
    defaults: new { controller = "Error", action = "Index" });

app.Run();
