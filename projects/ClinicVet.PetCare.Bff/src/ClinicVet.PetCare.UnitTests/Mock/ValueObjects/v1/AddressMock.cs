using Bogus;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Domain.ValueObjects.v1;

namespace ClinicVet.PetCare.UnitTests.Mock.ValueObjects.v1;

public sealed class AddressMock
{
    public static Address GetDefaultInstance()
    {
        return new Faker<Address>(Constants.Language)
           .RuleFor(addressMock => addressMock.Street, fakerMock => fakerMock.Address.StreetAddress())
           .RuleFor(addressMock => addressMock.Number, fakerMock => fakerMock.Random.Decimal(decimal.One).ToString())
           .RuleFor(addressMock => addressMock.City, fakerMock => fakerMock.Address.City())
           .RuleFor(addressMock => addressMock.State, fakerMock => fakerMock.Address.State())
           .RuleFor(addressMock => addressMock.ZipCode, fakerMock => fakerMock.Address.ZipCode())
           .Generate();
    }
}