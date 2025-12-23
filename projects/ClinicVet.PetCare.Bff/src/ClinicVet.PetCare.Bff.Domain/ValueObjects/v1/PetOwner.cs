using System.Text.Json.Serialization;

namespace ClinicVet.PetCare.Domain.ValueObjects.v1;

public sealed record PetOwner
{
    [JsonIgnore]
    public string? OwnerName { get; init; }

    public string? OwnerType { get; init; }

    public Contact? Contact { get; init; }

    public Document? Document { get; init; }
}