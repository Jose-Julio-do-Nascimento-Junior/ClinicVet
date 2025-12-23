using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1;

public record BasePetParametersDto
{
    public string? Name { get; init; }

    public string? Specie { get; init; }

    public string? Breed { get; init; }

    public string? BirthDate { get; init; }

    public Owner PetOwner { get; init; } = new();
}