
using Microsoft.EntityFrameworkCore;
using MemberService.Data;
using MemberService.Models.Entities;
using MemberService.Repositories.Interfaces;

namespace MemberService.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private readonly MemberDbContext _context;

        public MemberRepository(MemberDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Member>> GetAllAsync()
        {
            return await _context.Members.ToListAsync();
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            return await _context.Members.FindAsync(id);
        }

        public async Task<Member> GetByEmailAsync(string email)
        {
            return await _context.Members
                .FirstOrDefaultAsync(m => m.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Members.AnyAsync(m => m.Id == id);
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await _context.Members.AnyAsync(m => m.Email.ToLower() == email.ToLower());
        }

        public async Task<Member> CreateAsync(Member member)
        {
            await _context.Members.AddAsync(member);
            await SaveChangesAsync();
            return member;
        }

        public async Task UpdateAsync(Member member)
        {
            _context.Members.Update(member);
            await SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var member = await GetByIdAsync(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}