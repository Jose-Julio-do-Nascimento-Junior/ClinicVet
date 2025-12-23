namespace ClinicVet.PetCare.Domain.Dtos.v1;

public sealed record PetOwnerByFiltersDto : ParametersForPaginatedDto
{
    public string? Name { get; init; }

    public string? Document { get; init; }
}