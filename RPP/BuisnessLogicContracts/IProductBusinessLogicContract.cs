﻿using RPP.DataModels;

namespace RPP.BuisnessLogicContracts;

public interface IProductBusinessLogicContract
{
    List<ProductDataModel> GetAllProducts(bool onlyActive = true);
    List<ProductHistoryDataModel> GetProductHistoryByProduct(string productId);
    ProductDataModel GetProductByData(string data);
    void InsertProduct(ProductDataModel productDataModel);
    void UpdateProduct(ProductDataModel productDataModel);
    void DeleteProduct(string id);
}
