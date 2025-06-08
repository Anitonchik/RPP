using RPP.Report;

namespace RPP.BuisnessLogicContracts;

public interface IReportBuisnessLogicContract
{
    public List<ReportView> GetCirclesWithInterestsWithMedals(DateTime fromDate, DateTime toDate);
}
