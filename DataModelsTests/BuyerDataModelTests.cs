using RPP.DataModels;
using System.ComponentModel.DataAnnotations;

namespace RPP_Tests.DataModelsTests;

[TestFixture]
public class BuyerDataModelTests
{
    [Test]
    public void IdIsNullOrEmptyTest()
    {
        var buyer = CreateDataModel(null, "fio", "number");
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
        buyer = CreateDataModel(string.Empty, "fio", "number");
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void IdIsNotGuidTest()
    {
        var buyer = CreateDataModel("id", "fio", "number");
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void FIOIsNullOrEmptyTest()
    {
        var buyer = CreateDataModel(Guid.NewGuid().ToString(), null, "number");
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
        buyer = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, "number");
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void PhoneNumberIsNullOrEmptyTest()
    {
        var buyer = CreateDataModel(Guid.NewGuid().ToString(), "fio", null);
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
        buyer = CreateDataModel(Guid.NewGuid().ToString(), "fio", string.Empty);
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void PhoneNumberIsIncorrectTest()
    {
        var buyer = CreateDataModel(Guid.NewGuid().ToString(), "fio", "777");
        Assert.That(() => buyer.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void AllFieldsIsCorrectTest()
    {
        var buyerId = Guid.NewGuid().ToString();
        var fio = "Fio";
        var phoneNumber = "+7-777-777-77-77";
        var buyer = CreateDataModel(buyerId, fio, phoneNumber);
        Assert.That(() => buyer.Validate(), Throws.Nothing);
        Assert.Multiple(() =>
        {
            Assert.That(buyer.Id, Is.EqualTo(buyerId));
            Assert.That(buyer.FIO, Is.EqualTo(fio));
            Assert.That(buyer.PhoneNumber, Is.EqualTo(phoneNumber));
        });
    }
    private static BuyerDataModel CreateDataModel(string? id, string? fio, string? phoneNumber) => new(id, fio, phoneNumber);

}
