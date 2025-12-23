using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ClinicVet.PetCare.Domain.Fixeds.v1;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AgendaStatusType
{
    [EnumMember(Value = "AGENDADO")]
    Scheduled,

    [EnumMember(Value = "CANCELADO")]
    Canceled,

    [EnumMember(Value = "ATENDIDO")]
    Attended,

    [EnumMember(Value = "AUSENTE")]
    Absent
}