using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.CreatePet;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreatePet;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetParameter;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreatePet;

public sealed class CreatePetCommandProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreatePetCommandProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should be parameters in mapping valid")]
    public void ShouldBeParametersInMappingValid()
    {
        var mapper = CreateMapper();
        var command = CreatePetCommandMock.GetDefaultInstance();
        var parametersDto = PetParameterDtoMock.GetDefaultInstance();

        mapper.Map(command, parametersDto);

        Assert.True(command.Name == parametersDto.Name);
        Assert.True(command.PetOwner.Document!.Code == parametersDto.PetOwnerDocument);
        Assert.True(command.BirthDate == parametersDto.BirthDate);
    }
}