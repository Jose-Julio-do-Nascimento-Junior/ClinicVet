namespace ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;

public sealed record PetByFiltersDto : ParametersForPaginatedQuery
{
    public string? Document { get; init; }
}