using System.ComponentModel.DataAnnotations;

namespace RPP.DataModels;

public class OrderProductDataModel (string orderId, string productId, int count) : IValidation
{
    public string OrderId { get; private set; } = orderId;
    public string ProductId { get; private set; } = productId;
    public int Count { get; private set; } = count;

    public void Validate()
    {
        if (OrderId.IsEmpty())
            throw new ValidationException("Field OrderId is empty");
        if (!OrderId.IsGuid())
            throw new ValidationException("The value in the field OrderId is not a unique identifier");
        if (ProductId.IsEmpty())
            throw new ValidationException("Field ProductId is empty");
        if (!ProductId.IsGuid())
            throw new ValidationException("The value in the field Id is not a unique identifier");
        if (Count <= 0)
            throw new ValidationException("Field Count is less than or equal to 0");
    }
}
