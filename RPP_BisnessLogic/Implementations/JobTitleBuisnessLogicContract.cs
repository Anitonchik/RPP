using RPP;
using RPP.BuisnessLogicContracts;
using RPP.DataModels;
using RPP.StoragesContracts;
using System.ComponentModel.DataAnnotations;

namespace RPP_BuisnessLogic.Implementations;

internal class JobTitleBuisnessLogicContract(IJobTitleStorageContract jobTitleStorageContract) : IJobTitleBuisnessLogicContract
{
    private readonly IJobTitleStorageContract _jobTitleStorageContract = jobTitleStorageContract;


    public List<JobTitleDataModel> GetAllDataOfJobTitle(string JobTitleId)
    {
        if (JobTitleId.IsEmpty())
        {
            throw new ValidationException(nameof(JobTitleId));
        }
        if (!JobTitleId.IsGuid())
        {
            throw new ValidationException();
        }
        return _jobTitleStorageContract.GetJobTitleWithHistory(JobTitleId) ?? throw new Exception();
    }

    public List<JobTitleDataModel> GetAllJobTitles(bool onlyActive)
    {
        return _jobTitleStorageContract.GetList(onlyActive) ?? throw new Exception();
    }

    public JobTitleDataModel GetJobTitleByData(string data)
    {
        if (data.IsEmpty())
        {
            throw new ValidationException();
        }
        if (data.IsGuid())
        {
            return _jobTitleStorageContract.GetElementById(data) ?? throw new ValidationException();
        }
        return _jobTitleStorageContract.GetElementByName(data) ?? throw new Exception();

    }

    public void InsertJobTitle(JobTitleDataModel jobTitleDataModel)
    {
        ArgumentNullException.ThrowIfNull(jobTitleDataModel);
        jobTitleDataModel.Validate();
        _jobTitleStorageContract.AddElement(jobTitleDataModel);
    }

    public void RestoreJobTitle(string id)
    {
        if (id.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!id.IsGuid())
        {
            throw new ValidationException();
        }
        _jobTitleStorageContract.ResElement(id);
    }

    public void UpdateJobTitle(JobTitleDataModel jobTitleDataModel)
    {
        ArgumentNullException.ThrowIfNull(jobTitleDataModel);
        jobTitleDataModel.Validate();
        _jobTitleStorageContract.UpdElement(jobTitleDataModel);
    }

    public void DeleteJobTitle(string id)
    {
        if (id.IsEmpty())
        {
            throw new ValidationException();
        }
        if (!id.IsGuid())
        {
            throw new ValidationException();
        }
        _jobTitleStorageContract.DelElement(id);
    }
}
