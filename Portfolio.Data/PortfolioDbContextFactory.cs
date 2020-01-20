using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Data
{
    /*
    public class PortfolioDbContextFactory : IDesignTimeDbContextFactory<PortfolioDbContext>
    {
        public PortfolioDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PortfolioDbContext>();
            optionsBuilder.UseSqlServer("Server=.\\SQL2017;Database=Portfolio;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new PortfolioDbContext(optionsBuilder.Options);
        }
    }
    */
}
