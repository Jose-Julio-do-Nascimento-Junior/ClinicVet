using ClinicVet.PetCare.Domain.Dtos;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;

public sealed class GetTemporaryPetByFiltersQuery : BaseFiltersQueryDto
{
    public string? Document { get; set; }
}