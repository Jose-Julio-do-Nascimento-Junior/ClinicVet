using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Commands.v1;

public class BasePetOnwerCommand : Command
{
    public string Name { get; set; } = string.Empty;

    public PetOwnerType? OwnerType { get; set; }

    public Document? Document { get; set; }

    public Contact? Contact { get; set; }

    public Address Address { get; set; } = new();
}