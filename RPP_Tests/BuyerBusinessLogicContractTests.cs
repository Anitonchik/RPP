using Moq;
using RPP.BuisnessLogicContracts;
using RPP.DataModels;
using RPP.StoragesContracts;
using RPP_BuisnessLogic.Implementations;
using System.ComponentModel.DataAnnotations;

namespace RPP_Tests;

[TestFixture]
internal class BuyerBuisnessLogicContractTests
{
    private IBuyerBuisnessLogicContract _buyerBuisnessLogicContract;
    private Mock<IBuyerStorageContract> _buyerStorageContract;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _buyerStorageContract = new Mock<IBuyerStorageContract>();
        _buyerBuisnessLogicContract = new BuyerBuisnessLogicContract(_buyerStorageContract.Object);
    }

    [TearDown]
    public void TearDown()
    {
        _buyerStorageContract.Reset();
    }

    [Test]
    public void GetAllBuyers_ReturnListOfRecords_Test()
    {
        //Arrange
        var listOriginal = new List<BuyerDataModel>()
        {
            new(Guid.NewGuid().ToString(), "fio 1", "+7-111-111-11-11"),
            new(Guid.NewGuid().ToString(), "fio 2", "+7-555-444-33-23"),
            new(Guid.NewGuid().ToString(), "fio 3", "+7-777-777-7777"),
        };
        _buyerStorageContract.Setup(x => x.GetList()).Returns(listOriginal);
        //Act
        var list = _buyerBuisnessLogicContract.GetAllBuyers();
        //Assert
        Assert.That(list, Is.Not.Null);
        Assert.That(list, Is.EquivalentTo(listOriginal));
    }

    [Test]
    public void GetAllBuyers_ReturnEmptyList_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.GetList()).Returns([]);
        //Act
        var list = _buyerBuisnessLogicContract.GetAllBuyers();
        //Assert
        Assert.That(list, Is.Not.Null);
        Assert.That(list, Has.Count.EqualTo(0));
        _buyerStorageContract.Verify(x => x.GetList(), Times.Once);
    }

    [Test]
    public void GetAllBuyers_ReturnNull_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.GetAllBuyers(), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.GetList(), Times.Once);
    }

    [Test]
    public void GetAllBuyers_StorageThrowError_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.GetList()).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.GetAllBuyers(), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.GetList(), Times.Once);
    }
    [Test]
    public void GetBuyerByData_GetById_ReturnRecord_Test()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var record = new BuyerDataModel(id, "fio", "+7-111-111-11-11");
        _buyerStorageContract.Setup(x => x.GetElementById(id)).Returns(record);
        //Act
        var element = _buyerBuisnessLogicContract.GetBuyerByData(id);
        //Assert
        Assert.That(element, Is.Not.Null);
        Assert.That(element.Id, Is.EqualTo(id));
        _buyerStorageContract.Verify(x => x.GetElementById(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void GetBuyerByData_GetByFio_ReturnRecord_Test()
    {
        //Arrange
        var fio = "fio";
        var record = new BuyerDataModel(Guid.NewGuid().ToString(), fio, "+7- 111 - 111 - 11 - 11");
        _buyerStorageContract.Setup(x => x.GetElementByFIO(fio)).Returns(record);
        //Act
        var element = _buyerBuisnessLogicContract.GetBuyerByData(fio);
        //Assert
        Assert.That(element, Is.Not.Null);
        Assert.That(element.FIO, Is.EqualTo(fio));
        _buyerStorageContract.Verify(x => x.GetElementByFIO(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void GetBuyerByData_GetByPhoneNumber_ReturnRecord_Test()
    {
        //Arrange
        var phoneNumber = "+7-111-111-11-11";
        var record = new BuyerDataModel(Guid.NewGuid().ToString(), "fio", phoneNumber);
        _buyerStorageContract.Setup(x => x.GetElementByPhoneNumber(phoneNumber)).Returns(record);
        //Act
        var element = _buyerBuisnessLogicContract.GetBuyerByData(phoneNumber);
        //Assert
        Assert.That(element, Is.Not.Null);
        Assert.That(element.PhoneNumber, Is.EqualTo(phoneNumber));
        _buyerStorageContract.Verify(x =>
        x.GetElementByPhoneNumber(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void GetBuyerByData_EmptyData_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData(null), Throws.TypeOf<ValidationException>());
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData(string.Empty), Throws.TypeOf<ValidationException>());
        _buyerStorageContract.Verify(x => x.GetElementById(It.IsAny<string>()), Times.Never);
        _buyerStorageContract.Verify(x => x.GetElementByPhoneNumber(It.IsAny<string>()), Times.Never);
        _buyerStorageContract.Verify(x => x.GetElementByFIO(It.IsAny<string>()), Times.Never);
    }
    [Test]
    public void GetBuyerByData_GetById_NotFoundRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData(Guid.NewGuid().ToString()), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.GetElementById(It.IsAny<string>()), Times.Once);
        _buyerStorageContract.Verify(x => x.GetElementByPhoneNumber(It.IsAny<string>()), Times.Never);
        _buyerStorageContract.Verify(x => x.GetElementByFIO(It.IsAny<string>()), Times.Never);
    }

    [Test]
    public void GetBuyerByData_GetByFio_NotFoundRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData("fio"), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.GetElementById(It.IsAny<string>()), Times.Never);
        _buyerStorageContract.Verify(x => x.GetElementByFIO(It.IsAny<string>()), Times.Once);
        _buyerStorageContract.Verify(x => x.GetElementByPhoneNumber(It.IsAny<string>()), Times.Never);
    }
    [Test]
    public void
    GetBuyerByData_GetByPhoneNumber_NotFoundRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData("+7-111-111-11-12"), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.GetElementById(It.IsAny<string>()), Times.Never);
        _buyerStorageContract.Verify(x => x.GetElementByFIO(It.IsAny<string>()), Times.Never);
        _buyerStorageContract.Verify(x => x.GetElementByPhoneNumber(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void GetBuyerByData_StorageThrowError_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.GetElementById(It.IsAny<string>())).Throws(new Exception());
        _buyerStorageContract.Setup(x => x.GetElementByFIO(It.IsAny<string>())).Throws(new Exception());
        _buyerStorageContract.Setup(x => x.GetElementByPhoneNumber(It.IsAny<string>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() =>  _buyerBuisnessLogicContract.GetBuyerByData(Guid.NewGuid().ToString()), Throws.TypeOf<Exception>());
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData("fio"), Throws.TypeOf<Exception>());
        Assert.That(() => _buyerBuisnessLogicContract.GetBuyerByData("+7-111-111-11-12"), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.GetElementById(It.IsAny<string>()), Times.Once);
        _buyerStorageContract.Verify(x => x.GetElementByFIO(It.IsAny<string>()), Times.Once);
        _buyerStorageContract.Verify(x => x.GetElementByPhoneNumber(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void InsertBuyer_CorrectRecord_Test()
    {
        //Arrange
        var flag = false;
        var record = new BuyerDataModel(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11");
        _buyerStorageContract.Setup(x => x.AddElement(It.IsAny<BuyerDataModel>()))
        .Callback((BuyerDataModel x) =>
        {
            flag = x.Id == record.Id && x.FIO == record.FIO && x.PhoneNumber == record.PhoneNumber;
        });
        //Act
        _buyerBuisnessLogicContract.InsertBuyer(record);
        //Assert
        _buyerStorageContract.Verify(x => x.AddElement(It.IsAny<BuyerDataModel>()), Times.Once);
        Assert.That(flag);
    }
    [Test]
    public void InsertBuyer_RecordWithExistsData_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.AddElement(It.IsAny<BuyerDataModel>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.InsertBuyer(new(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11")),  Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.AddElement(It.IsAny<BuyerDataModel>()), Times.Once);
    }
    [Test]
    public void InsertBuyer_NullRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.InsertBuyer(null),
        Throws.TypeOf<ArgumentNullException>());
        _buyerStorageContract.Verify(x => x.AddElement(It.IsAny<BuyerDataModel>()), Times.Never);
    }
    [Test]
    public void InsertBuyer_InvalidRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.InsertBuyer(new BuyerDataModel("id", "fio", "+7-111-111-11-11")), Throws.TypeOf<ValidationException>());
        _buyerStorageContract.Verify(x => x.AddElement(It.IsAny<BuyerDataModel>()), Times.Never);
    }

    [Test]
    public void InsertBuyer_StorageThrowError_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.AddElement(It.IsAny<BuyerDataModel>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.InsertBuyer(new(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11")), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.AddElement(It.IsAny<BuyerDataModel>()), Times.Once);
    }
    [Test]
    public void UpdateBuyer_CorrectRecord_Test()
    {
        //Arrange
        var flag = false;
        var record = new BuyerDataModel(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11");
        _buyerStorageContract.Setup(x => x.UpdElement(It.IsAny<BuyerDataModel>()))
        .Callback((BuyerDataModel x) =>
        {
            flag = x.Id == record.Id && x.FIO == record.FIO && x.PhoneNumber == record.PhoneNumber;
        });
        //Act
        _buyerBuisnessLogicContract.UpdateBuyer(record);
        //Assert
        _buyerStorageContract.Verify(x =>
        x.UpdElement(It.IsAny<BuyerDataModel>()), Times.Once);
        Assert.That(flag);
    }
    [Test]
    public void UpdateBuyer_RecordWithIncorrectData_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.UpdElement(It.IsAny<BuyerDataModel>())).
            Throws(new Exception(""));
        //Act&Assert
        Assert.That(() =>
        _buyerBuisnessLogicContract.UpdateBuyer(new(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11")),Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.UpdElement(It.IsAny<BuyerDataModel>()), Times.Once);
    }

    [Test]
    public void UpdateBuyer_RecordWithExistsData_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.UpdElement(It.IsAny<BuyerDataModel>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.UpdateBuyer(new(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11")), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.UpdElement(It.IsAny<BuyerDataModel>()), Times.Once);
    }
    [Test]
    public void UpdateBuyer_NullRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.UpdateBuyer(null), Throws.TypeOf<ArgumentNullException>());
        _buyerStorageContract.Verify(x => x.UpdElement(It.IsAny<BuyerDataModel>()), Times.Never);
    }
    [Test]
    public void UpdateBuyer_InvalidRecord_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.UpdateBuyer(new BuyerDataModel("id", "fio", "+7-111-111-11-11")),
        Throws.TypeOf<ValidationException>());
        _buyerStorageContract.Verify(x => x.UpdElement(It.IsAny<BuyerDataModel>()), Times.Never);
    }
    [Test]
    public void UpdateBuyer_StorageThrowError_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.UpdElement(It.IsAny<BuyerDataModel>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.UpdateBuyer(new(Guid.NewGuid().ToString(), "fio", "+7-111-111-11-11")), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.UpdElement(It.IsAny<BuyerDataModel>()), Times.Once);
    }
    [Test]
    public void DeleteBuyer_CorrectRecord_Test()
    {
        //Arrange
        var id = Guid.NewGuid().ToString();
        var flag = false;
        _buyerStorageContract.Setup(x => x.DelElement(It.Is((string x) => x == id))).Callback(() => { flag = true; });
        //Act
        _buyerBuisnessLogicContract.DeleteBuyer(id);
        //Assert
        _buyerStorageContract.Verify(x => x.DelElement(It.IsAny<string>()), Times.Once);
        Assert.That(flag);
    }
    [Test]
    public void DeleteBuyer_RecordWithIncorrectId_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.DelElement(It.IsAny<string>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.DeleteBuyer(Guid.NewGuid().ToString()), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.DelElement(It.IsAny<string>()), Times.Once);
    }
    [Test]
    public void DeleteBuyer_IdIsNullOrEmpty_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.DeleteBuyer(null), Throws.TypeOf<ValidationException>());
        Assert.That(() => _buyerBuisnessLogicContract.DeleteBuyer(string.Empty), Throws.TypeOf<ValidationException>());
        _buyerStorageContract.Verify(x => x.DelElement(It.IsAny<string>()), Times.Never);
    }
    [Test]
    public void DeleteBuyer_IdIsNotGuid_ThrowException_Test()
    {
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.DeleteBuyer("id"), Throws.TypeOf<ValidationException>());
        _buyerStorageContract.Verify(x => x.DelElement(It.IsAny<string>()), Times.Never);
    }
    [Test]
    public void DeleteBuyer_StorageThrowError_ThrowException_Test()
    {
        //Arrange
        _buyerStorageContract.Setup(x => x.DelElement(It.IsAny<string>())).Throws(new Exception());
        //Act&Assert
        Assert.That(() => _buyerBuisnessLogicContract.DeleteBuyer(Guid.NewGuid().ToString()), Throws.TypeOf<Exception>());
        _buyerStorageContract.Verify(x => x.DelElement(It.IsAny<string>()), Times.Once);
    }

}
