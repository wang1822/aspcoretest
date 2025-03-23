using AspCoreStudy.Models;
using Microsoft.EntityFrameworkCore;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AspCoreStudy.Services;
using AspCoreStudy;
using Serilog;
using AspCoreStudy.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();  // 必须添加的服务
builder.Services.AddSwaggerGen();  // 注册 Swagger 生成器

//使用 Autofac 作为依赖注入容器
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// 从配置中获取 SecretKey
var secretKey = builder.Configuration["TokenSettings:SecretKey"];

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    // 扫描并注册 Repositories 和 Services 文件夹中的接口和实现类
    _ = containerBuilder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                    .Where(t => t.Namespace != null && (t.Namespace.Contains("AspCoreStudy.Repositories")
                    || t.Namespace.Contains("AspCoreStudy.Services")))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();  // 设置生命周期（你也可以使用 InstancePerDependency() 或 SingleInstance()）

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
    options.UseMySql(connectionString, ServerVersion.Parse("8.0.35-mysql"))
);

builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<ValidateModelStateFilter>(); // 注册统一处理 ModelState 验证的过滤器
    options.Filters.Add<GlobalExceptionFilter>(); // 注册全局异常过滤器
});

// 配置 Serilog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // 输出到控制台
    .WriteTo.File("Logs/app.log", rollingInterval: RollingInterval.Day) // 输出到文件
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddHttpClient();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-issuer",
            ValidAudience = "your-audience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// 添加授权服务
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ViewPage", policy =>
        policy.Requirements.Add(new PermissionRequirement("ViewPage")));
    options.AddPolicy("Management", policy =>
        policy.Requirements.Add(new PermissionRequirement("Management")));
    options.AddPolicy("EditPage", policy =>
        policy.Requirements.Add(new PermissionRequirement("EditPage")));
    options.AddPolicy("DelePage", policy =>
        policy.Requirements.Add(new PermissionRequirement("DelePage")));
});

// 注册自定义授权处理程序
builder.Services.AddSingleton<IAuthorizationHandler, PermissionHandler>();

var app = builder.Build();

// // 获取 Autofac 容器
// var container = app.Services.GetAutofacRoot();  // 这是正确的方式

// // 打印已注册的服务
// foreach (var registration in container.ComponentRegistry.Registrations)
// {
//     Console.WriteLine($"Service: {registration.Activator.LimitType}");
// }

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // 启用 Swagger JSON 生成
    app.UseSwaggerUI(options =>
    {
        // 配置 Swagger UI，选择 Swagger JSON 文档
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = "swagger";  // Swagger UI 的路由前缀
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}");

app.Run();
