
using AutoMapper;
using MemberService.Models.DTOs;
using MemberService.Models.Entities;
using MemberService.Repositories.Interfaces;
using MemberService.Services.Interfaces;

namespace MemberService.Services
{
    public class MemberManagementService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberManagementService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            var members = await _memberRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto> GetMemberByIdAsync(int id)
        {
            var member = await _memberRepository.GetByIdAsync(id);
            if (member == null)
                return null;

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> GetMemberByEmailAsync(string email)
        {
            var member = await _memberRepository.GetByEmailAsync(email);
            if (member == null)
                return null;

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto)
        {
            // Verificar si el correo ya existe
            if (await _memberRepository.EmailExistsAsync(createMemberDto.Email))
                throw new InvalidOperationException("Email already exists");

            var member = _mapper.Map<Member>(createMemberDto);

            await _memberRepository.CreateAsync(member);

            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> UpdateMemberAsync(int id, UpdateMemberDto updateMemberDto)
        {
            var existingMember = await _memberRepository.GetByIdAsync(id);

            if (existingMember == null)
                return null;

            // Verificar si el email ya existe y no es del mismo miembro
            if (!string.IsNullOrEmpty(updateMemberDto.Email) &&
                updateMemberDto.Email != existingMember.Email &&
                await _memberRepository.EmailExistsAsync(updateMemberDto.Email))
            {
                throw new InvalidOperationException("Email already exists");
            }

            _mapper.Map(updateMemberDto, existingMember);

            await _memberRepository.UpdateAsync(existingMember);

            return _mapper.Map<MemberDto>(existingMember);
        }

        public async Task<bool> DeleteMemberAsync(int id)
        {
            if (!await _memberRepository.ExistsAsync(id))
                return false;

            await _memberRepository.DeleteAsync(id);
            return true;
        }
    }
}