using System.Runtime.Serialization;

namespace ClinicVet.AgendaStatus.Job.Domain.Fixeds.v1;

public enum AgendaStatusType
{
    [EnumMember(Value = "AUSENTE")]
    Absent,

    [EnumMember(Value = "AGENDADO")]
    Scheduled
}