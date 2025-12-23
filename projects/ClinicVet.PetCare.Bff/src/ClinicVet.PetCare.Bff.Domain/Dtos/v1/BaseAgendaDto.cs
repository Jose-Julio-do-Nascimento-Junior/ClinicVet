using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1;

public record BaseAgendaDto
{
    public string? AppointmentAt { get; init; }

    public string? Reason { get; init; }

    public string? AgendaStatusType { get; init; }

    public Pet? Pet { get; init; }

    public PetOwner? PetOwner { get; init; }
}