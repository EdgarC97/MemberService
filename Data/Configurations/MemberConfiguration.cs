// Data/Configurations/MemberConfiguration.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MemberService.Models.Entities;

namespace MemberService.Data.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(m => m.Email)
                .IsUnique();

            builder.Property(m => m.BirthDate)
                .IsRequired();

            builder.Property(m => m.RegistrationDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(m => m.Balance)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasDefaultValue(0);
        }
    }
}