using API.Data;
using Microsoft.EntityFrameworkCore;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(x =>
{
    // 允许任何请求头，允许任何请求方法，指定允许的来源
    x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200");
});

app.MapControllers();

app.Run();
