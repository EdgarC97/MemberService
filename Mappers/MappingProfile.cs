
using AutoMapper;
using MemberService.Models.Entities;
using MemberService.Models.DTOs;

namespace MemberService.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Entity to DTO
            CreateMap<Member, MemberDto>();

            // DTO to Entity
            CreateMap<CreateMemberDto, Member>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialBalance))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            CreateMap<UpdateMemberDto, Member>();
        }
    }
}