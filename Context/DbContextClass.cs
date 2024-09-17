using GraphQlDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQlDemo.Context
{
    public class DbContextClass:DbContext
    {
        public DbContextClass(DbContextOptions<DbContextClass> options):base(options)
        {
            
        }
        public DbSet<ProductDetails> Products { get; set; }
        public virtual DbSet<ItemData> Items { get; set; }
        public virtual DbSet<ItemList> Lists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ItemList>().HasMany(x => x.ItemDatas)
                                           .WithOne(x => x.ItemList).HasForeignKey(x => x.ListId)
                                           .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
