
namespace DAL.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string storedProcedure, P parameters, string connectionId = "default");
        Task SaveData<P>(string query, P parameters, string connectionId = "default");
        Task<int> GetCountOrId<P>(string storedProcedure, P parameters, string connectionId = "default");
        Task<int> GetCountOrIdQ<P>(string query, P parameters, string connectionId = "default");
        Task<decimal> GetCostQ<P>(string query, P parameters, string connectionId = "default");
        Task<string> GetText<P>(string storedProcedure, P parameters, string connectionId = "default");
    }
}