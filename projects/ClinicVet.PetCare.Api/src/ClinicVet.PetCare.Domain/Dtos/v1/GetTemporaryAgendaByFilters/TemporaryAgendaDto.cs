using ClinicVet.PetCare.Domain.Fixeds.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaByFilters;

public sealed record TemporaryAgendaDto
{
    public string? AppointmentAt { get; init; }
 
    public string? Reason { get; init; }

    public string? AgendaStatus { get; init; }

    public string? PetName { get; init; }

    public string? PetSpecie { get; init; }

    public string? OwnerPhone { get; init; }

    public string? OwnerType { get; init; }
 
    public string? OwnerDocument { get; init; }

    public DocumentType? DocType { get; init; }
}