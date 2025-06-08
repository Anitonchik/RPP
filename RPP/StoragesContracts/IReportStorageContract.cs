using RPP.Report;

namespace RPP.StoragesContracts;

public interface IReportStorageContract
{
    public List<ReportView> GetCirclesWithInterestsWithMedals(DateTime fromDate, DateTime toDate);
}
