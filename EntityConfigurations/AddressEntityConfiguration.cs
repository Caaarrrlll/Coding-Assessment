using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechSolutionsCRM.Models;

namespace TechSolutions.Server.EntityConfigurations;

public class AddressEntityConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable(nameof(Address));

        builder.HasKey(x => x.Id);

        builder.Property(a => a.AddressName)
            .HasMaxLength(50);

        builder.Property(a => a.AddressType);

        builder.Property(a => a.AddressLine1)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.AddressLine2)
            .HasMaxLength(100);

        builder.Property(a => a.City)
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(a => a.PostalCode)
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(a => a.Province)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasOne(a => a.Customer)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerId)
            .IsRequired();
    }
}
