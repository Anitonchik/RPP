using RPP.DataModels;

namespace RPP.StoragesContracts;

public interface IOrderStorageContract
{
    List<OrderDataModel> GetList(DateTime? startDate = null, DateTime? endDate = null, 
        string? workerId = null, string? buyerId = null, string? productId = null);
    OrderDataModel? GetElementById(string id);
    void AddElement(OrderDataModel orderDataModel);
    void DelElement(string id);

}
