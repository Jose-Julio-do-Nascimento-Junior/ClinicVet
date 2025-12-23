using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.UniTests.Mock.ValueObjects.v1;

public sealed class ContactMock
{
    public static Contact GetDefaultInstance()
    {
        return new Faker<Contact>(Constants.Language)
           .RuleFor(contactMock => contactMock.Phone, fakerMock => fakerMock.Person.Phone)
           .Generate();
    }

    public static Contact GetNullInstance()
       => new Faker<Contact>(Constants.Language).Generate();
}