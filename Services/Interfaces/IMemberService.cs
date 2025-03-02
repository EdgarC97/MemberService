using MemberService.Models.DTOs;

namespace MemberService.Services.Interfaces
{
    // Interface defining the contract for member-related operations.
    public interface IMemberService
    {
        // Retrieves all members as a collection of MemberDto objects.
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();

        // Retrieves a specific member by its unique identifier.
        Task<MemberDto> GetMemberByIdAsync(int id);

        // Retrieves a member based on their email address.
        Task<MemberDto> GetMemberByEmailAsync(string email);

        // Creates a new member using the provided CreateMemberDto and returns the created member.
        Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto);

        // Updates an existing member with new data from UpdateMemberDto.
        // Returns the updated member or null if the member does not exist.
        Task<MemberDto> UpdateMemberAsync(int id, UpdateMemberDto updateMemberDto);

        // Deletes a member by its unique identifier.
        // Returns true if the deletion was successful, otherwise false.
        Task<bool> DeleteMemberAsync(int id);
    }
}
