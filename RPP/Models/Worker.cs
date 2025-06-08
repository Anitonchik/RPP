namespace RPP.Models;

public class Worker
{
    public required string Id { get; set; }
    public required string FIO { get; set; }
    public required string JobTitleId { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime EmploymentDate { get; set; }
    public bool IsDeleted { get; set; }
    public List<Order>? Orders { get; set; }
}
