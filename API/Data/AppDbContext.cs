using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

// 内联构造函数
public class AppDbContext(DbContextOptions options):DbContext(options)
{
    // AppUser 实体类  Users 表名
    public DbSet<AppUser> Users { get; set; }
}