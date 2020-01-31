using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Portfolio.Data.Configurations
{
    class PortfolioConfiguration : IEntityTypeConfiguration<Model.Portfolio>
    {
        public void Configure(EntityTypeBuilder<Model.Portfolio> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .HasMany(o => o.Transactions)
                .WithOne()
                .HasForeignKey(x => x.PortfolioId);

            builder.Ignore(o => o.Summaries);
        }
    }
}
