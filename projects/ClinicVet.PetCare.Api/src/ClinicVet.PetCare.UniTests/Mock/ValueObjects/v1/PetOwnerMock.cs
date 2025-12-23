using Bogus;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

public sealed class PetOwnerMock
{
    public static PetOwner GetDefaultInstance()
    {
        return new Faker<PetOwner>(Constants.Language)
           .RuleFor(petOwnerMock => petOwnerMock.OwnerName, fakerMock => fakerMock.Person.FirstName)
           .RuleFor(petOwnerMock => petOwnerMock.OwnerType, PetOwnerType.Permanent)
           .RuleFor(petOwnerMock => petOwnerMock.Contact, ContactMock.GetDefaultInstance())
           .RuleFor(petOwnerMock => petOwnerMock.Document, DocumentMock.GetDefaultInstance())
           .Generate();
    }

    public static PetOwner GetNullInstance()
    {
        return new Faker<PetOwner>(Constants.Language)
           .RuleFor(petOwnerMock => petOwnerMock.Document, DocumentMock.GetNullInstance())
           .Generate();
    }
}