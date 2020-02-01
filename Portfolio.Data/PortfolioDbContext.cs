using Microsoft.EntityFrameworkCore;
using System;
using Portfolio.Model;
using Portfolio.Data.Configurations;

namespace Portfolio.Data
{
    public class PortfolioDbContext : DbContext
    {
        public DbSet<Model.Portfolio> Portfolios { get; set; }
        public DbSet<StockQuote> StockQuotes { get; set; }

        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Model.Portfolio>().HasKey(o => o.Id);
            //modelBuilder.Entity<Model.Portfolio>().Ignore(o => o.Summaries);

            modelBuilder.ApplyConfiguration(new PortfolioConfiguration());
            modelBuilder.ApplyConfiguration(new StockQuoteConfiguration());
        }
    }
}
