using Bogus;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;

namespace ClinicVet.PetCare.UniTests.Mock.Dtos.v1.GetTemporaryAgendaByFilters;

public sealed class TemporaryAgendaDtoMock
{
    public static List<TemporaryAgendaDto> GetDefaultInstances()
    {
        return new List<TemporaryAgendaDto> { GetDefaultInstance() };
    }

    public static TemporaryAgendaDto GetDefaultInstance()
    {
        return new Faker<TemporaryAgendaDto>(Constants.Language)
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.AppointmentAt, DateTime.Now.ToString())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.Reason, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.AgendaStatus, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.PetName, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.PetSpecie, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.OwnerPhone, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.OwnerType, PetOwnerType.Permanent.ToString())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.OwnerDocument, fakerMock => fakerMock.Random.String())
           .RuleFor(temporaryAgendaDto => temporaryAgendaDto.DocType, DocumentType.CPF)
           .Generate();
    }

    public static List<TemporaryAgendaDto> GetEmptyInstances() => new List<TemporaryAgendaDto>();
}