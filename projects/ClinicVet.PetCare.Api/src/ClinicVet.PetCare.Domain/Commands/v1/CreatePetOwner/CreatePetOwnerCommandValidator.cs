using ClinicVet.PetCare.Domain.Resources.v1;
using FluentValidation;

namespace ClinicVet.PetCare.Domain.Commands.v1.CreatePetOwner;

public sealed class CreatePetOwnerCommandValidator : AbstractValidator<CreatePetOwnerCommand>
{
    public CreatePetOwnerCommandValidator()
    {
        RuleFor(command => command.Name)
           .NotEmpty()
           .WithMessage(Message.RequiredName);

        RuleFor(command => command.Document!.Code)
           .NotEmpty()
           .WithMessage(Message.RequiredDocument);

        RuleFor(command => command.Address.Street)
           .NotEmpty()
           .WithMessage(Message.RequiredStreet);

        RuleFor(command => command.Address.City)
           .NotEmpty()
           .WithMessage(Message.RequiredCity);

        RuleFor(command => command.Address.State)
           .NotEmpty()
           .WithMessage(Message.RequiredState);

        RuleFor(command => command.Address.ZipCode)
           .NotEmpty()
           .WithMessage(Message.RequiredZipCode);
    }
}