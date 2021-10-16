using Domains;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<House> Houses { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("AzureWebJobsStorage"));
        //}

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).ValueGeneratedOnAdd();
            });

            builder.Entity<House>(entity =>
            {
                entity.Property(e => e.HouseId).ValueGeneratedOnAdd();
            });

            builder.Entity<Image>(entity =>
            {
                entity.Property(e => e.ImageKey).ValueGeneratedOnAdd();
            });

            builder.Entity<MortgageOffer>(entity =>
            {
                entity.Property(e => e.MortgageOfferId).ValueGeneratedOnAdd();
            });

            builder.Entity<FinancialInformation>(entity =>
            {
                entity.Property(e => e.FinancialInformationId).ValueGeneratedOnAdd();
            });
        }
    }
}
