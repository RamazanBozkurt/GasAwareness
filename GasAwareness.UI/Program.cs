using FluentValidation;
using FluentValidation.AspNetCore;
using GasAwareness.API.Enums;
using GasAwareness.API.Helpers;
using GasAwareness.UI;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();

builder.WebHost.ConfigureKestrel(options => options.Limits.MaxRequestBodySize = 50 * 1024 * 1024);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "GasAwareness.Auth"; 
        options.LoginPath = "/Login";              
        //options.AccessDeniedPath = "/Forbidden";   
        options.Cookie.HttpOnly = true;            
        options.ExpireTimeSpan = TimeSpan.FromHours(2);
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy =>policy.RequireRole(RoleName.Admin));
    options.AddPolicy("RequireVisitor", policy => policy.RequireRole(RoleName.Visitor));
    options.AddPolicy("RequireEditor", policy => policy.RequireRole(RoleName.Editor));
    options.AddPolicy("RequireAdminAndEditor", policy => policy.RequireRole(RoleGroup.AdminAndEditor));
    options.AddPolicy("RequireAllRoles", policy => policy.RequireRole(RoleGroup.AllRoles));
});

builder.Services.AddValidatorsFromAssemblyContaining<VideoCreateValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true; 
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
