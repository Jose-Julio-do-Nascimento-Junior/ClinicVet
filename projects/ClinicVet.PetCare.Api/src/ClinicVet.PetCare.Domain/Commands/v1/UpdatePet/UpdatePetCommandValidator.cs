using ClinicVet.PetCare.Domain.Resources.v1;
using FluentValidation;

namespace ClinicVet.PetCare.Domain.Commands.v1.UpdatePet;

public sealed class UpdatePetCommandValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetCommandValidator()
    {
        RuleFor(command => command.Name)
          .NotEmpty()
          .WithMessage(Message.RequiredPetName);

        RuleFor(command => command.PetOwner.Document)
          .NotEmpty()
          .WithMessage(Message.RequiredDocument);
    }
}