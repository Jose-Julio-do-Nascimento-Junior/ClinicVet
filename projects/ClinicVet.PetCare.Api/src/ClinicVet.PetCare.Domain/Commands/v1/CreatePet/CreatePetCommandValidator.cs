using ClinicVet.PetCare.Domain.Resources.v1;
using FluentValidation;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreatePet;

public sealed class CreatePetCommandValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetCommandValidator()
    {
        RuleFor(command => command.PetOwner.Document!.Code)
           .NotEmpty()
           .WithMessage(Message.RequiredDocument);
    }
}