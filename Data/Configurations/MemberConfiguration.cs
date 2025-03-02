// Data/Configurations/MemberConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MemberService.Models.Entities;

namespace MemberService.Data.Configurations
{
    // Implements the IEntityTypeConfiguration interface to configure the Member entity.
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        // The Configure method defines how the Member entity should be mapped to the database.
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            // Define the primary key for the Member entity.
            builder.HasKey(m => m.Id);

            // Configure the FirstName property:
            // - Make it required.
            // - Set a maximum length of 50 characters.
            builder.Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            // Configure the LastName property:
            // - Make it required.
            // - Set a maximum length of 50 characters.
            builder.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(50);

            // Configure the Email property:
            // - Make it required.
            // - Set a maximum length of 100 characters.
            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);

            // Create an index on the Email property and enforce uniqueness.
            builder.HasIndex(m => m.Email)
                .IsUnique();

            // Configure the BirthDate property:
            // - Make it required.
            builder.Property(m => m.BirthDate)
                .IsRequired();

            // Configure the RegistrationDate property:
            // - Make it required.
            // - Set a default value using SQL GETDATE() function.
            builder.Property(m => m.RegistrationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Configure the IsActive property:
            // - Make it required.
            // - Set a default value of true.
            builder.Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            // Configure the Balance property:
            // - Make it required.
            // - Set the precision to 18 digits in total, with 2 decimal places.
            // - Set a default value of 0.
            builder.Property(m => m.Balance)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasDefaultValue(0);
        }
    }
}
