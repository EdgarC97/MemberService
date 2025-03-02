using Microsoft.EntityFrameworkCore;
using MemberService.Data;
using MemberService.Models.Entities;
using MemberService.Repositories.Interfaces;

namespace MemberService.Repositories
{
    // Repository class for managing Member entities.
    // Implements the IMemberRepository interface for data access operations.
    public class MemberRepository : IMemberRepository
    {
        // Private field to store the database context.
        private readonly MemberDbContext _context;

        // Constructor that accepts a MemberDbContext via dependency injection.
        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }

        // Retrieves all members from the database.
        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            // Returns the list of all members as an asynchronous operation.
            return await _context.Members.ToListAsync();
        }

        // Retrieves a specific member by its unique identifier.
        public async Task<Member> GetByIdAsync(int id)
        {
            // Finds and returns the member with the given id.
            return await _context.Members.FindAsync(id);
        }

        // Retrieves a member based on their email address.
        public async Task<Member> GetByEmailAsync(string email)
        {
            // Searches for a member with a matching email (case-insensitive).
            return await _context.Members
                .FirstOrDefaultAsync(m => m.Email.ToLower() == email.ToLower());
        }

        // Checks whether a member exists by its unique identifier.
        public async Task<bool> ExistsAsync(int id)
        {
            // Returns true if any member with the given id exists, otherwise false.
            return await _context.Members.AnyAsync(m => m.Id == id);
        }

        // Checks if a member with the given email already exists.
        public async Task<bool> EmailExistsAsync(string email)
        {
            // Returns true if any member with a matching email exists (case-insensitive).
            return await _context.Members.AnyAsync(m => m.Email.ToLower() == email.ToLower());
        }

        // Creates a new member in the database.
        public async Task<Member> CreateAsync(Member member)
        {
            // Adds the new member entity to the context.
            await _context.Members.AddAsync(member);
            // Persists changes to the database.
            await SaveChangesAsync();
            // Returns the newly created member.
            return member;
        }

        // Updates an existing member's details.
        public async Task UpdateAsync(Member member)
        {
            // Marks the member entity as modified in the context.
            _context.Members.Update(member);
            // Persists the update changes to the database.
            await SaveChangesAsync();
        }

        // Deletes a member from the database by its unique identifier.
        public async Task DeleteAsync(int id)
        {
            // Retrieve the member to be deleted.
            var member = await GetByIdAsync(id);
            // If the member exists, remove it from the context.
            if (member != null)
            {
                _context.Members.Remove(member);
                // Persist the deletion to the database.
                await SaveChangesAsync();
            }
        }

        // Helper method to persist changes to the database.
        public async Task SaveChangesAsync()
        {
            // Saves all changes made in the context asynchronously.
            await _context.SaveChangesAsync();
        }
    }
}
