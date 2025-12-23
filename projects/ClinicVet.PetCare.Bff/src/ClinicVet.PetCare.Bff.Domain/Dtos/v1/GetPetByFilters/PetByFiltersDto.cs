namespace ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;

public sealed record PetByFiltersDto : ParametersForPaginatedDto
{
    public string? Document { get; init; }
}