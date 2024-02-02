using DAL.Data;
using DAL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class RecordRepository : IRecordRepository
    {
        private readonly IDataAccess _db;
        public RecordRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Record>> GetRecords()
        {
            string sproc = "up_RecordSelectAll";
            var records = await _db.GetData<Record, dynamic>(sproc, new { });
            return records;
        }

        public async Task<IEnumerable<Record>> GetRecordList()
        {
            string sproc = "up_GetAllArtistsAndRecords";
            var records = await _db.GetData<Record, dynamic>(sproc, new { });
            return records;
        }

        public async Task<Record?> GetRecordById(int recordId)
        {
            string sproc = "up_RecordSelectById";
            var parameter = new DynamicParameters();
            parameter.Add("@RecordId", recordId);

            IEnumerable<Record> record = await _db.GetData<Record, dynamic>(sproc, parameter);
            return record?.FirstOrDefault() ?? null;
        }

        public async Task<Record?> GetFormattedRecord(int recordId)
        {
            string sproc = "up_getSingleRecord";
            var parameter = new DynamicParameters();
            parameter.Add("@RecordId", recordId);

            IEnumerable<Record> record = await _db.GetData<Record, dynamic>(sproc, parameter);
            return record?.FirstOrDefault() ?? null;
        }

        public async Task<dynamic?> GetArtistRecordEntity(int recordId)
        {
            string sproc = "up_GetArtistRecordEntity";
            var parameter = new DynamicParameters();
            parameter.Add("@RecordId", recordId);

            IEnumerable<dynamic> record = await _db.GetData<dynamic, dynamic>(sproc, parameter);
            return record?.FirstOrDefault() ?? null;
        }

        public async Task<IEnumerable<dynamic>> GetMissingRecordReviews()
        {
            string sproc = "up_MissingRecordReview";

            IEnumerable<dynamic> records = await _db.GetData<dynamic, dynamic>(sproc, new { });
            return records;
        }

        public async Task<int> GetTotalNumberOfCDs()
        {
            string sproc = "up_GetTotalNumberOfAllCDs";

            int count = await _db.GetCountOrId<dynamic>(sproc, new { });
            return count;
        }

        public async Task<int> GetTotalNumberOfDiscs()
        {
            string sproc = "up_GetTotalNumberOfAllRecords";

            int count = await _db.GetCountOrId<dynamic>(sproc, new { });
            return count;
        }

        public async Task<int> GetTotalNumberOfRecords()
        {
            string sproc = "up_GetTotalNumberOfRecords";

            int count = await _db.GetCountOrId<dynamic>(sproc, new { });
            return count;
        }

        public async Task<int> GetTotalNumberOfBlurays()
        {
            string sproc = "up_GetTotalNumberOfAllBlurays";

            int count = await _db.GetCountOrId<dynamic>(sproc, new { });
            return count;
        }

        public async Task<int> GetNoReviewCount()
        {
            string sproc = "up_GetNoRecordReviewCount";

            int count = await _db.GetCountOrId<dynamic>(sproc, new { });
            return count;
        }

        public async Task<int> CountAllDiscs(string media="")
        {
            var mediaType = 0;

            switch (media)
            {
                case "":
                    mediaType = 0;
                    break;
                case "DVD":
                    mediaType = 1;
                    break;
                case "CD":
                    mediaType = 2;
                    break;
                case "R":
                    mediaType = 3;
                    break;
                default:
                    break;
            }

            string sproc = "up_GetMediaCountByType";
            var parameter = new DynamicParameters();
            parameter.Add("@MediaType", mediaType);

            int count = await _db.GetCountOrId<dynamic>(sproc, parameter);
            return count;
        }

        public async Task<dynamic?> GetNumberOfArtistRecords(int artistId)
        {
            string sproc = "up_GetArtistAndNumberOfRecords";
            var parameter = new DynamicParameters();
            parameter.Add("@ArtistId", artistId);

            dynamic result = await _db.GetData<dynamic, dynamic>(sproc, parameter);
            return result ?? null;
        }

        public async Task<string> GetArtistName(int recordId)
        {
            string sproc = "up_GetArtistNameByRecordId";
            var parameter = new DynamicParameters();
            parameter.Add("@RecordId", recordId);

            string name = await _db.GetText<dynamic>(sproc, parameter);
            return name;
        }

        public async Task<int> GetYearDiscCount(int year)
        {
            string sproc = "up_GetRecordedYearNumber";
            var parameter = new DynamicParameters();
            parameter.Add("@Year", year);

            int count = await _db.GetCountOrId<dynamic>(sproc, parameter);
            return count;
        }

        public async Task<int> GetBoughtYearDiscCount(int year)
        {
            string sproc = "up_GetBoughtDiscCountForYear";
            var parameter = new DynamicParameters();
            parameter.Add("@Year", year);

            int count = await _db.GetCountOrId<dynamic>(sproc, parameter);
            return count;
        }

        public async Task<bool> AddRecord(Record record)
        {
            try
            {
                string sproc = "adm_RecordInsert";
                var parameters = new DynamicParameters();
                parameters.Add("@RecordId", record.RecordId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
                parameters.Add("@ArtistId", record.ArtistId);
                parameters.Add("@Name", record.Name);
                parameters.Add("@Field", record.Field);
                parameters.Add("@Recorded", record.Recorded);
                parameters.Add("@Label", record.Label);
                parameters.Add("@Pressing", record.Pressing);
                parameters.Add("@Rating", record.Rating);
                parameters.Add("@Discs", record.Discs);
                parameters.Add("@Media", record.Media);
                parameters.Add("@Bought", record.Bought);
                parameters.Add("@Cost", record.Cost);
                parameters.Add("@Review", record.Review);

                await _db.SaveData(sproc, parameters);
                int recordId = parameters.Get<int>("@RecordId");

                await Console.Out.WriteLineAsync($"New Record Id: {recordId}.");
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateRecord(Record record)
        {
            var result = 0;

            try
            {
                string sproc = "adm_UpdateRecord";
                var parameters = new DynamicParameters();
                parameters.Add("@RecordId", record.RecordId);
                parameters.Add("@Name", record.Name);
                parameters.Add("@Field", record.Field);
                parameters.Add("@Recorded", record.Recorded);
                parameters.Add("@Label", record.Label);
                parameters.Add("@Pressing", record.Pressing);
                parameters.Add("@Rating", record.Rating);
                parameters.Add("@Discs", record.Discs);
                parameters.Add("@Media", record.Media);
                parameters.Add("@Bought", record.Bought);
                parameters.Add("@Cost", record.Cost);
                parameters.Add("@Review", record.Review);
                parameters.Add("@Result", result, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _db.SaveData(sproc, parameters);
                await Console.Out.WriteLineAsync($"Updated Record Id: {result}.");

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteRecord(int recordId)
        {
            try
            {
                string sproc = "up_DeleteRecord";
                var parameter = new DynamicParameters();
                parameter.Add("@RecordId", recordId);

                await _db.SaveData(sproc, parameter);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
