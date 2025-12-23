using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetByFilters;

public sealed class GetPetByFiltersQueryMock
{
    public static GetPetByFiltersQuery GetDefaultInstance()
    {
        return new Faker<GetPetByFiltersQuery>(Constants.Language)
           .RuleFor(query => query.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(query => query.Skip, fakerMock => fakerMock.Random.Int())
           .RuleFor(query => query.Take, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}