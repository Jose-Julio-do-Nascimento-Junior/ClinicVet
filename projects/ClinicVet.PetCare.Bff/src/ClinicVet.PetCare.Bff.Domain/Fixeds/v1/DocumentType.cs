using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ClinicVet.PetCare.Domain.Fixeds.v1;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DocumentType
{
    [EnumMember(Value = "CPF")]
    CPF,

    [EnumMember(Value = "CNPJ")]
    CNPJ
}