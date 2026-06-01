using Inventra.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Persistence.Configurations
{
    public class WarehouseConfiguration
    : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(
            EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(250);
        }
    }
}
