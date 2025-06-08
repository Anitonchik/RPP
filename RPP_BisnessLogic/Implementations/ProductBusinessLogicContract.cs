using RPP;
using RPP.BuisnessLogicContracts;
using RPP.DataModels;
using RPP.StoragesContracts;
using System.ComponentModel.DataAnnotations;

namespace RPP_BuisnessLogic.Implementations;

internal class ProductBusinessLogicContract(IProductStorageContract productStorageContract) : IProductBusinessLogicContract
{
    private readonly IProductStorageContract _productStorageContract = productStorageContract; 

    public void DeleteProduct(string id)
    {
        if (id.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!id.IsGuid())
        {
            throw new ValidationException();
        }
        _productStorageContract.DelElement(id);
    }

    public List<ProductDataModel> GetAllProducts(bool onlyActive = true)
    {
        return _productStorageContract.GetList(onlyActive) ?? throw new Exception();
    } 
    public List<ProductHistoryDataModel> GetProductHistoryByProduct(string productId)
    {
        if (productId.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!productId.IsGuid())
        {
            throw new ValidationException();
        }
        return _productStorageContract.GetHistoryByProductId(productId) ?? throw new Exception();
    }

    public ProductDataModel GetProductByData(string data)
    {
        if (data.IsEmpty())
        {
            throw new ValidationException();
        }
        if (data.IsGuid())
        {
            return _productStorageContract.GetElementById(data) ?? throw new Exception();
        }
        return _productStorageContract.GetElementByName(data) ?? throw new Exception();
    }

    public void InsertProduct(ProductDataModel productDataModel)
    {
        ArgumentNullException.ThrowIfNull(productDataModel);
        productDataModel.Validate();
        _productStorageContract.AddElement(productDataModel);
    }

    public void UpdateProduct(ProductDataModel productDataModel)
    {
        ArgumentNullException.ThrowIfNull(productDataModel);
        productDataModel.Validate();
        _productStorageContract.UpdElement(productDataModel);
    }
}
