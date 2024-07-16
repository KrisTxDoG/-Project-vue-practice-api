using Microsoft.EntityFrameworkCore;

namespace vue_practice_api.DataBase
{
    public class AppDbContext : DbContext
    {
        // 定義 DbSet 屬性
        public DbSet<Item> Items { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
