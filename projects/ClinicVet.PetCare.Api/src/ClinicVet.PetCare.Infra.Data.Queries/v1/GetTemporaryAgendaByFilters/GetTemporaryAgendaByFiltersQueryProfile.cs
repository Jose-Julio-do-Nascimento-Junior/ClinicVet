using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.Domain.Helpers.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters.Responses;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;

public sealed class GetTemporaryAgendaByFiltersQueryProfile : Profile
{
    public GetTemporaryAgendaByFiltersQueryProfile()
    {
        CreateMap<GetTemporaryAgendaByFiltersQuery, GetAgendaByFiltersDto>()
            .ForMember(dest => dest.Status, src => src.Ignore());

        CreateMap<TemporaryAgendaDto, AgendaDto>()
            .ForMember(dest => dest.AppointmentAt, src => src.MapFrom(opt => opt.AppointmentAt))
            .ForMember(dest => dest.Reason, src => src.MapFrom(opt => opt.Reason))
            .ForMember(dest => dest.AgendaStatusType, src => src.MapFrom(opt => opt.AgendaStatus))
            .ForPath(dest => dest.Pet!.Name, src => src.MapFrom(opt => opt.PetName))
            .ForPath(dest => dest.Pet!.Specie, src => src.MapFrom(opt => opt.PetSpecie))
            .ForPath(dest => dest.PetOwner!.Contact!.Phone, src => src.MapFrom(opt => opt.OwnerPhone))
            .ForPath(dest => dest.PetOwner!.OwnerType,opt => opt.MapFrom(src => PetOwnerTypeHelper.ParsePetOwnerType(src.OwnerType)))
            .ForPath(dest => dest.PetOwner!.Document!.Code, src => src.MapFrom(opt => opt.OwnerDocument))
            .ForPath(dest => dest.PetOwner!.Document!.Type,src => src.MapFrom(opt => opt.DocType));

        CreateMap<AgendaDto, GetTemporaryAgendaByFiltersQueryResponseDetail>();
    }
}