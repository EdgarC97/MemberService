using AutoMapper;
using MemberService.Models.Entities;
using MemberService.Models.DTOs;

namespace MemberService.Mappers
{
    // The MappingProfile class inherits from AutoMapper's Profile class,
    // and is used to define the mapping configurations between different object types.
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map from Member entity to MemberDto.
            // This creates a mapping configuration to convert a Member object into a MemberDto object.
            CreateMap<Member, MemberDto>();

            // Map from CreateMemberDto to Member entity.
            // This mapping is used when creating a new Member from the CreateMemberDto.
            CreateMap<CreateMemberDto, Member>()
                // Map the InitialBalance from CreateMemberDto to the Balance property of Member.
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.InitialBalance))
                // Set the RegistrationDate of the Member to the current DateTime.
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => DateTime.Now))
                // Set the IsActive property of the Member to true.
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true));

            // Map from UpdateMemberDto to Member entity.
            // This mapping is used to update an existing Member using data from UpdateMemberDto.
            CreateMap<UpdateMemberDto, Member>();
        }
    }
}
