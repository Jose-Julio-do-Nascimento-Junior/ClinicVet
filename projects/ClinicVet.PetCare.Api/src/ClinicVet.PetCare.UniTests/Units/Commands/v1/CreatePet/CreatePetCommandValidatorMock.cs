using ClinicVet.PetCare.Domain.Commands.v1.CreatePet;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreatePet;
using FluentValidation.Results;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreatePet;

public sealed class CreatePetCommandValidatorMock
{
    private static ValidationResult EstablishContext(CreatePetCommand command)
    {
        var validator = new CreatePetCommandValidator();
        return validator.Validate(command);
    }

    [Fact(DisplayName = "Should be parameters valid")]
    public void ShouldBeParametersValid()
    {
        var command = CreatePetCommandMock.GetDefaultInstance();

        var result = EstablishContext(command);

        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Should be invalid parameters")]
    public void ShouldBeInvalidParameters()
    {
        var command = CreatePetCommandMock.GetNullInstance();

        var result = EstablishContext(command);

        Assert.False(result.IsValid);
    }
}