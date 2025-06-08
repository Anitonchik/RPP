using RPP.BuisnessLogicContracts;
using RPP.StoragesContracts;
using System.Threading;

namespace RPP.Report;
/*как будто этот класс вообще не нужен*/
public class ReportBuisnessLogicContract : IReportBuisnessLogicContract
{
    private readonly IReportStorageContract _storageContract;

    public List<ReportView> GetCirclesWithInterestsWithMedals(DateTime fromDate, DateTime toDate)
    {
        return _storageContract.GetCirclesWithInterestsWithMedals(fromDate, toDate);
        
    }
}
