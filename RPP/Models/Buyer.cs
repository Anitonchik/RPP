namespace RPP.Models;

public class Buyer
{
    public required string Id { get; set; }

    public required string FIO { get; set; }

    public required string PhoneNumber { get; set; }
    public List<Order>? Sales { get; set; }
}
