namespace ClinicVet.PetCare.Domain.Dtos.v1;

public record ParametersForPaginatedDto
{
    public int Offset { get; init; } = 0;

    public int Limit { get; init; } = 10;
}