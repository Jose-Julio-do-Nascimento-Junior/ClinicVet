using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters.Responses;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersQueryProfile : Profile
{
    public GetAgendaByFiltersQueryProfile()
    {
        CreateMap<GetAgendaByFiltersQuery, GetAgendaByFiltersDto>()
            .ForMember(dest => dest.Status, src => src.MapFrom(opt => opt.Status!.ToUpper()));

        CreateMap<AgendaDto, GetAgendaByFiltersQueryResponseDetail>();
    }
}