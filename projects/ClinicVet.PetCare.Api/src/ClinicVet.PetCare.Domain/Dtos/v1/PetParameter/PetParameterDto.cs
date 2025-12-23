using Newtonsoft.Json;

namespace ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;

public sealed record PetParameterDto
{
    [JsonProperty(PropertyName = "P_NAME")]
    public string? Name { get; init; }

    [JsonProperty(PropertyName = "P_SPECIES")]
    public string? Specie { get; init; }

    [JsonProperty(PropertyName = "P_BREED")]
    public string? Breed { get; init; }

    [JsonProperty(PropertyName = "P_BIRTH_DATE")]
    public string? BirthDate { get; init; }

    [JsonProperty(PropertyName = "P_OWNER_DOC")]
    public string? PetOwnerDocument { get; init; }

    [JsonProperty(PropertyName = "P_DOC_TYPE")]
    public string? DocumentType { get; init; }
}