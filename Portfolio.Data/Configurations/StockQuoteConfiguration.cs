using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portfolio.Data.Configurations
{
    class StockQuoteConfiguration : IEntityTypeConfiguration<StockQuote>
    {
        public void Configure(EntityTypeBuilder<StockQuote> builder)
        {
            builder.HasKey(p => new { p.Code, p.Date });

            builder.HasIndex(p => p.Date)
                .HasName("IDX_Date");
        }
    }
}
