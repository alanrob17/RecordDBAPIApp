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
        Task<IEnumerable<Record>> GetRecordList();
        Task<IEnumerable<dynamic>> GetMissingRecordReviews();
        Task<Record?> GetRecordById(int recordId);
        Task<Record?> GetFormattedRecord(int recordId);
        Task<dynamic?> GetArtistRecordEntity(int recordId);
        Task<IEnumerable<Total>> GetTotalCosts();
        Task<IEnumerable<dynamic>> GetTotalDiscsForEachArtist();
        Task<bool> UpdateRecord(Record record);
        Task<bool> DeleteRecord(int recordId);
        Task<int> GetTotalNumberOfCDs();
        Task<int> GetTotalNumberOfDiscs();
        Task<int> GetTotalNumberOfRecords();
        Task<int> GetTotalNumberOfBlurays();
        Task<int> GetYearDiscCount(int year);
        Task<int> GetBoughtYearDiscCount(int year);
        Task<int> CountAllDiscs(string media = "");
        Task<dynamic?> GetNumberOfArtistRecords(int artistId);
        Task<int> GetNoReviewCount();
        Task<string> GetArtistName(int recordId);
    }
}
