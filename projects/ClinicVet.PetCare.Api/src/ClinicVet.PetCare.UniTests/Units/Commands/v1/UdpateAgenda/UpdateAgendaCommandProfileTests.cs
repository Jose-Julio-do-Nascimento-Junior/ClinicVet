using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UdpateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.AgendaParameter;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UdpateAgenda;

public sealed class UpdateAgendaCommandProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UpdateAgendaCommandProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should be parameters in mapping valid")]
    public void ShouldBeParametersInMappingValid()
    {
        var mapper = CreateMapper();
        var command = UpdateAgendaCommandMock.GetDefaultInstance();
        var parametersDto = AgendaParameterDtoMock.GetDefaultInstance();

        mapper.Map(command, parametersDto);

        Assert.True(command.Pet.Name == parametersDto.PetName);
        Assert.True(command.Pet.Specie == parametersDto.PetSpecie);
        Assert.True(command.PetOwner.Document!.Code == parametersDto.OwnerDocument);
    }
}