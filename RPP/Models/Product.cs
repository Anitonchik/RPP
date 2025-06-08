using RPP.Enums;

namespace RPP.Models;
public class Product
{
    public required string Id { get; set; }
    public required string ProductName { get; set; }
    public ProductType ProductType { get; set; }
    public double Price { get; set; }
    public bool IsDeleted { get; set; }
    public string? PrevProductName { get; set; }
    public string? PrevPrevProductName { get; set; }
    public List<ProductHistory>? ProductHistories { get; set; }
    public List<OrderProduct>? OrderProducts { get; set; }
}
