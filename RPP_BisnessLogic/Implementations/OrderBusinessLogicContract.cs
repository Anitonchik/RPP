using RPP;
using RPP.BuisnessLogicContracts;
using RPP.DataModels;
using RPP.Extensions;
using RPP.StoragesContracts;
using System.ComponentModel.DataAnnotations;

namespace RPP_BuisnessLogic.Implementations;

internal class OrderBuisnessLogicContract(IOrderStorageContract orderStorageContract) : IOrderBusinessLogicContract
{
    private readonly IOrderStorageContract _orderStorageContract = orderStorageContract;

    public void CancelOrder(string id)
    {
        if (id.IsEmpty())
        {
            throw new ValidationException(nameof(id));
        }
        if (!id.IsGuid())
        {
            throw new ValidationException();
        }
        _orderStorageContract.DelElement(id);
    }

    public List<OrderDataModel> GetAllOrdersByBuyerByPeriod(string buyerId, DateTime fromDate, DateTime toDate)
    {
        if (fromDate.IsDateNotOlder(toDate))
        {
            throw new Exception();
        }
        if (buyerId.IsEmpty())
        {
            throw new ValidationException(nameof(buyerId));
        }
        if (!buyerId.IsGuid())
        {
            throw new ValidationException();
        }
        return _orderStorageContract.GetList(fromDate, toDate, buyerId: buyerId) ?? throw new Exception();
    }

    public List<OrderDataModel> GetAllOrdersByPeriod(DateTime fromDate, DateTime toDate)
    {
        if (fromDate.IsDateNotOlder(toDate))
        {
            throw new Exception();
        }
        return _orderStorageContract.GetList(fromDate, toDate) ?? throw new Exception();
    }

    public List<OrderDataModel> GetAllOrdersByProductByPeriod(string productId, DateTime fromDate, DateTime toDate)
    {
        if (fromDate.IsDateNotOlder(toDate))
        {
            throw new Exception();
        }
        if (productId.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!productId.IsGuid())
        {
            throw new ValidationException();
        }
        return _orderStorageContract.GetList(fromDate, toDate, productId: productId) ?? throw new Exception();
    }

    public List<OrderDataModel> GetAllOrdersByWorkerByPeriod(string workerId, DateTime fromDate, DateTime toDate)
    {
        if (fromDate.IsDateNotOlder(toDate))
        {
            throw new Exception();
        }
        if (workerId.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!workerId.IsGuid())
        {
            throw new ValidationException();
        }
        return _orderStorageContract.GetList(fromDate, toDate, workerId: workerId) ?? throw new Exception();
    }

    public OrderDataModel GetOrderByData(string data)
    {
        if (data.IsEmpty())
        {
            throw new ValidationException(nameof(data));
        }
        if (!data.IsGuid())
        {
            throw new ValidationException();
        }
        return _orderStorageContract.GetElementById(data) ?? throw new Exception();
    }

    public void InsertOrder(OrderDataModel orderDataModel)
    {
        ArgumentNullException.ThrowIfNull(orderDataModel);
        orderDataModel.Validate();
        _orderStorageContract.AddElement(orderDataModel);
    }
}
