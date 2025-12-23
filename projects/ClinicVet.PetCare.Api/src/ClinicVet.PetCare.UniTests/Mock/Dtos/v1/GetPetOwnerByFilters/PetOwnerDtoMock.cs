using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetOwnerByFilters;

public sealed class PetOwnerDtoMock
{
    public static List<PetOwnerDto> GetDefaultInstances()
    {
        return new List<PetOwnerDto> { GetDefaultInstance() };
    }

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

    public static List<PetOwnerDto> GetEmptyInstances() => new List<PetOwnerDto>();
}