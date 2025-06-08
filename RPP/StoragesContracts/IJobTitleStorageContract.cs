using RPP.DataModels;

namespace RPP.StoragesContracts;

public interface IJobTitleStorageContract
{
    List<JobTitleDataModel> GetList(bool onlyActual = true);
    List<JobTitleDataModel> GetJobTitleWithHistory(string JobTitleId);
    JobTitleDataModel? GetElementById(string id);
    JobTitleDataModel? GetElementByName(string name);
    void AddElement(JobTitleDataModel jobTitleDataModel);
    void UpdElement(JobTitleDataModel jobTitleDataModel);
    void DelElement(string id);
    void ResElement(string id);
}
