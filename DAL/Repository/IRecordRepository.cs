using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRecordRepository
    {
        Task<bool> AddRecord(Record record);
        Task<IEnumerable<Record>> GetRecords();
        Task<Record?> GetRecordById(int recordId);
        Task<bool> UpdateRecord(Record record);
        Task<bool> DeleteRecord(int recordId);
    }
}
