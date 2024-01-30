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
        Task<Artist> GetArtistById(int ArtistId);
        Task<bool> UpdateArtist(Artist artist);
        Task<bool> DeleteArtist(int ArtistId);
    }
}
