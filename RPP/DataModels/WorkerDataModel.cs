using System.ComponentModel.DataAnnotations;

namespace RPP.DataModels;

public class WorkerDataModel (string id, string fio, string jobTitleId, DateTime birthDate, DateTime employmentDate, bool isDeleted) : IValidation
{
    public string Id { get; private set; } = id;
    public string FIO { get; private set; } = fio;
    public string JobTitleId { get; private set; } = jobTitleId;
    public DateTime BirthDate { get; private set; } = birthDate;
    public DateTime EmploymentDate { get; private set; } = employmentDate;
    public bool IsDeleted { get; private set; } = isDeleted;
    public void Validate()
    {
        if (Id.IsEmpty())
            throw new ValidationException("Field Id is empty");
        if (!Id.IsGuid())
            throw new ValidationException("The value in the field Id is not a unique identifier");
        if (FIO.IsEmpty())
            throw new ValidationException("Field FIO is empty");
        if (JobTitleId.IsEmpty())
            throw new ValidationException("Field PostId is empty");
        if (!JobTitleId.IsGuid())
            throw new ValidationException("The value in the field PostId is not a unique identifier");
        if (BirthDate.Date > DateTime.Now.AddYears(-16).Date)
            throw new ValidationException($"Minors cannot be hired (BirthDate = {BirthDate.ToShortDateString()})");
        if (EmploymentDate.Date < BirthDate.Date)
            throw new ValidationException("The date of employment cannot be less than the date of birth");
        if ((EmploymentDate - BirthDate).TotalDays / 365 < 16) // EmploymentDate.Year - BirthDate.Year
            throw new ValidationException($@"Minors cannot be hired (EmploymentDate - {EmploymentDate.ToShortDateString()}, 
                                            BirthDate - {BirthDate.ToShortDateString()})");
    }
}
