using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Dtos.v1;

public record BaseAgendaParametersDto
{
    public DateTime? AppointmentAt { get; init; }

    public string? Reason { get; init; }

    public AgendaStatusType? Status { get; init; }

    public Pet Pet { get; init; } = new();

    public PetOwner PetOwner { get; init; } = new();
}