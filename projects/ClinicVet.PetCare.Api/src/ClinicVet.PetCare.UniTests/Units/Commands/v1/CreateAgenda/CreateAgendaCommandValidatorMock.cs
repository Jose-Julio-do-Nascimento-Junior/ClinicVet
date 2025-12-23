using ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.UniTests.Mock.Commands.v1.CreateAgenda;
using FluentValidation.Results;
using Xunit;

namespace ClinicVet.PetCare.UniTests.Units.Commands.v1.CreateAgenda;

public sealed class CreateAgendaCommandValidatorMock
{
    private static ValidationResult EstablishContext(CreateAgendaCommand command)
    {
        var validator = new CreateAgendaCommandValidator();
        return validator.Validate(command);
    }

    [Fact(DisplayName = "Should be parameters valid")]
    public void ShouldBeParametersValid()
    {
        var command = CreateAgendaCommandMock.GetDefaultInstance();

        var result = EstablishContext(command);

        Assert.True(result.IsValid);
    }

    [Fact(DisplayName = "Should be invalid parameters")]
    public void ShouldBeInvalidParameters()
    {
        var command = CreateAgendaCommandMock.GetNullInstance();

        var result = EstablishContext(command);

        Assert.False(result.IsValid);
    }
}