namespace ClinicVet.PetCare.Domain.ValueObjects.v1;

public sealed record Pet
{
    public string? Name { get; init; }

    public string? Specie { get; init; }
}