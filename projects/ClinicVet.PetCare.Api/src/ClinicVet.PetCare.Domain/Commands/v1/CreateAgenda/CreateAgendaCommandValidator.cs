using ClinicVet.PetCare.Domain.Resources.v1;
using FluentValidation;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandValidator : AbstractValidator<CreateAgendaCommand>
{
    public CreateAgendaCommandValidator()
    {
        RuleFor(command => command.Pet.Name)
           .NotEmpty()
           .WithMessage(Message.RequiredPetName);

        RuleFor(command => command.PetOwner.Document!.Code)
           .NotEmpty()
           .WithMessage(Message.RequiredDocument);
    }
}