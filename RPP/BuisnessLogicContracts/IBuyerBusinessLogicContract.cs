using RPP.DataModels;

namespace RPP.BuisnessLogicContracts;

internal interface IBuyerBuisnessLogicContract
{
    List<BuyerDataModel> GetAllBuyers();
    void JsonSerializeAsync(BuyerDataModel buyerDataModel);
    BuyerDataModel GetBuyerByData(string data);
    void InsertBuyer(BuyerDataModel buyerDataModel);
    void UpdateBuyer(BuyerDataModel buyerDataModel);
    void DeleteBuyer(string id);
}
