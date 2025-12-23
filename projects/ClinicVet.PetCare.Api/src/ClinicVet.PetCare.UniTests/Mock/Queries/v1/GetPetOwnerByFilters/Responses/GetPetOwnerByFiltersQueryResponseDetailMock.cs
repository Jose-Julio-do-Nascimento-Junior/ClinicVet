using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetOwnerByFilters.Responses;

public sealed class GetPetOwnerByFiltersQueryResponseDetailMock
{
    public static List<GetPetOwnerByFiltersQueryResponseDetail> GetDefaultInstances()
    {
        return new List<GetPetOwnerByFiltersQueryResponseDetail> { GetDefaultInstance() };
    }

    public static GetPetOwnerByFiltersQueryResponseDetail GetDefaultInstance()
    {
        return new Faker<GetPetOwnerByFiltersQueryResponseDetail>(Constants.Language)
           .RuleFor(petOwnerDto => petOwnerDto.Name, fakerMock => fakerMock.Person.FullName)
           .RuleFor(petOwnerDto => petOwnerDto.OwnerType, fakerMock => fakerMock.Random.String())
           .RuleFor(petOwnerDto => petOwnerDto.CreatedAt, DateTime.Now.ToString())
           .RuleFor(petOwnerDto => petOwnerDto.Document, DocumentMock.GetDefaultInstance())
           .RuleFor(petOwnerDto => petOwnerDto.Contact, ContactMock.GetDefaultInstance())
           .RuleFor(petOwnerDto => petOwnerDto.Address, AddressMock.GetDefaultInstance())
           .Generate();
    }
}