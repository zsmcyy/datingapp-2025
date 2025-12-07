using System.Text;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// 将创建的 AppDbContext 类添加到 DI 容器中 
// GetConnectionString 从指定的配置中获取指定的连接字符串
builder.Services.AddDbContext<AppDbContext>(opt=>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors();
// 当应用程序启动时，创建该服务的一个实例，并在整个应用程序生命周期内使用一直保留，令牌服务不需要这种功能
// builder.Services.AddSingleton
// 在每次请求时都创建一个新的 TokenService 实例，生命周期太短了，无法满足需求
// builder.Services.AddTransient
// 服务的作用于限定在单个 HTTP 请求内，在同一个请求内的所有依赖该服务的组件都使用同一个实例
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var tokenKey = builder.Configuration["TokenKey"] ?? throw new Exception("无法获取令牌密钥-Program.cs");
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey =  true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x =>
{
    // 允许任何请求头，允许任何请求方法，指定允许的来源
    x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200");
});

// 身份验证的两个中间件
app.UseAuthentication();    // 是谁
app.UseAuthorization();     // 是否有权限

app.MapControllers();

app.Run();
