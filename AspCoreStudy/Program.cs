using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000);  // 或者你可以选择指定的 IP 地址
});

// 从 appsettings.json 中获取连接字符串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 将 MySQL 的 DbContext 添加到依赖注入容器中
builder.Services.AddDbContext<my_databaseContext>(options =>
    options.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.41-mysql"))
);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Account}/{action=Login}");

app.Run();
