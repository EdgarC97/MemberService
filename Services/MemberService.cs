using AutoMapper;
using MemberService.Models.DTOs;
using MemberService.Models.Entities;
using MemberService.Repositories.Interfaces;
using MemberService.Services.Interfaces;

namespace MemberService.Services
{
    // This service implements member management functionality and the IMemberService interface.
    public class MemberManagementService : IMemberService
    {
        // Repository for accessing member data.
        private readonly IMemberRepository _memberRepository;
        // AutoMapper instance for mapping between entities and DTOs.
        private readonly IMapper _mapper;

        // Constructor with dependency injection for IMemberRepository and IMapper.
        public MemberManagementService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        // Retrieves all members from the repository and maps them to MemberDto objects.
        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        // Retrieves a member by its unique identifier.
        // If found, it maps the Member entity to a MemberDto; otherwise, returns null.
        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                return null;

            return _mapper.Map<MemberDto>(member);
        }

        // Retrieves a member by their email address.
        // If found, it maps the Member entity to a MemberDto; otherwise, returns null.
        public async Task<MemberDto> GetMemberByEmailAsync(string email)
        {
            var member = await _memberRepository.GetByEmailAsync(email);
            if (member == null)
                return null;

            return _mapper.Map<MemberDto>(member);
        }

        // Creates a new member based on the CreateMemberDto.
        // Throws an exception if the email is already in use.
        public async Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto)
        {
            // Verify that the email does not already exist.
            if (await _memberRepository.EmailExistsAsync(createMemberDto.Email))
                throw new InvalidOperationException("Email already exists");

            // Map the CreateMemberDto to a Member entity.
            var member = _mapper.Map<Member>(createMemberDto);

            // Create the new member in the repository.
            await _memberRepository.CreateAsync(member);

            // Map the newly created Member entity back to a MemberDto and return it.
            return _mapper.Map<MemberDto>(member);
        }

        // Updates an existing member based on the provided UpdateMemberDto.
        // Returns the updated MemberDto, or null if the member does not exist.
        public async Task<MemberDto> UpdateMemberAsync(int id, UpdateMemberDto updateMemberDto)
        {
            // Retrieve the existing member from the repository.
            var existingMember = await _memberRepository.GetByIdAsync(id);

            // If no member is found, return null.
            if (existingMember == null)
                return null;

            // Check if the email is being updated and ensure it does not already exist for another member.
            if (!string.IsNullOrEmpty(updateMemberDto.Email) &&
                updateMemberDto.Email != existingMember.Email &&
                await _memberRepository.EmailExistsAsync(updateMemberDto.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            // Map the changes from the UpdateMemberDto onto the existing member entity.
            _mapper.Map(updateMemberDto, existingMember);

            // Update the member in the repository.
            await _memberRepository.UpdateAsync(existingMember);

            // Map the updated Member entity back to a MemberDto and return it.
            return _mapper.Map<MemberDto>(existingMember);
        }

        // Deletes a member by its unique identifier.
        // Returns true if deletion was successful, otherwise false.
        public async Task<bool> DeleteMemberAsync(int id)
        {
            // Check if the member exists.
            if (!await _memberRepository.ExistsAsync(id))
                return false;

            // Delete the member from the repository.
            await _memberRepository.DeleteAsync(id);
            return true;
        }
    }
}
