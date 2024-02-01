using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IArtistRepository
    {
        Task<bool> AddArtist(Artist artist);
        Task<IEnumerable<Artist>> GetArtists();
        Task<IEnumerable<Artist>> GetArtistList();
        Task<IEnumerable<Artist>> GetArtistsWithNoBio();
        Task<int> GetNoBioCount();
        Task<int> GetArtistId(string firstName, string lastName);
        Task<int> GetArtistIdFromRecord(int recordId);
        Task<Artist> GetArtistById(int ArtistId);
        Task<Artist> GetArtistByName(string name);
        Task<Artist> GetArtistByFirstLastName(string firstName, string lastName);
        Task<Artist> GetBiography(int ArtistId);
        Task<bool> UpdateArtist(Artist artist);
        Task<bool> DeleteArtist(int ArtistId);
    }
}
