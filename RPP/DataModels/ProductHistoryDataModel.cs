using System.ComponentModel.DataAnnotations;

namespace RPP.DataModels;

public class ProductHistoryDataModel(string productId, double oldPrice) : IValidation
{
    public string ProductId { get; private set; } = productId;
    public double OldPrice { get; private set; } = oldPrice;
    public DateTime ChangeDate { get; private set; } = DateTime.UtcNow;
    public void Validate()
    {
        if (ProductId.IsEmpty())
            throw new ValidationException("Field ProductId is empty");
        if (!ProductId.IsGuid())
            throw new ValidationException("The value in the field ProductId is not a unique identifier");
        if (OldPrice <= 0)
            throw new ValidationException("Field OldPrice is less than or equal to 0");
    }
}
