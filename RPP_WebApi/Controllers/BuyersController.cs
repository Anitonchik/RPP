using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RPP.BuisnessLogicContracts;
using RPP.DataModels;

namespace RPP_WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BuyersController(IBuyerBuisnessLogicContract buyerBuisnessLogicContract) : ControllerBase
{
    private readonly IBuyerBuisnessLogicContract _buyerBuisnessLogicContract = buyerBuisnessLogicContract;

    [HttpGet]
    public List<BuyerDataModel> GetAllBuyers()
    {
        return _buyerBuisnessLogicContract.GetAllBuyers();
    }

    [HttpGet("{data}")]
    public BuyerDataModel GetBuyerByData(string data)
    {
        return _buyerBuisnessLogicContract.GetBuyerByData(data);
    }

    [HttpPost]
    public void InsertBuyer([FromBody] BuyerDataModel buyerDataModel)
    {
        _buyerBuisnessLogicContract.InsertBuyer(buyerDataModel);
    }

    [HttpPut]
    public void UpdateBuyer([FromBody] BuyerDataModel buyerDataModel)
    {
        _buyerBuisnessLogicContract.UpdateBuyer(buyerDataModel);
    }

    [HttpDelete("{id}")]
    public void DeleteBuyer(string id)
    {
        _buyerBuisnessLogicContract.DeleteBuyer(id);
    }

    [HttpPost]
    public void JsonSerialize([FromBody] BuyerDataModel buyerDataModel)
    {
        _buyerBuisnessLogicContract.JsonSerializeAsync(buyerDataModel);
    }
}
