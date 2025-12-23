using ClinicVet.PetCare.Domain.Dtos;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;

public sealed class GetPetByFiltersQuery : BaseFiltersQueryDto
{
    public string? Document { get; set; }
}