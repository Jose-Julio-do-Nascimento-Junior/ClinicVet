using ClinicVet.PetCare.Domain.Resources.v1;
using FluentValidation;

namespace ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;

public sealed class UpdateAgendaCommandValidator : AbstractValidator<UpdateAgendaCommand>
{
    public UpdateAgendaCommandValidator()
    {

        RuleFor(command => command.Pet!.Name)
           .NotEmpty()
           .WithMessage(Message.RequiredPetName);

        RuleFor(command => command.PetOwner.Document)
           .NotEmpty()
           .WithMessage(Message.RequiredDocument);
    }
}