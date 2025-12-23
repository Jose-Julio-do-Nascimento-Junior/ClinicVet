using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

public sealed class OwnerMock
{
    public static Owner GetDefaultInstance()
    {
        return new Faker<Owner>(Constants.Language)
           .RuleFor(ownerMock => ownerMock.Document, DocumentMock.GetDefaultInstance())
           .Generate();
    }

    public static Owner GetNullInstance()
    {
        return new Faker<Owner>(Constants.Language)
           .RuleFor(ownerMock => ownerMock.Document, DocumentMock.GetNullInstance())
           .Generate();
    }
}