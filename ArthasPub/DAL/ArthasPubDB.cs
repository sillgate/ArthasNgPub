using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using ArthasPub.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ArthasPub.DAL
{

    //inherit the identityDBcontext structure
    public class ArthasPubDB : IdentityDbContext<ApplicationUser>
    {
        public ArthasPubDB() : base("ArthasPubDB")
        { }
        
        public DbSet<Item> Items { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }

        //create db
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        //create instance
        public static ArthasPubDB Create()
        {
            return new ArthasPubDB();
        }

    }
}