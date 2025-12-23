using Bogus;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

public sealed class DocumentMock
{
    public static Document GetDefaultInstance()
    {
        return new Faker<Document>(Constants.Language)
           .RuleFor(operatorMock => operatorMock.Code, fakerMock => fakerMock.Random.String())
           .RuleFor(operatorMock => operatorMock.Type, DocumentType.CPF)
           .Generate();
    }

    public static Document GetNullInstance()
       => new Faker<Document>(Constants.Language).Generate();
}