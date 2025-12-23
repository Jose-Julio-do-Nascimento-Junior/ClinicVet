using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetTemporaryPetByFilters;

public sealed class TemporaryPetDtoMock
{
    public static List<TemporaryPetDto>GetDefaultInstances()
    {
        return new List<TemporaryPetDto> { GetDefaultInstance() };
    }

    public static TemporaryPetDto GetDefaultInstance()
    {
        return new Faker<TemporaryPetDto>(Constants.Language)
           .RuleFor(temporaryPetDto => temporaryPetDto.PetName, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryPetDto => temporaryPetDto.PetSpecie, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryPetDto => temporaryPetDto.PetBreed, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryPetDto => temporaryPetDto.PetBirthDate, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryPetDto => temporaryPetDto.OwnerDocument, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryPetDto => temporaryPetDto.DocType, DocumentType.CPF)
           .Generate();
    }

    public static List<TemporaryPetDto> GetEmptyInstances() => new List<TemporaryPetDto>();
}