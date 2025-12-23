using AutoMapper;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.UniTests.Mock.Queries.v1.GetTemporaryPetByFilters.Responses;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Queries.v1.GetTemporaryPetByFilters;

public sealed class GetTemporaryPetByFiltersQueryProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<GetTemporaryPetByFiltersQueryProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should map valid parameters in filters")]
    public void ShouldMapValidParametersInFilters()
    {
        var mapper = CreateMapper();
        var query = GetTemporaryPetByFiltersQueryMock.GetDefaultInstance();
        var filterDto = PetByFiltersDtoMock.GetDefaultInstance();

        mapper.Map(query, filterDto);

        Assert.True(query.Document == filterDto.Document);
        Assert.True(query.Skip == filterDto.Skip);
        Assert.True(query.Take == filterDto.Take);
    }

    [Fact(DisplayName = "Should maping return valid for temporary pets")]
    public void ShouldMapingReturnValidForTemporaryPets()
    {
        var mapper = CreateMapper();
        var petDto = PetDtoMock.GetDefaultInstance();
        var temporaryPetDto = TemporaryPetDtoMock.GetDefaultInstance();

        mapper.Map(temporaryPetDto, petDto);

        Assert.True(petDto.Name == temporaryPetDto.PetName);
        Assert.True(petDto.Specie == temporaryPetDto.PetSpecie);
        Assert.True(petDto.BirthDate == temporaryPetDto.PetBirthDate);
        Assert.True(petDto.Breed == temporaryPetDto.PetBreed);
        Assert.True(petDto.PetOwner!.Document!.Code == temporaryPetDto.OwnerDocument);
        Assert.True(petDto.PetOwner.Document.Type == temporaryPetDto.DocType);
    }

    [Fact(DisplayName = "Should map valid parameters")]
    public void ShouldMapValidParameters()
    {
        var mapper = CreateMapper();
        var petDto = PetDtoMock.GetDefaultInstance();
        var responseDetail = GetTemporaryPetByFiltersQueryResponsesDetailMock.GetDefaultInstance();

        mapper.Map(petDto, responseDetail);
        Assert.True(responseDetail.Name == petDto.Name);
        Assert.True(responseDetail.Specie == petDto.Specie);
        Assert.True(responseDetail.PetOwner == petDto.PetOwner);
    }
}