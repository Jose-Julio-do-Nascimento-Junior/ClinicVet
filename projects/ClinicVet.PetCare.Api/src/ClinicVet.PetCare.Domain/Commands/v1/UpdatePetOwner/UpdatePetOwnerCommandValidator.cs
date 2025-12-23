using ClinicVet.PetCare.Domain.Resources.v1;
using FluentValidation;

namespace ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandValidator : AbstractValidator<UpdatePetOwnerCommand>
{
    public UpdatePetOwnerCommandValidator()
    {
        RuleFor(command => command.Document!.Code)
           .NotEmpty()
           .WithMessage(Message.RequiredDocument);
    }
}