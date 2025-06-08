using RPP.Enums;
using System.ComponentModel.DataAnnotations;

namespace RPP.DataModels;

public class ProductDataModel (string id, string productName, ProductType productType, double price, bool isDeleted) : IValidation
{
    public string Id { get; private set; } = id;
    public string ProductName { get; private set; } = productName;
    public ProductType ProductType { get; private set; } = productType;
    public double Price { get; private set; } = price;
    public bool IsDeleted { get; private set; } = isDeleted;
    public void Validate()
    {
        if (Id.IsEmpty())
            throw new ValidationException("Field Id is empty");
        if (!Id.IsGuid())
            throw new ValidationException("The value in the field Id is not a unique identifier");
        if (ProductName.IsEmpty())
            throw new ValidationException("Field ProductName is empty");
        if (ProductType == ProductType.None)
            throw new ValidationException("Field ProductType is empty");
        if (Price <= 0)
            throw new ValidationException("Field Price is less than or equal to 0");
    }
}
