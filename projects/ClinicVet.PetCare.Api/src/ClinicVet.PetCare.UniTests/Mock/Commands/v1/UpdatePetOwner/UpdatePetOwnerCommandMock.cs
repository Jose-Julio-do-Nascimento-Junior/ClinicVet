using Bogus;
using ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandMock
{
    public static UpdatePetOwnerCommand GetDefaultInstance()
    {
        return new Faker<UpdatePetOwnerCommand>(Constants.Language)
           .RuleFor(command => command.Name, fakerMock => fakerMock.Person.FullName)
           .RuleFor(command => command.OwnerType, PetOwnerType.Permanent)
           .RuleFor(command => command.Document, DocumentMock.GetDefaultInstance())
           .RuleFor(command => command.Contact, ContactMock.GetDefaultInstance())
           .RuleFor(command => command.Address, AddressMock.GetDefaultInstance())
           .Generate();
    }

    public static UpdatePetOwnerCommand GetNullInstance()
    {
        return new Faker<UpdatePetOwnerCommand>(Constants.Language)
           .RuleFor(command => command.Name, string.Empty)
           .RuleFor(command => command.Document, DocumentMock.GetNullInstance())
           .RuleFor(command => command.Contact, ContactMock.GetNullInstance())
           .RuleFor(command => command.Address, AddressMock.GetNulltInstance())
           .Generate();
    }
}