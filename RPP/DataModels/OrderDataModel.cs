using System.ComponentModel.DataAnnotations;

namespace RPP.DataModels;

public class OrderDataModel (string id, string buyerId, string workerId, double price, DateTime dateStart, DateTime dateEnd, bool isCancel, List<OrderProductDataModel> products) : IValidation
{
    public string Id { get; private set; } = id;
    public string BuyerId { get; private set; } = buyerId;
    public string WorkerId { get; private set; } = workerId;
    public double Price { get; private set; } = price;
    public DateTime DateStart { get; private set; } = dateStart;
    public DateTime DateEnd { get; private set; } = dateEnd;
    public bool IsCancel { get; private set; } = isCancel;
    public List<OrderProductDataModel> Products { get; private set; } = products;

    public void Validate()
    {
        if (Id.IsEmpty())
            throw new ValidationException("Field Id is empty");
        if (!Id.IsGuid())
            throw new ValidationException("The value in the field Id is not a unique identifier");
        if (BuyerId.IsEmpty())
            throw new ValidationException("Field BuyerId is empty");
        if (!BuyerId.IsGuid())
            throw new ValidationException("The value in the field BuyerId is not a unique identifier");
        if (WorkerId.IsEmpty())
            throw new ValidationException("Field WorkerId is empty");
        if (!WorkerId.IsGuid())
            throw new ValidationException("The value in the field WorkerId is not a unique identifier");
        if (DateEnd < DateStart)
            throw new ValidationException("The end date of the order cannot be less than the start date");
        if ((Products?.Count ?? 0) == 0)
            throw new ValidationException("The sale must include products");
    }
}
