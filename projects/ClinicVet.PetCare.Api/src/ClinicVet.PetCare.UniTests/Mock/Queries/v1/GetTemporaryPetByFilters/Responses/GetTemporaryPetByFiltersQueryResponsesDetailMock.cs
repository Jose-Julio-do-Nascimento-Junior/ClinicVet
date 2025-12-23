using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryPetByFilters.Responses;

public sealed class GetTemporaryPetByFiltersQueryResponsesDetailMock
{
    public static List<GetTemporaryPetByFiltersQueryResponsesDetail> GetDefaultInstances()
    {
        return new List<GetTemporaryPetByFiltersQueryResponsesDetail> { GetDefaultInstance() };
    }

    public static GetTemporaryPetByFiltersQueryResponsesDetail GetDefaultInstance()
    {
        return new Faker<GetTemporaryPetByFiltersQueryResponsesDetail>(Constants.Language)
           .RuleFor(petDto => petDto.Name, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.BirthDate, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}