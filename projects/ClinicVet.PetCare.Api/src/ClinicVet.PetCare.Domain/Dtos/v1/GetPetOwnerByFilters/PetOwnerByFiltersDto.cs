namespace ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;

public sealed record PetOwnerByFiltersDto : ParametersForPaginatedQuery
{
    public string? Name { get; init; }

    public string? Document { get; init; }
}