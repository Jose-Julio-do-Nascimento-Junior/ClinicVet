using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;

public sealed class PetByFiltersDtoMock
{
    public static PetByFiltersDto GetDefaultInstance()
    {
        return new Faker<PetByFiltersDto>(Constants.Language)
           .RuleFor(filterDto => filterDto.Document, fakerMock => fakerMock.Random.String())
           .RuleFor(filterDto => filterDto.Limit, fakerMock => fakerMock.Random.Int())
           .RuleFor(filterDto => filterDto.Offset, fakerMock => fakerMock.Random.Int())
           .Generate();
    }
}