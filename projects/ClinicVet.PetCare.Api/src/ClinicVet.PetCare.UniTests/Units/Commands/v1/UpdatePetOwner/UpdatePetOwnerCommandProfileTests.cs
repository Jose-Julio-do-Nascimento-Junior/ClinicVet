using AutoMapper;
using ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.UniTests.Mock.Dtos.v1.PetOwnerParameter;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandProfileTests
{
    private static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<UpdatePetOwnerCommandProfile>());
        return config.CreateMapper();
    }

    [Fact(DisplayName = "Should be parameters in mapping valid")]
    public void ShouldBeParametersInMappingValid()
    {
        var mapper = CreateMapper();
        var command = UpdatePetOwnerCommandMock.GetDefaultInstance();
        var parametersDto = PetOwnerParametersDtoMock.GetDefaultInstance();

        mapper.Map(command, parametersDto);

        Assert.True(command.Name == parametersDto.Name);
        Assert.True(command.Document!.Code == parametersDto.Document);
        Assert.True(command.Contact!.Phone == parametersDto.Phone);
        Assert.True(command.Address.Street == parametersDto.Street);
        Assert.True(command.Address.Number == parametersDto.Number);
        Assert.True(command.Address.City == parametersDto.City);
        Assert.True(command.Address.State == parametersDto.State);
        Assert.True(command.Address.ZipCode == parametersDto.ZipCode);
    }
}