
using MemberService.Models.DTOs;

namespace MemberService.Services.Interfaces
{
    public interface IMemberService
    {
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<MemberDto> GetMemberByIdAsync(int id);
        Task<MemberDto> GetMemberByEmailAsync(string email);
        Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto);
        Task<MemberDto> UpdateMemberAsync(int id, UpdateMemberDto updateMemberDto);
        Task<bool> DeleteMemberAsync(int id);
    }
}