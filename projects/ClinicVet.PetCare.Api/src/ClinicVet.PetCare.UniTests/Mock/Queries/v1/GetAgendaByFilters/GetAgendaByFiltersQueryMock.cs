using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;

namespace ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersQueryMock
{
    public static GetAgendaByFiltersQuery GetDefaultInstance()
    {
        return new Faker<GetAgendaByFiltersQuery>(Constants.Language)
           .RuleFor(query => query.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(query => query.Status, fakerMock => fakerMock.Random.String())
           .RuleFor(query => query.Skip, fakerMock => fakerMock.Random.Int())
           .RuleFor(query => query.Take, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}