using ClinicVet.PetCare.Domain.Fixeds.v1;
using System.Text.Json.Serialization;

namespace ClinicVet.PetCare.Domain.ValueObjects.v1;

public sealed record Document
{
    public string? Code { get; init; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DocumentType? Type { get; init; }
}