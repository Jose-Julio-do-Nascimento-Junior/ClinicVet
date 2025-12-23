using ClinicVet.PetCare.Domain.Fixeds.v1;

namespace ClinicVet.PetCare.Domain.ValueObjects.v1;

public sealed record Document
{
    public string? Code { get; init; }

    public DocumentType? Type { get; init; }
}