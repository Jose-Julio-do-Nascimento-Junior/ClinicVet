using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.AgendaParameter;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CreateAgendaCommandProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should be parameters in mapping valid")]
    public void ShouldBeParametersInMappingValid()
    {
        var mapper = CreateMapper();
        var command = CreateAgendaCommandMock.GetDefaultInstance();
        var parametersDto = AgendaParameterDtoMock.GetDefaultInstance();

        mapper.Map(command, parametersDto);

        Assert.True(command.Pet.Name == parametersDto.PetName);
        Assert.True(command.Pet.Specie == parametersDto.PetSpecie);
        Assert.True(command.PetOwner.Document!.Code == parametersDto.OwnerDocument);
    }
}