using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersDtoMock
{
    public static GetAgendaByFiltersDto GetDefaultInstance()
    {
        return new Faker<GetAgendaByFiltersDto>(Constants.Language)
           .RuleFor(filterDto => filterDto.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(filterDto => filterDto.Status, fakerMock => fakerMock.Random.String())
           .RuleFor(filterDto => filterDto.Limit, fakerMock => fakerMock.Random.Int())
           .RuleFor(filterDto => filterDto.Offset, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}