using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SamerAdvancedStore.Models
{
    public class AppContext :DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Invintorey> Invintories { get; set; }
      //  public DbSet<InventoryProducts> InventoryProductses { get; set; }
        public DbSet<Transactions> OrderDetailses { get; set; }
        public AppContext() : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasRequired(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);


            //modelBuilder.Entity<Product>()
            //    .HasMany(x => x.Invintory)
            //    .WithRequired(x => x.Products)
            //    .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<Invintorey>()
                .HasMany(x => x.Products)
                .WithOptional(x => x.Invintory)
                .HasForeignKey(x => x.InvintoryId)
                .WillCascadeOnDelete(true);

        
            ;
            modelBuilder.Entity<Order>()
                .HasMany(x => x.OrderDetailses)
                .WithOptional(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Product>()
                .HasMany(x => x.OrderDetailses)
                .WithRequired(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Category>()
                .HasMany(x => x.Products)
                .WithRequired(x => x.Category)
                .HasForeignKey(x => x.CategoryId)
                .WillCascadeOnDelete(true);



        
            base.OnModelCreating(modelBuilder);
        }

        public static AppContext Create()
        {
            return new AppContext();
        }
    }
}