using System.Runtime.Serialization;

namespace ClinicVet.PetManager.Job.Domain.Fixeds.v1;

public enum OwnerType
{
    [EnumMember(Value = "TEMPORARIO")]
    Temporary
}