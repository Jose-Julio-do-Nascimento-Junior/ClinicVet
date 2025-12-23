using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;

public record PetOwnerDto
{
    public string? Name { get; init; }

    public string? OwnerType { get; init; }

    public string? CreatedAt { get; init; }

    public Document? Document { get; init; }

    public Contact? Contact { get; init; }

    public Address? Address { get; init; }
}