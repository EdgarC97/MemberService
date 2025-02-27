// Data/MemberDbContext.cs
using Microsoft.EntityFrameworkCore;
using MemberService.Models.Entities;
using MemberService.Data.Configurations;
using MemberService.Data.Seeders;

namespace MemberService.Data
{
    public class MemberDbContext : DbContext
    {
        public MemberDbContext(DbContextOptions<MemberDbContext> options)
            : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MemberConfiguration());

            // Aplicar datos semilla
            modelBuilder.Entity<Member>().HasData(MemberSeeder.GetSeedData());
        }
    }
}