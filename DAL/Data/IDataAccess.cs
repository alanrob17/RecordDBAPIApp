
namespace DAL.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string storedProcedure, P parameters, string connectionId = "default");
        Task SaveData<P>(string query, P parameters, string connectionId = "default");
        Task<int> SaveDataGetId<P>(string query, P parameters, string connectionId = "default");
    }
}