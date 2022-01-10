using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pui_Madalina_Proiect.Models;

namespace Pui_Madalina_Proiect.Data
{
    public class CollectionContext : DbContext
    {
        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Corporations> Corporations { get; set; }
        public DbSet<PublishedGame> PublishedGames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Corporations>().ToTable("Corporation");
            modelBuilder.Entity<PublishedGame>().ToTable("PublishedGame");

            modelBuilder.Entity<PublishedGame>().HasKey(c => new { c.GameID, c.CorporationID });//configureaza cheia primara compusa
        }
    }
}
