
using MemberService.Models.Entities;

namespace MemberService.Repositories.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member> GetByIdAsync(int id);
        Task<Member> GetByEmailAsync(string email);
        Task<bool> ExistsAsync(int id);
        Task<bool> EmailExistsAsync(string email);
        Task<Member> CreateAsync(Member member);
        Task UpdateAsync(Member member);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}