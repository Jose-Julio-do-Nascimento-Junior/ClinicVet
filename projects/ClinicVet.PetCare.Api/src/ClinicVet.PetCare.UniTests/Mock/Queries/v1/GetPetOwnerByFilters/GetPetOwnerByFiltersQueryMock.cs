using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetOwnerByFilters;

public sealed class GetPetOwnerByFiltersQueryMock
{
    public static GetPetOwnerByFiltersQuery GetDefaultInstance()
    {
        return new Faker<GetPetOwnerByFiltersQuery>(Constants.Language)
           .RuleFor(query => query.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(query => query.Name, fakerMock => fakerMock.Person.FirstName)
           .RuleFor(query => query.Skip, fakerMock => fakerMock.Random.Int())
           .RuleFor(query => query.Take, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}