using AutoMapper;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetPetOwnerByFilters.Responses;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetPetOwnerByFilters;

public sealed class GetPetOwnerByFiltersQueryProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetPetOwnerByFiltersQueryProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should map valid parameters in filters")]
    public void ShouldMapValidParametersInFilters()
    {
        var mapper = CreateMapper();
        var query = GetPetOwnerByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetOwnerByFiltersDtoMock.GetDefaultInstance();

        mapper.Map(query, filterDto);

        Assert.True(query.Name == filterDto.Name);
        Assert.True(query.Document == filterDto.Document);
        Assert.True(query.Skip == filterDto.Skip);
        Assert.True(query.Take == filterDto.Take);
    }

    [Fact(DisplayName = "Should map valid parameters")]
    public void ShouldMapValidParameters()
    {
        var mapper = CreateMapper();
        var petOwnerDto = PetOwnerDtoMock.GetDefaultInstance();
        var responseDetail = GetPetOwnerByFiltersQueryResponseDetailMock.GetDefaultInstance();

        mapper.Map(petOwnerDto, responseDetail);

        Assert.True(petOwnerDto.Name == responseDetail.Name);
        Assert.True(petOwnerDto.OwnerType == responseDetail.OwnerType);
        Assert.True(petOwnerDto.Document!.Code == responseDetail.Document!.Code);
        Assert.True(petOwnerDto.Document.Type == responseDetail.Document.Type);
        Assert.True(petOwnerDto.Contact!.Phone == responseDetail.Contact!.Phone);
        Assert.True(petOwnerDto.Address!.Street == responseDetail.Address!.Street);
        Assert.True(petOwnerDto.Address.Number == responseDetail.Address.Number);
        Assert.True(petOwnerDto.Address.City == responseDetail.Address.City);
        Assert.True(petOwnerDto.Address.State == responseDetail.Address.State);
        Assert.True(petOwnerDto.Address.ZipCode == responseDetail.Address.ZipCode);
    }
}