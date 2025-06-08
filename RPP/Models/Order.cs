using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPP.Models;

public class Order
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string WorkerId { get; set; }
    public string? BuyerId { get; set; }
    public DateTime OrderDate { get; set; }
    public double Sum { get; set; }
    public bool IsCancel { get; set; }
    public Worker? Worker { get; set; }
    public Buyer? Buyer { get; set; }

    [ForeignKey("OrderId")]
    public List<OrderProduct>? OrderProducts { get; set; }
}
