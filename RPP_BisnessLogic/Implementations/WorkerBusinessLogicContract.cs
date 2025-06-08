using RPP;
using RPP.BuisnessLogicContracts;
using RPP.DataModels;
using RPP.Extensions;
using RPP.StoragesContracts;
using System.ComponentModel.DataAnnotations;

namespace RPP_BuisnessLogic.Implementations;

internal class WorkerBusinessLogicContract(IWorkerStorageContract workerStorageContract) : IWorkerBusinessLogicContract
{
    private readonly IWorkerStorageContract _workerStorageContract = workerStorageContract;

    public void DeleteWorker(string id)
    {
        if (id.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!id.IsGuid())
        {
            throw new ValidationException();
        }
        _workerStorageContract.DelElement(id);
    }

    public List<WorkerDataModel> GetAllWorkers(bool onlyActive = true)
    {
        return _workerStorageContract.GetList(onlyActive) ?? throw new Exception();
    }

    public List<WorkerDataModel> GetAllWorkersByBirthDate(DateTime fromDate, DateTime toDate, bool onlyActive = true)
    {
        if (fromDate.IsDateNotOlder(toDate))
        {
            throw new Exception();
        }
        return _workerStorageContract.GetList(onlyActive, fromBirthDate: fromDate, toBirthDate: toDate) ?? throw new Exception();
    }

    public List<WorkerDataModel> GetAllWorkersByEmploymentDate(DateTime fromDate, DateTime toDate, bool onlyActive = true)
    {
        if (fromDate.IsDateNotOlder(toDate))
        {
            throw new Exception();
        }
        return _workerStorageContract.GetList(onlyActive, fromEmploymentDate: fromDate, toEmploymentDate: toDate) ?? throw new Exception();
    }

    public List<WorkerDataModel> GetAllWorkersByJobTitle(string JobTitleId, bool onlyActive = true)
    {
        if (JobTitleId.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!JobTitleId.IsGuid())
        {
            throw new ValidationException();
        }
        return _workerStorageContract.GetList(onlyActive, JobTitleId) ?? throw new Exception();
    }

    public WorkerDataModel GetWorkerByData(string data)
    {
        if (data.IsEmpty())
        {
            throw new ValidationException();
        }
        if (data.IsGuid())
        {
            return _workerStorageContract.GetElementById(data) ?? throw new Exception();
        }
        return _workerStorageContract.GetElementByFIO(data) ?? throw new Exception();
    }

    public void InsertWorker(WorkerDataModel workerDataModel)
    {
        ArgumentNullException.ThrowIfNull(workerDataModel);
        workerDataModel.Validate();
        _workerStorageContract.AddElement(workerDataModel);
    }

    public void UpdateWorker(WorkerDataModel workerDataModel)
    {
        ArgumentNullException.ThrowIfNull(workerDataModel);
        workerDataModel.Validate();
        _workerStorageContract.UpdElement(workerDataModel);
    }
}
