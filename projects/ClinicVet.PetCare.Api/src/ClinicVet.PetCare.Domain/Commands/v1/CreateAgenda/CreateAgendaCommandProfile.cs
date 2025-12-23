using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.Domain.Fixeds.v1;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandProfile : Profile
{
    public CreateAgendaCommandProfile()
    {
        CreateMap<CreateAgendaCommand, AgendaParameterDto>()
            .ForMember(dest => dest.Status, src => src.MapFrom(opt => opt.Status ?? AgendaStatusType.Scheduled))
            .ForMember(dest => dest.PetName, src => src.MapFrom(opt => opt.Pet!.Name))
            .ForMember(dest => dest.PetSpecie, src => src.MapFrom(opt => opt.Pet!.Specie))
            .ForMember(dest => dest.OwnerType, src => src.MapFrom(opt => opt.PetOwner.OwnerType))
            .ForMember(dest => dest.OwnerPhone, src => src.MapFrom(opt => opt.PetOwner!.Contact!.Phone))
            .ForMember(dest => dest.OwnerDocument, src => src.MapFrom(opt => opt.PetOwner!.Document!.Code))
            .ForMember(dest => dest.DocumentType, src => src.MapFrom(opt => opt.PetOwner.Document!.Type ?? DocumentType.CPF));
    }
}