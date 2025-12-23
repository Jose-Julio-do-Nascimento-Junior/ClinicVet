using ClinicVet.PetCare.Domain.Dtos;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;

public sealed class GetPetOwnerByFiltersQuery : BaseFiltersQueryDto
{
    public string? Document { get; set; }

    public string? Name { get; set; }
}