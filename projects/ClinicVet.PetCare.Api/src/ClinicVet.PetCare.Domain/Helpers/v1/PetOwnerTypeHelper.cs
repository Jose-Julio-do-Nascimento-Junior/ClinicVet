using ClinicVet.PetCare.Domain.Fixeds.v1;

namespace ClinicVet.PetCare.Domain.Helpers.v1;

public static class PetOwnerTypeHelper
{
    public static PetOwnerType? ParsePetOwnerType(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;

        var petOwnerType = value.Trim().ToUpperInvariant();

        return petOwnerType switch
        {
            "PERMANENT" or "PERMANENTE" => PetOwnerType.Permanent,
            "TEMPORARY" or "TEMPORARIO" => PetOwnerType.Temporary,
            "NGO" or "ONG" => PetOwnerType.NGO,
            _ => default
        };
    }
}