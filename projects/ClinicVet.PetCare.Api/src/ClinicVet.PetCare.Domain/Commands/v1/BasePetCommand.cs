using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.Domain.Commands.v1;

public class BasePetCommand : Command
{
    public string? Name { get; set; }

    public string? Specie { get; set; }

    public string? Breed { get; set; }

    public string? BirthDate { get; set; }

    public Owner PetOwner { get; set; } = new();
}