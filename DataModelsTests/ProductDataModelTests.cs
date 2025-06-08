using System.ComponentModel.DataAnnotations;
using RPP.DataModels;
using RPP.Enums;

namespace RPP_Tests.DataModelsTests;

[TestFixture]
internal class ProductDataModelTests
{
    [Test]
    public void IdIsNullOrEmptyTest()
    {
        var product = CreateDataModel(null, "name", ProductType.Cloth, 10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
        product = CreateDataModel(string.Empty, "name", ProductType.Cloth, 10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void IdIsNotGuidTest()
    {
        var product = CreateDataModel("id", "name", ProductType.Cloth, 10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void ProductNameIsEmptyTest()
    {
        var product = CreateDataModel(Guid.NewGuid().ToString(), null, ProductType.Cloth, 10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
        product = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, ProductType.Cloth, 10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void ProductTypeIsNoneTest()
    {
        var product = CreateDataModel(Guid.NewGuid().ToString(), null, ProductType.None, 10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void PriceIsLessOrZeroTest()
    {
        var product = CreateDataModel(Guid.NewGuid().ToString(), "name",  ProductType.Cloth, 0, false);
        Assert.That(() => product.Validate(),
        Throws.TypeOf<ValidationException>());
        product = CreateDataModel(Guid.NewGuid().ToString(), "name", ProductType.Cloth, -10, false);
        Assert.That(() => product.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void AllFieldsIsCorrectTest()
    {
        var productId = Guid.NewGuid().ToString();
        var productName = "name";
        var productType = ProductType.Cloth;
        var productPrice = 10;
        var productIsDelete = false;
        var product = CreateDataModel(productId, productName, productType, productPrice, productIsDelete);
        Assert.That(() => product.Validate(), Throws.Nothing);
        Assert.Multiple(() =>
        {
            Assert.That(product.Id, Is.EqualTo(productId));
            Assert.That(product.ProductName, Is.EqualTo(productName));
            Assert.That(product.ProductType, Is.EqualTo(productType));
            Assert.That(product.Price, Is.EqualTo(productPrice));
            Assert.That(product.IsDeleted, Is.EqualTo(productIsDelete));
        });
    }
    private static ProductDataModel CreateDataModel(string? id, string? productName, ProductType productType,  
        double price, bool isDeleted) => new(id, productName, productType, price, isDeleted);
}
