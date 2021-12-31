using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantApi.Entities
{
    public class DBContext : DbContext
    {
        private string connectionString = "Server=DESKTOP-K9GE6JJ\\SQLEXPRESS;Database=RestaurantApi;Trusted_Connection=True;";
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(x => x.Name).IsRequired().HasMaxLength(25);
            modelBuilder.Entity<Dish>()
                .Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Address>()
                .Property(x => x.City).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Address>()
                .Property(x => x.Street).IsRequired().HasMaxLength(50);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
