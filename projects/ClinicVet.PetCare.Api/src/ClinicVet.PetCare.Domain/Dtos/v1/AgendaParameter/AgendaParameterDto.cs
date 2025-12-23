using Newtonsoft.Json;

namespace ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;

public sealed record AgendaParameterDto
{
    [JsonProperty(PropertyName = "P_APPOINTMENT_AT")]
    public DateTime AppointmentAt { get; init; }

    [JsonProperty(PropertyName = "P_REASON")]
    public string? Reason { get; init; }

    [JsonProperty(PropertyName = "P_STATUS")]
    public string? Status { get; init; }

    [JsonProperty(PropertyName = "P_PET_NAME")]
    public string? PetName { get; init; }

    [JsonProperty(PropertyName = "P_PET_SPECIES")]
    public string? PetSpecie { get; init; }

    [JsonProperty(PropertyName = "P_OWNER_DOCUMENT")]
    public string? OwnerDocument { get; init; }

    [JsonProperty(PropertyName = "P_OWNER_PHONE")]
    public string? OwnerPhone { get; init; }

    [JsonProperty(PropertyName = "P_OWNER_TYPE")]
    public string? OwnerType { get; init; }

    [JsonProperty(PropertyName = "P_DOC_TYPE")]
    public string? DocumentType { get; init; }
}