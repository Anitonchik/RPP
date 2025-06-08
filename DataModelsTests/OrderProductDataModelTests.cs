using System.ComponentModel.DataAnnotations;
using RPP.DataModels;

namespace RPP_Tests.DataModelsTests;

[TestFixture]
internal class OrderProductDataModelTests
{
    [Test]
    public void OrderIdIsNullOrEmptyTest()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), null, 1);
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
        order = CreateDataModel( Guid.NewGuid().ToString(), string.Empty, 1);
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void OrderIdIsNotGuidTest()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), "id", 1);
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void ProductIdIsNullOrEmptyTest()
    {
        var product = CreateDataModel(null, Guid.NewGuid().ToString(), 1);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
        product = CreateDataModel(string.Empty, Guid.NewGuid().ToString(), 1);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void ProductIdIsNotGuidTest()
    {
        var product = CreateDataModel("id", Guid.NewGuid().ToString(), 1);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void CountNotCorrectTest()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), -10);
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void AllFieldsIsCorrectTest()
    {
        var orderId = Guid.NewGuid().ToString();
        var productId = Guid.NewGuid().ToString();
        var count = 10;
        var orderProduct = CreateDataModel(orderId, productId, count);
        Assert.That(() => orderProduct.Validate(), Throws.Nothing);
        Assert.Multiple(() =>
        {
            Assert.That(orderProduct.OrderId, Is.EqualTo(orderId));
            Assert.That(orderProduct.ProductId, Is.EqualTo(productId));
            Assert.That(orderProduct.Count, Is.EqualTo(count));
        });
    }

    private static OrderProductDataModel CreateDataModel(string? orderId, string? productId, int count) =>
        new(orderId, productId, count);
}
