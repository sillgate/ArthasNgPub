using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArthasPub.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArthasPub.DAL
{
    public class ArthasPubDB : IdentityDbContext<ApplicationUser>
    {
        public ArthasPubDB() : base("ArthasPubDB")
        { }
        public DbSet<Item> Items { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            //modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            //modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });
            //modelBuilder.Entity<ApplicationUser>().HasKey<string>(l => l.Id);
            base.OnModelCreating(modelBuilder);
        }

        public static ArthasPubDB Create()
        {
            return new ArthasPubDB();
        }

    }
}