using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Configurations
{
    public class StockMovementConfiguration
    : IEntityTypeConfiguration<StockMovement>
    {
        public void Configure(
            EntityTypeBuilder<StockMovement> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Quantity)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.HasOne(x => x.Stock)
                .WithMany(x => x.StockMovements)
                .HasForeignKey(x => x.StockId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
