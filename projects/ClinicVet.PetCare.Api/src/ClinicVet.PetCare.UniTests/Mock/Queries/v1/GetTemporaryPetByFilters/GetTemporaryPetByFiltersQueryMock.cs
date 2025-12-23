using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryPetByFilters;

public sealed class GetTemporaryPetByFiltersQueryMock
{
    public static GetTemporaryPetByFiltersQuery GetDefaultInstance()
    {
        return new Faker<GetTemporaryPetByFiltersQuery>(Constants.Language)
           .RuleFor(query => query.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(query => query.Skip, fakerMock => fakerMock.Random.Int())
           .RuleFor(query => query.Take, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}