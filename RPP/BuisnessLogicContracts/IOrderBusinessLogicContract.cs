using RPP.DataModels;

namespace RPP.BuisnessLogicContracts;

public interface IOrderBusinessLogicContract
{
    List<OrderDataModel> GetAllOrdersByPeriod(DateTime fromDate, DateTime toDate);
    List<OrderDataModel> GetAllOrdersByWorkerByPeriod(string workerId, DateTime fromDate, DateTime toDate);
    List<OrderDataModel> GetAllOrdersByBuyerByPeriod(string buyerId, DateTime fromDate, DateTime toDate);
    List<OrderDataModel> GetAllOrdersByProductByPeriod(string productId, DateTime fromDate, DateTime toDate);
    OrderDataModel GetOrderByData(string data);
    void InsertOrder(OrderDataModel orderDataModel);
    void CancelOrder(string id);
}
