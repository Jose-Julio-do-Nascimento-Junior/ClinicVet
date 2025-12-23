namespace ClinicVet.PetCare.Domain.ValueObjects.v1;

public sealed record Owner
{
    public Document? Document { get; init; }
}