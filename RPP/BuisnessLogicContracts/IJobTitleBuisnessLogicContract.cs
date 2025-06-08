using RPP.DataModels;

namespace RPP.BuisnessLogicContracts;

public interface IJobTitleBuisnessLogicContract
{
    List<JobTitleDataModel> GetAllJobTitles(bool onlyActive);
    List<JobTitleDataModel> GetAllDataOfJobTitle(string JobTitleId);
    JobTitleDataModel GetJobTitleByData(string data);
    void InsertJobTitle(JobTitleDataModel jobTitleDataModel);
    void UpdateJobTitle(JobTitleDataModel jobTitleDataModel);
    void DeleteJobTitle(string id);
    void RestoreJobTitle(string id);
}
