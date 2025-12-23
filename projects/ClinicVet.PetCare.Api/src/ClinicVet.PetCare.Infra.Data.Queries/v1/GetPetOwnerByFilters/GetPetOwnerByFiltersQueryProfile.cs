using AutoMapper;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters.Responses;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;

public sealed class GetPetOwnerByFiltersQueryProfile : Profile
{
    public GetPetOwnerByFiltersQueryProfile()
    {
        CreateMap<GetPetOwnerByFiltersQuery, PetOwnerByFiltersDto>();

        CreateMap<PetOwnerDto, GetPetOwnerByFiltersQueryResponseDetail>();
    }
}