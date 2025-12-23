using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1;

public record BasePetDto
{
    public string? Name { get; init; }

    public string? Specie { get; init; }

    public string? Breed { get; init; }

    public string? BirthDate { get; init; }

    public PetOwner? PetOwner { get; init; }
}