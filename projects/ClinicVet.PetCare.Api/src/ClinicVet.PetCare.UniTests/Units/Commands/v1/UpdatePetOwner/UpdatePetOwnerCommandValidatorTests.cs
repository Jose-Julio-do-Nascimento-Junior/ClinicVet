using ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.UpdatePetOwner;
using FluentValidation.Results;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.UpdatePetOwner;

public sealed class UpdatePetOwnerCommandValidatorTests
{
    private static ValidationResult EstablishContext(UpdatePetOwnerCommand command)
    {
        var validator = new UpdatePetOwnerCommandValidator();
        return validator.Validate(command);
    }

    [Fact(DisplayName = "Should be parameters valid")]
    public void ShouldBeParametersValid()
    {
        var command = UpdatePetOwnerCommandMock.GetDefaultInstance();

        var result = EstablishContext(command);

        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Should be invalid parameters")]
    public void ShouldBeInvalidParameters()
    {
        var command = UpdatePetOwnerCommandMock.GetNullInstance();

        var result = EstablishContext(command);

        Assert.False(result.IsValid);
    }
}