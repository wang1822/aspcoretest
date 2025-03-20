using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AspCoreStudy.Services;
using AspCoreStudy;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//使用 Autofac 作为依赖注入容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // 扫描并注册 Repositories 和 Services 文件夹中的接口和实现类
    _ = containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                    .Where(t => t.Namespace != null && (t.Namespace.Contains("AspCoreStudy.Repositories")
                    || t.Namespace.Contains("AspCoreStudy.Services")))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();  // 设置生命周期（你也可以使用 InstancePerDependency() 或 SingleInstance()）

    // 从配置中获取 SecretKey
    var secretKey = builder.Configuration["TokenSettings:SecretKey"];

    // 注册 TokenService
    _ = containerBuilder.RegisterType<TokenService>()
                    .WithParameter("secretKey", secretKey)
                    .SingleInstance();  // 设置为单例
});

// 配置 Kestrel 服务器
// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(5000);  // 或者你可以选择指定的 IP 地址
// });

// 从 appsettings.json 中获取连接字符串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 将 MySQL 的 DbContext 添加到依赖注入容器中
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.41-mysql"))
);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<GlobalExceptionFilter>(); // 注册全局异常过滤器
});

// 配置 Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // 输出到控制台
    .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day) // 输出到文件
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// // 获取 Autofac 容器
// var container = app.Services.GetAutofacRoot();  // 这是正确的方式

// // 打印已注册的服务
// foreach (var registration in container.ComponentRegistry.Registrations)
// {
//     Console.WriteLine($"Service: {registration.Activator.LimitType}");
// }

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
