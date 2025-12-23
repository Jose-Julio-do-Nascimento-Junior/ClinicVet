using ClinicVet.PetCare.Domain.Fixeds.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetByFilters;

public sealed record TemporaryPetDto
{
    public string? PetName { get; init; }

    public string? PetSpecie { get; init; }

    public string? PetBreed { get; init; }

    public string? PetBirthDate { get; init; }

    public string? OwnerDocument { get; init; }

    public DocumentType? DocType { get; init; }
}