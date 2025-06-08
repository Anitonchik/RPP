using RPP.Enums;
using System.ComponentModel.DataAnnotations;

namespace RPP.DataModels;

public class JobTitleDataModel(string id, string jobTitleName, JobTitleType jobTitleType, double salary, bool isActual, DateTime changeDate) : IValidation
{
    public string Id { get; private set; } = id;
    public string JobTitleName { get; private set; } = jobTitleName;
    public JobTitleType JobTitleType { get; private set; } = jobTitleType;
    public double Salary { get; private set; } = salary;
    public bool IsActual { get; private set; } = isActual;
    public DateTime ChangeDate { get; private set; } = changeDate;
    public void Validate()
    {
        if (Id.IsEmpty())
            throw new ValidationException("Field Id is empty");
        if (!Id.IsGuid())
            throw new ValidationException("The value in the field Id is not a unique identifier");
        if (JobTitleName.IsEmpty())
            throw new ValidationException("Field PostName is empty");
        if (JobTitleType == JobTitleType.None)
            throw new ValidationException("Field PostType is empty");
        if (Salary <= 0)
            throw new ValidationException("Field Salary is empty");
    }
}
