using Microsoft.EntityFrameworkCore;
using RPP.StoragesContracts;
using System.Collections.ObjectModel;

namespace RPP.Report;

public class ReportStorageContract : IReportStorageContract
{
    private readonly RPPDbContext _dbContext;
    private readonly int maxThreads;
    private readonly SemaphoreSlim semaphoreSlim;

    public ReportStorageContract(RPPDbContext dbContext)
    {
        _dbContext = dbContext;
        maxThreads = 4;
        semaphoreSlim = new SemaphoreSlim(maxThreads);
    }

    public List<ReportView> GetCirclesWithInterestsWithMedals(DateTime fromDate, DateTime toDate)
    {
        semaphoreSlim.Wait();
        try
        {
            var sql = $"SELECT c.\"CircleName\" as \"CircleName\", c.\"Description\" as \"CircleDescription\", " +
                $"i.\"InterestName\" as \"InterestName\", md.\"MedalName\" as \"MedalName\" " +
                $"FROM \"Circles\" c " +
                $"JOIN \"Storekeepers\" st ON st.\"Id\" = c.\"StorekeeperId\" " +
                $"JOIN \"CircleMaterials\" cm ON c.\"Id\" = cm.\"CircleId\" " +
                $"JOIN \"Materials\" mt ON cm.\"MaterialId\" = mt.\"Id\" " +
                $"JOIN \"Medals\" md ON md.\"MaterialId\" = cm.\"MaterialId\" " +
                $"JOIN \"InterestMaterials\" im ON im.\"MaterialId\" = mt.\"Id\" " +
                $"JOIN \"Interests\" i ON i.\"Id\" = im.\"InterestId\" " +
                $"JOIN \"LessonInterests\" li ON li.\"InterestId\" = i.\"Id\" " +
                $"JOIN \"Lessons\" l ON l.\"Id\" = li.\"LessonId\" " +
                $"WHERE(l.\"LessonDate\" between '{fromDate}' and '{toDate}');";
            
            return _dbContext.Set<ReportView>().FromSqlRaw(sql).ToList();
        }
        catch (Exception ex)
        {
            _dbContext.ChangeTracker.Clear();
            throw new Exception();
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }
}
