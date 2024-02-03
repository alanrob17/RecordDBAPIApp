using DAL.Models;

namespace DAL.Repository
{
    public interface IStatisticRepository
    {
        Task<Statistic> GetStatistics();
    }
}