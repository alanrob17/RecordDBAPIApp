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

        public async Task<Record?> GetRecordById(int recordId)
        {
            string sproc = "up_RecordSelectById";
            var parameter = new DynamicParameters();
            parameter.Add("@RecordId", recordId);

            IEnumerable<Record> record = await _db.GetData<Record, dynamic>(sproc, parameter);
            return record?.FirstOrDefault() ?? null;
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
