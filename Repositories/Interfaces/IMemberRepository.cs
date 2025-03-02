using MemberService.Models.Entities;

namespace MemberService.Repositories.Interfaces
{
    // Interface for the Member repository, defining CRUD and utility operations for Member entities.
    public interface IMemberRepository
    {
        // Retrieves all members from the data source.
        Task<IEnumerable<Member>> GetAllAsync();

        // Retrieves a specific member by its unique identifier.
        Task<Member> GetByIdAsync(int id);

        // Retrieves a member based on their email address.
        Task<Member> GetByEmailAsync(string email);

        // Checks if a member exists by its unique identifier.
        Task<bool> ExistsAsync(int id);

        // Checks if a member with the specified email exists.
        Task<bool> EmailExistsAsync(string email);

        // Creates a new member in the data source.
        Task<Member> CreateAsync(Member member);

        // Updates an existing member's details.
        Task UpdateAsync(Member member);

        // Deletes a member from the data source using its unique identifier.
        Task DeleteAsync(int id);

        // Persists changes made in the context to the data source.
        Task SaveChangesAsync();
    }
}
