using Newtonsoft.Json;

namespace ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;

public sealed record PetOwnerParametersDto
{
    [JsonProperty(PropertyName = "NAME")]
    public string? Name { get; init; }

    [JsonProperty(PropertyName = "DOCUMENT")]
    public string? Document { get; init; }

    [JsonProperty(PropertyName = "DOCUMENT_TYPE")]
    public string? DocumentType { get; init; }

    [JsonProperty(PropertyName = "OWNER_TYPE")]
    public string? OwnerType { get; init; }

    [JsonProperty(PropertyName = "PHONE")]
    public string? Phone { get; init; }

    [JsonProperty(PropertyName = "STREET")]
    public string? Street { get; init; }

    [JsonProperty(PropertyName = "RESIDENCE_NUMBER")]
    public string? Number { get; init; }

    [JsonProperty(PropertyName = "CITY")]
    public string? City { get; init; }

    [JsonProperty(PropertyName = "STATE")]
    public string? State { get; init; }

    [JsonProperty(PropertyName = "ZIP_CODE")]
    public string? ZipCode { get; init; }
}