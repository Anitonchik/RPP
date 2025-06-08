using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPP.Models;

public class OrderProduct
{
    public required string OrderId { get; set; }
    public required string ProductId { get; set; }
    public int Count { get; set; }
    public double Price { get; set; }
    public Order? Order { get; set; }
    public Product? Product { get; set; }
}
