using RPP.BuisnessLogicContracts;
using RPP.DataModels;
using System.Text.RegularExpressions;
using System.Text.Json;
using RPP;
using RPP.StoragesContracts;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.Extensions.Localization;


namespace RPP_BuisnessLogic.Implementations;

internal class BuyerBuisnessLogicContract (IBuyerStorageContract buyerStorageContract, IStringLocalizer<Messages> localizer) : IBuyerBuisnessLogicContract
{
    private readonly IBuyerStorageContract _buyerStorageContract = buyerStorageContract;
    private readonly string _path = "file.json";

    private readonly IStringLocalizer<Messages> _localizer = localizer;

    public List<BuyerDataModel> GetAllBuyers()
    {
        return _buyerStorageContract.GetList() ?? throw new Exception();
    }

    public async void JsonSerializeAsync(BuyerDataModel buyerDataModel)
    {
        FileStream fs = new FileStream("user.json", FileMode.OpenOrCreate);
        await JsonSerializer.SerializeAsync(fs, buyerDataModel);
    }

    public BuyerDataModel GetBuyerByData(string data)
    {
        if (data.IsEmpty())
        {
            throw new ValidationException();
        }
        if (data.IsGuid())
        {
            return _buyerStorageContract.GetElementById(data) ?? throw new Exception();
        }
        if (Regex.IsMatch(data, @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)?[\d\- ]{7,10}$"))
        {
            return _buyerStorageContract.GetElementByPhoneNumber(data) ?? throw new Exception();
        }
        return _buyerStorageContract.GetElementByFIO(data) ?? throw new Exception();
    }

    public void InsertBuyer(BuyerDataModel buyerDataModel)
    {
        ArgumentNullException.ThrowIfNull(buyerDataModel);
        buyerDataModel.Validate(_localizer);
        _buyerStorageContract.AddElement(buyerDataModel);
    }

    public void UpdateBuyer(BuyerDataModel buyerDataModel)
    {
        ArgumentNullException.ThrowIfNull(buyerDataModel);
        buyerDataModel.Validate(_localizer);
        _buyerStorageContract.UpdElement(buyerDataModel);
    }

    public void DeleteBuyer(string id)
    {
        if (id.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!id.IsGuid())
        {
            throw new ValidationException();
        }
        _buyerStorageContract.DelElement(id);
    }
}
