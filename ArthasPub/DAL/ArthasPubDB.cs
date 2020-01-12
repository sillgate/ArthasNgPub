using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArthasPub.Models;

namespace ArthasPub.DAL
{
    public class ArthasPubDB : DbContext
    {
        public ArthasPubDB() : base("ArthasPubDB")
        { }
        public DbSet<Item> Items { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}