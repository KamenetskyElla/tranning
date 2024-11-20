using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersDemo.Domain;

namespace OrdersDemo.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order));

        // Even though we're not configuring anything specific for Date property,
        // it's crucial to write the following line, and let the EF know that we want to include this property.
        // EF by default, excludes all properties with getters only. It assumes they are calculated properties.
        builder.Property(x => x.DateCreated);

        builder.Property(x => x.OrderNo).IsRequired().HasMaxLength(6);

        builder.OwnsOne(x => x.Customer, o =>
        {
            o.Property(x => x.Type);
            o.Property(x => x.FirstName).IsRequired().HasMaxLength(100);
            o.Property(x => x.LastName).IsRequired().HasMaxLength(100);
            o.Property(x => x.Email).HasMaxLength(100);
        });

        builder.OwnsOne(x => x.Address, o =>
        {
            o.Property(x => x.Street).IsRequired().HasMaxLength(250);
            o.Property(x => x.City).IsRequired().HasMaxLength(100);
            o.Property(x => x.PostalCode).IsRequired().HasMaxLength(10);
            o.Property(x => x.Country).IsRequired().HasMaxLength(100);
        });

        builder.HasKey(x => x.Id);
    }
}
