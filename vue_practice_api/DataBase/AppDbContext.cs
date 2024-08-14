using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace vue_practice_api.DataBase
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        // 定義 DbSet 屬性
        public DbSet<Item> Items { get; set; }

        public DbSet<FileUploads> FileUploads { get; set; }
        public DbSet<ProductModel> ProductModels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}

