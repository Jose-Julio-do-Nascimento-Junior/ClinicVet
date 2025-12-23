using AutoMapper;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetByFilters.Response;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetPetByFilters;

public sealed class GetPetByFiltersQueryProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetPetByFiltersQueryProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should map valid parameters in filters")]
    public void ShouldMapValidParametersInFilters()
    {
        var mapper = CreateMapper();
        var query = GetPetByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetByFiltersDtoMock.GetDefaultInstance();

        mapper.Map(query, filterDto);

        Assert.True(query.Document == filterDto.Document);
        Assert.True(query.Skip == filterDto.Skip);
        Assert.True(query.Take == filterDto.Take);
    }

    [Fact(DisplayName = "Should map valid parameters")]
    public void ShouldMapValidParameters()
    {
        var mapper = CreateMapper();
        var petDto = PetDtoMock.GetDefaultInstance();
        var resposneDetail = GetPetByFiltersQueryResponseDetailMock.GetDefaultInstance();

        mapper.Map(petDto, resposneDetail);

        Assert.True(petDto.Name == resposneDetail.Name);
        Assert.True(petDto.PetOwner == resposneDetail.PetOwner);
    }
}