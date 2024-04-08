using log4net.Config;
using log4net;
using Microsoft.AspNetCore.Authentication.Cookies;
using MySqlConnector;
using VehicleVilla.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IVehicleDataService, VehicleDAO>();
builder.Services.AddMySqlDataSource(builder.Configuration.GetConnectionString("Default")!);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
});

// Configure Log4net
var log4netConfig = new FileInfo(Path.Combine(builder.Environment.ContentRootPath, "Config/log4net.xml"));
XmlConfigurator.ConfigureAndWatch(log4netConfig);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseSession();

app.UseRouting();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
