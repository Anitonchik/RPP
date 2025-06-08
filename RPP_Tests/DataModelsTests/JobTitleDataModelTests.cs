using RPP.DataModels;
using RPP.Enums;
using System.ComponentModel.DataAnnotations;

namespace RPP_Tests.DataModelsTests;

[TestFixture]
public class JobTitleDateModelTests
{
    [Test]
    public void IdIsNullOrEmptyTest()
    {
        var jobTitle = CreateDataModel(null, "name", JobTitleType.Delivaryman, 10, true, DateTime.UtcNow);
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
        jobTitle = CreateDataModel(string.Empty, "name", JobTitleType.Delivaryman, 10, true, DateTime.UtcNow);
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
    }
    [Test]
    public void IdIsNotGuidTest()
    {
        var jobTitle = CreateDataModel("id", "name", JobTitleType.Delivaryman, 10, true, DateTime.UtcNow);
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void JobTitleNameIsEmptyTest()
    {
        var manufacturer = CreateDataModel(Guid.NewGuid().ToString(), null, JobTitleType.Delivaryman, 10, true, DateTime.UtcNow);
        Assert.That(() => manufacturer.Validate(), Throws.TypeOf<ValidationException>());
        manufacturer = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, JobTitleType.Delivaryman, 10, true, DateTime.UtcNow);
        Assert.That(() => manufacturer.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void JobTitleTypeIsNoneTest()
    {
        var jobTitle = CreateDataModel(Guid.NewGuid().ToString(), "name", JobTitleType.None, 10, true, DateTime.UtcNow);
        Assert.That(() => jobTitle.Validate(),
        Throws.TypeOf<ValidationException>());
    }
    [Test]
    public void SalaryIsLessOrZeroTest()
    {
        var jobTitle = CreateDataModel(Guid.NewGuid().ToString(), "name", JobTitleType.Delivaryman, 0, true, DateTime.UtcNow);
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
        jobTitle = CreateDataModel(Guid.NewGuid().ToString(), "name", JobTitleType.Delivaryman, -10, true, DateTime.UtcNow);
        Assert.That(() => jobTitle.Validate(), Throws.TypeOf<ValidationException>());
    }
    [Test]
    public void AllFieldsIsCorrectTest()
    {
        var jobTitleId = Guid.NewGuid().ToString();
        var jobTitlejobTitleId = Guid.NewGuid().ToString();
        var jobTitleName = "name";
        var jobTitleType = JobTitleType.Delivaryman;
        var salary = 10;
        var isActual = false;
        var changeDate = DateTime.UtcNow.AddDays(-1);
        var jobTitle = CreateDataModel(jobTitleId, jobTitleName, jobTitleType, salary, isActual, changeDate);
        Assert.That(() => jobTitle.Validate(), Throws.Nothing);
        Assert.Multiple(() =>
        {
            Assert.That(jobTitle.Id, Is.EqualTo(jobTitleId));
            Assert.That(jobTitle.JobTitleName, Is.EqualTo(jobTitleName));
            Assert.That(jobTitle.JobTitleType, Is.EqualTo(jobTitleType));
            Assert.That(jobTitle.Salary, Is.EqualTo(salary));
            Assert.That(jobTitle.IsActual, Is.EqualTo(isActual));
            Assert.That(jobTitle.ChangeDate, Is.EqualTo(changeDate));
        });
    }
    private static JobTitleDataModel CreateDataModel(string? id, string? JobTitleName, JobTitleType JobTitleType, double salary, bool isActual, DateTime
        changeDate) => new(id, JobTitleName, JobTitleType, salary, isActual, changeDate);

}
