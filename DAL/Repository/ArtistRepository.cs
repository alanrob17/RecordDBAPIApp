using DAL.Data;
using DAL.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IDataAccess _db;
        public ArtistRepository(IDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Artist>> GetArtists()
        {
            string sproc = "up_ArtistSelectFull";
            var artists = await _db.GetData<Artist, dynamic>(sproc, new { });
            return artists;
        }

        public async Task<IEnumerable<Artist>> GetArtistList()
        {
            string sproc = "up_getArtistListandNone";
            var artists = await _db.GetData<Artist, dynamic>(sproc, new { });
            return artists;
        }

        public async Task<IEnumerable<Artist>> GetArtistsWithNoBio()
        {
            string sproc = "up_selectArtistsWithNoBio";
            var artists = await _db.GetData<Artist, dynamic>(sproc, new { });
            return artists;
        }

        public async Task<int> GetNoBioCount()
        {
            string sproc = "up_NoBioCount";
            int count = await _db.GetCount<dynamic>(sproc, new { });
            return count;
        }

        public async Task<int> GetArtistId(string firstName, string lastName)
        {
            string sproc = "up_getArtistID";
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName);
            parameters.Add("@LastName", lastName);


            int artistId = await _db.GetCount<dynamic>(sproc, parameters);
            return artistId;
        }

        public async Task<Artist> GetArtistById(int artistId)
        {
            string sproc = "up_ArtistSelectById";
            var parameter = new DynamicParameters();
            parameter.Add("@ArtistId", artistId);

            IEnumerable<Artist> artist = await _db.GetData<Artist, dynamic>(sproc, parameter);
            return artist.FirstOrDefault();
        }

        public async Task<int> GetArtistIdFromRecord(int recordId)
        {
            int artistId = 0;
            string sproc = "up_getArtistIdFromRecordId";
            var parameter = new DynamicParameters();
            parameter.Add("@RecordId", recordId);

            artistId = await _db.GetCount<dynamic>(sproc, parameter);
            return artistId;
        }

        public async Task<Artist> GetArtistByName(string name)
        {
            string sproc = "up_GetArtistByName";
            var parameter = new DynamicParameters();
            parameter.Add("@Name", name);

            IEnumerable<Artist> artist = await _db.GetData<Artist, dynamic>(sproc, parameter);
            return artist.FirstOrDefault();
        }

        public async Task<Artist> GetArtistByFirstLastName(string firstName, string lastName)
        {
            string sproc = "up_ArtistByFirstLastName";
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", firstName);
            parameters.Add("@LastName", lastName);

            IEnumerable<Artist> artist = await _db.GetData<Artist, dynamic>(sproc, parameters);
            return artist.FirstOrDefault();
        }

        public async Task<Artist> GetBiography(int artistId)
        {
            string sproc = "up_ArtistSelectById";
            var parameter = new DynamicParameters();
            parameter.Add("@ArtistId", artistId);

            IEnumerable<Artist> artist = await _db.GetData<Artist, dynamic>(sproc, parameter);
            return artist.FirstOrDefault();
        }

        public async Task<bool> AddArtist(Artist artist)
        {
            try
            {
                string sproc = "sp_AddNewArtist";
                var parameters = new DynamicParameters();
                parameters.Add("@FirstName", artist.FirstName);
                parameters.Add("@LastName", artist.LastName);
                parameters.Add("@Name", artist.Name);
                parameters.Add("@Biography", artist.Biography);
                parameters.Add("@ArtistId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await _db.SaveData(sproc, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateArtist(Artist artist)
        {
            try
            {
                string sproc = "up_UpdateArtist";
                var parameters = new DynamicParameters();
                parameters.Add("@ArtistId", artist.ArtistId);
                parameters.Add("@FirstName", artist.FirstName);
                parameters.Add("@LastName", artist.LastName);
                parameters.Add("@Name", artist.Name);
                parameters.Add("@Biography", artist.Biography);
                parameters.Add("@Result", dbType: DbType.Int32, direction: ParameterDirection.InputOutput);

                await _db.SaveData(sproc, parameters);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteArtist(int artistId)
        {
            try
            {
                string sproc = "up_deleteArtist";
                var parameter = new DynamicParameters();
                parameter.Add("@ArtistId", artistId);

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
