using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;
using ClinicVet.PetCare.UnitTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.Dtos.v1.GetPetOwnerByFilters;

public sealed class PetOwnerDtoMock
{
    public static PetOwnerDto GetDefaultInstance()
    {
        return new Faker<PetOwnerDto>(Constants.Language)
            .RuleFor(petOwnerDto => petOwnerDto.Name, fakerMock => fakerMock.Person.FullName)
            .RuleFor(petOwnerDto => petOwnerDto.OwnerType, fakerMock => fakerMock.Random.String())
            .RuleFor(petOwnerDto => petOwnerDto.CreatedAt, DateTime.Now.ToString())
            .RuleFor(petOwnerDto => petOwnerDto.Document, DocumentMock.GetDefaultInstance())
            .RuleFor(petOwnerDto => petOwnerDto.Contact, ContactMock.GetDefaultInstance())
            .RuleFor(petOwnerDto => petOwnerDto.Address, AddressMock.GetDefaultInstance())
            .Generate();
    }
}