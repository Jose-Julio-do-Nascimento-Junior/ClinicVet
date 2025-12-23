using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters.Responses;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;

public sealed class GetPetByFiltersQueryProfile : Profile
{
    public GetPetByFiltersQueryProfile()
    {
        CreateMap<GetPetByFiltersQuery, PetByFiltersDto>();

        CreateMap<PetDto, GetPetByFiltersQueryResponseDetail>();
    }
}