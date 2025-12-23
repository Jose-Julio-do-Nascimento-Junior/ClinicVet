using ClinicVet.PetCare.Domain.Commands.v1.UpdatePet;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UpdatePet;
using FluentValidation.Results;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UpdatePet;

public sealed class UpdatePetCommandValidatorTests
{
    private static ValidationResult EstablishContext(UpdatePetCommand command)
    {
        var validator = new UpdatePetCommandValidator();
        return validator.Validate(command);
    }

    [Fact(DisplayName = "Should be parameters valid")]
    public void ShouldBeParametersValid()
    {
        var command = UpdatePetCommandMock.GetDefaultInstance();

        var result = EstablishContext(command);

        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Should be invalid parameters")]
    public void ShouldBeInvalidParameters()
    {
        var command = UpdatePetCommandMock.GetNullInstance();

        var result = EstablishContext(command);

        Assert.False(result.IsValid);
    }
}