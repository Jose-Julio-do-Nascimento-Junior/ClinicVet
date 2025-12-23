using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;

namespace ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;

public sealed class UpdateAgendaCommandProfile : Profile
{
	public UpdateAgendaCommandProfile()
	{
        CreateMap<UpdateAgendaCommand, AgendaParameterDto>()
            .ForMember(dest => dest.PetName, src => src.MapFrom(opt => opt.Pet!.Name))
            .ForMember(dest => dest.PetSpecie, src => src.MapFrom(opt => opt.Pet!.Specie))
            .ForMember(dest => dest.OwnerDocument, src => src.MapFrom(opt => opt.PetOwner!.Document!.Code))
            .ForMember(dest => dest.OwnerPhone, src => src.MapFrom(opt => opt.PetOwner!.Contact!.Phone))
            .ForMember(dest => dest.OwnerType, src => src.MapFrom(opt => opt.PetOwner!.OwnerType))
            .ForMember(dest => dest.DocumentType, src => src.MapFrom(opt => opt.PetOwner!.Document!.Type));
    }
}