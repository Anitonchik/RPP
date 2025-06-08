using RPP.Enums;

namespace RPP.Models;

public class JobTitle
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public required string JobTitleId { get; set; }
    public required string JobTitleName { get; set; }
    public JobTitleType JobTitleType { get; set; }
    public double Salary { get; set; }
    public bool IsActual { get; set; }
    public DateTime ChangeDate { get; set; }
}
