using RPP.DataModels;
using System.ComponentModel.DataAnnotations;

namespace RPP_Tests.DataModelsTests;

public class WorkerDataModelTests
{
    [Test]
    public void IdIsNullOrEmptyTest()
    {
        var worker = CreateDataModel(null, "fio", Guid.NewGuid().ToString(), DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
        worker = CreateDataModel(string.Empty, "fio", Guid.NewGuid().ToString(), DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void IdIsNotGuidTest()
    {
        var worker = CreateDataModel("id", "fio", Guid.NewGuid().ToString(), DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void FIOIsNullOrEmptyTest()
    {
        var worker = CreateDataModel(Guid.NewGuid().ToString(), null, Guid.NewGuid().ToString(), DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
        worker = CreateDataModel(Guid.NewGuid().ToString(), string.Empty, Guid.NewGuid().ToString(), DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void jobTitleIdIsNullOrEmptyTest()
    {
        var worker = CreateDataModel(Guid.NewGuid().ToString(), "fio", null, DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
        worker = CreateDataModel(Guid.NewGuid().ToString(), "fio", string.Empty, DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void jobTitleIdIsNotGuidTest()
    {
        var worker = CreateDataModel(Guid.NewGuid().ToString(), "fio", "jobTitleId", DateTime.Now.AddYears(-18), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void BirthDateIsNotCorrectTest()
    {
        var worker = CreateDataModel(Guid.NewGuid().ToString(), "fio", Guid.NewGuid().ToString(), DateTime.Now.AddYears(-16).AddDays(1), DateTime.Now, false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void BirthDateAndEmploymentDateIsNotCorrectTest()
    {
        var worker = CreateDataModel(Guid.NewGuid().ToString(), "fio", Guid.NewGuid().ToString(),
            DateTime.Now.AddYears(-18), DateTime.Now.AddYears(-18).AddDays(-1), false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
        worker = CreateDataModel(Guid.NewGuid().ToString(), "fio", Guid.NewGuid().ToString(),
            DateTime.Now.AddYears(-18), DateTime.Now.AddYears(-16), false);
        Assert.That(() => worker.Validate(), Throws.TypeOf<ValidationException>());
    }

    [Test]
    public void AllFieldsIsCorrectTest()
    {
        var workerId = Guid.NewGuid().ToString();
        var fio = "fio";
        var jobTitleId = Guid.NewGuid().ToString();
        var birthDate = DateTime.Now.AddYears(-16).AddDays(-1);
        var employmentDate = DateTime.Now;
        var isDelete = false;
        var worker = CreateDataModel(workerId, fio, jobTitleId, birthDate,
        employmentDate, isDelete);
        Assert.That(() => worker.Validate(), Throws.Nothing);
        Assert.Multiple(() =>
        {
            Assert.That(worker.Id, Is.EqualTo(workerId));
            Assert.That(worker.FIO, Is.EqualTo(fio));
            Assert.That(worker.JobTitleId, Is.EqualTo(jobTitleId));
            Assert.That(worker.BirthDate, Is.EqualTo(birthDate));
            Assert.That(worker.EmploymentDate,
            Is.EqualTo(employmentDate));
            Assert.That(worker.IsDeleted, Is.EqualTo(isDelete));
        });
    }
    private static WorkerDataModel CreateDataModel(string? id, string? fio, string? jobTitleId, DateTime birthDate, DateTime employmentDate, bool isDeleted) =>
        new(id, fio, jobTitleId, birthDate, employmentDate, isDeleted);
}
