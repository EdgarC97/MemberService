
using MemberService.Models.Entities;

namespace MemberService.Data.Seeders
{
    public static class MemberSeeder
    {
        public static List<Member> GetSeedData()
        {
            return new List<Member>
            {
                new Member
                {
                    Id = 1,
                    FirstName = "Juan",
                    LastName = "Pérez",
                    Email = "juan.perez@example.com",
                    BirthDate = new DateTime(1985, 5, 15),
                    RegistrationDate = new DateTime(2025, 01, 01),
                    IsActive = true,
                    Balance = 1000.00m
                },
                new Member
                {
                    Id = 2,
                    FirstName = "María",
                    LastName = "García",
                    Email = "maria.garcia@example.com",
                    BirthDate = new DateTime(1990, 8, 22),
                    RegistrationDate = new DateTime(2025, 01, 01),
                    IsActive = true,
                    Balance = 2500.50m
                },
                new Member
                {
                    Id = 3,
                    FirstName = "Carlos",
                    LastName = "Rodríguez",
                    Email = "carlos.rodriguez@example.com",
                    BirthDate = new DateTime(1978, 3, 10),
                    RegistrationDate = new DateTime(2025, 01, 01),
                    IsActive = true,
                    Balance = 5000.75m
                }
            };
        }
    }
}