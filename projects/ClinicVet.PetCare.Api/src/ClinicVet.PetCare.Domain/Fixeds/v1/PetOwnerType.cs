using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ClinicVet.PetCare.Domain.Fixeds.v1;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PetOwnerType
{
    [EnumMember(Value = "PERMANENTE")]
    Permanent,

    [EnumMember(Value = "TEMPORARIO")]
    Temporary,

    [EnumMember(Value = "ONG")]
    NGO
}