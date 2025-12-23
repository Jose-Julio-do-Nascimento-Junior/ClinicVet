using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1;

public record BasePetOwnerParametersDto
{
    public string Name { get; init; } = string.Empty;

    public PetOwnerType? OwnerType { get; init; }

    public Document? Document { get; init; }

    public Contact? Contact { get; init; }

    public Address Address { get; init; } = new();
}