using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetOwnerParameter;

public sealed class PetOwnerParametersDtoMock
{
    public static PetOwnerParametersDto GetDefaultInstance()
    {
        return new Faker<PetOwnerParametersDto>(Constants.Language)
           .RuleFor(parametersMock => parametersMock.Name, fakerMock => fakerMock.Person.FirstName)
           .RuleFor(parametersMock => parametersMock.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.DocumentType, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.OwnerType, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.Phone, fakerMock => fakerMock.Person.Phone)
           .RuleFor(parametersMock => parametersMock.Street, fakerMock => fakerMock.Address.StreetAddress())
           .RuleFor(parametersMock => parametersMock.Number, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.City, fakerMock => fakerMock.Address.City())
           .RuleFor(parametersMock => parametersMock.State, fakerMock => fakerMock.Address.State())
           .RuleFor(parametersMock => parametersMock.ZipCode, fakerMock => fakerMock.Address.ZipCode())
           .Generate();
    }
}