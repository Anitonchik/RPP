using RPP.DataModels;
using System.ComponentModel.DataAnnotations;

namespace RPP_Tests.DataModelsTests;

[TestFixture]
public class OrderDataModelTests
{
    [Test]
    public void IdIsNullOrEmptyTest()
    {
        var order = CreateDataModel(null, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
        order = CreateDataModel(string.Empty, Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void IdIsNotGuidTest()
    {
        var jobTitle = CreateDataModel("1", Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void BuyerIdIsNullOrEmptyTest()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), null, Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
        order = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void BuyerIdIsNotGuidTest()
    {
        var jobTitle = CreateDataModel(Guid.NewGuid().ToString(), "1", Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void WorkerIdIsNullOrEmptyTest()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), null, 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
        order = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), string.Empty, 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void WorkerIdIsNotGuidTest()
    {
        var jobTitle = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), "1", 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(1), false, CreateSubDataModel());
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void StartDateAndEndDateIsNotCorrect()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 100, DateTime.UtcNow.AddDays(2), DateTime.UtcNow, false, CreateSubDataModel());
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void ProductsIsNullOrEmptyTest()
    {
        var order = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(2), false, null);
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
        order = CreateDataModel(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 100, DateTime.UtcNow, DateTime.UtcNow.AddDays(2), false, []);
        Assert.That(() => order.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void AllFieldsIsCorrectTest()
    {
        var id = Guid.NewGuid().ToString();
        var buyerId = Guid.NewGuid().ToString();
        var workerId = Guid.NewGuid().ToString();
        var price = 10;
        var dateStart = DateTime.UtcNow;
        var dateEnd = DateTime.UtcNow.AddDays(2);
        var isCancel = false;
        var products = CreateSubDataModel();
        var order = CreateDataModel(id, buyerId, workerId, price, dateStart, dateEnd, isCancel, products);
        Assert.That(() => order.Validate(), Throws.Nothing);
        Assert.Multiple(() =>
        {
            Assert.That(order.Id, Is.EqualTo(id));
            Assert.That(order.BuyerId, Is.EqualTo(buyerId));
            Assert.That(order.WorkerId, Is.EqualTo(workerId));
            Assert.That(order.Price, Is.EqualTo(price));
            Assert.That(order.DateStart, Is.EqualTo(dateStart));
            Assert.That(order.DateEnd, Is.EqualTo(dateEnd));
            Assert.That(order.IsCancel, Is.EqualTo(isCancel));
            Assert.That(order.Products, Is.EqualTo(products));
        });
    }

    private static OrderDataModel CreateDataModel(string? id, string? buyerId, string? workerId, int price,
        DateTime dateStart, DateTime dateEnd, bool isCancel, List<OrderProductDataModel> products) => new(id, buyerId, workerId, price, dateStart, dateEnd, isCancel, products);

    private static List<OrderProductDataModel> CreateSubDataModel() => [new(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), 1)];
}
