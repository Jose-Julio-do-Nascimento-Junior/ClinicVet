using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetParameter;

public sealed class PetParameterDtoMock
{
    public static PetParameterDto GetDefaultInstance()
    {
        return new Faker<PetParameterDto>(Constants.Language)
           .RuleFor(parametersMock => parametersMock.Name, fakerMock => fakerMock.Name.FirstName())
           .RuleFor(parametersMock => parametersMock.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.BirthDate, DateTime.Now.ToString())
           .RuleFor(parametersMock => parametersMock.PetOwnerDocument, fakerMock => fakerMock.Random.String())
           .RuleFor(parametersMock => parametersMock.DocumentType, fakerMock => fakerMock.Random.String())
           .Generate();
    }
}