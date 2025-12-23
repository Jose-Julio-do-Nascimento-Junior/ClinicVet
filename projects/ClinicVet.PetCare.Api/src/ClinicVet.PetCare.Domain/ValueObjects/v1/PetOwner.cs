using ClinicVet.PetCare.Domain.Fixeds.v1;
using System.Text.Json.Serialization;

namespace ClinicVet.PetCare.Domain.ValueObjects.v1;

public sealed record PetOwner
{
    [JsonIgnore]
    public string? OwnerName { get; init; }

    public PetOwnerType? OwnerType { get; init; }

    public Contact? Contact { get; init; }

    public Document? Document { get; init; }
}