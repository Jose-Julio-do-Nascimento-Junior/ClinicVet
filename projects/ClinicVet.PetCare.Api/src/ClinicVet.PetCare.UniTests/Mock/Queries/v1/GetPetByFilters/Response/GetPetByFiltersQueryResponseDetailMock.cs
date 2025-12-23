using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters.Responses;
using ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetByFilters.Response;

public sealed class GetPetByFiltersQueryResponseDetailMock
{
    public static List<GetPetByFiltersQueryResponseDetail> GetDefaultInstances()
    {
        return new List<GetPetByFiltersQueryResponseDetail> { GetDefaultInstance() };
    }

    public static GetPetByFiltersQueryResponseDetail GetDefaultInstance()
    {
        return new Faker<GetPetByFiltersQueryResponseDetail>(Constants.Language)
           .RuleFor(petDto => petDto.Name, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Specie, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.Breed, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.BirthDate, fakerMock => fakerMock.Random.String())
           .RuleFor(petDto => petDto.PetOwner, PetOwnerMock.GetDefaultInstance())
           .Generate();
    }
}