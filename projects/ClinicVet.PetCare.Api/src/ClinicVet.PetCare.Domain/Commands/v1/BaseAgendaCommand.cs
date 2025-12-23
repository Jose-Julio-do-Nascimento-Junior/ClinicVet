using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Commands.v1;

public class BaseAgendaCommand : Command
{
    public DateTime? AppointmentAt { get; set; }

    public string? Reason { get; set; }

    public AgendaStatusType? Status { get; set; }

    public Pet Pet { get; set; } = new();

    public PetOwner PetOwner { get; set; } = new();
}