using RPP.DataModels;

namespace RPP.StoragesContracts;

public interface IWorkerStorageContract
{
    List<WorkerDataModel> GetList(bool onlyActive = true, string? JobTitleId = null,
        DateTime? fromBirthDate = null, DateTime? toBirthDate = null, DateTime? fromEmploymentDate = null,
        DateTime? toEmploymentDate = null);
    WorkerDataModel? GetElementById(string id);
    WorkerDataModel? GetElementByFIO(string fio);
    void AddElement(WorkerDataModel workerDataModel);
    void UpdElement(WorkerDataModel workerDataModel);
    void DelElement(string id);
}
