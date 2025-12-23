using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePetOwner;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;
using ClinicVet.PetCare.UnitTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerDtoMock
{
    public static UpdatePetOwnerDto GetDefaultInstance()
    {
        return new Faker<UpdatePetOwnerDto>(Constants.Language)
           .RuleFor(petOwnerDto => petOwnerDto.Name, fakerMock => fakerMock.Person.FullName)
           .RuleFor(petOwnerDto => petOwnerDto.OwnerType, PetOwnerType.Permanent)
           .RuleFor(petOwnerDto => petOwnerDto.Document, DocumentMock.GetDefaultInstance())
           .RuleFor(petOwnerDto => petOwnerDto.Contact, ContactMock.GetDefaultInstance())
           .RuleFor(petOwnerDto => petOwnerDto.Address, AddressMock.GetDefaultInstance())
           .Generate();
    }
}