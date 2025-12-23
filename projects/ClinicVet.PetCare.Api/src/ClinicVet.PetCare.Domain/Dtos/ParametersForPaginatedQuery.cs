namespace ClinicVet.PetCare.Domain.Dtos;

public record ParametersForPaginatedQuery
{
    public int Take { get; init; }

    public int Skip { get; init; }
}