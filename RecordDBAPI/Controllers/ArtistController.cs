using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecordDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        // GET: api/<ArtistController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var artists = await _artistRepository.GetArtists();
            return Ok(artists);
        }

        // GET: api/<ArtistController>
        [HttpGet]
        [Route("GetArtistList")]
        public async Task<IActionResult> GetArtistList()
        {
            var artists = await _artistRepository.GetArtistList();
            return Ok(artists);
        }

        // GET: api/<ArtistController>/GetNoBio
        [HttpGet]
        [Route("GetNoBio")]
        public async Task<IActionResult> GetNoBio()
        {
            var artists = await _artistRepository.GetArtistsWithNoBio();
            return Ok(artists);
        }

        // GET: api/<ArtistController>/GetNoBioCount
        [HttpGet]
        [Route("GetNoBioCount")]
        public async Task<IActionResult> GetNoBioCount()
        {
            int count = await _artistRepository.GetNoBioCount();
            return Ok(count);
        }

        // GET: api/<ArtistController>/GetArtistId/FirstName/{firstName}/LastName/{lastname}
        [HttpGet]
        [Route("GetArtistId/FirstName/{firstName}/LastName/{lastName}")]
        public async Task<IActionResult> GetArtistId(string firstName, string lastName)
        {
            int artistId = await _artistRepository.GetArtistId(firstName, lastName);
            return Ok(artistId);
        }

        // GET api/<ArtistController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var artist = await _artistRepository.GetArtistById(id);
            if (artist is null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        // GET api/<ArtistController>/GetArtistIdFromRecord/5
        [HttpGet]
        [Route("GetArtistIdFromRecord/{recordId}")]
        public async Task<IActionResult> GetArtistIdFromRecord(int recordId)
        {
            int artistId = await _artistRepository.GetArtistIdFromRecord(recordId);
            if (artistId == 0)
            {
                return NotFound();
            }
            return Ok(artistId);
        }

        // GET api/<ArtistController>/GetByName/Name/{name}
        [HttpGet]
        [Route("GetByName/Name/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var artist = await _artistRepository.GetArtistByName(name);
            if (artist is null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        // GET api/<ArtistController>/GetByFirstLastName/FirstName/{firstname}/LastName/{lastname}
        [HttpGet]
        [Route("GetByFirstLastName/FirstName/{firstName}/LastName/{lastName}")]
        public async Task<IActionResult> GetByFirstLastName(string firstName, string lastName)
        {
            var artist = await _artistRepository.GetArtistByFirstLastName(firstName, lastName);
            if (artist is null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        // GET api/<ArtistController>/GetBiography/5
        [HttpGet]
        [Route("GetBiography/{id}")]
        public async Task<IActionResult> GetBiography(int id)
        {
            var artist = await _artistRepository.GetBiography(id);
            if (artist is null)
            {
                return NotFound();
            }
            return Ok(artist);
        }

        // POST api/<ArtistController>
        [HttpPost]
        public async Task<IActionResult> Post(Artist artist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var result = await _artistRepository.AddArtist(artist);
            if (!result)
            {
                return BadRequest("could not save data");
            }

            return Ok();

        }

        // PUT api/<ArtistController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int artistId, [FromBody] Artist newArtist)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var artist = await _artistRepository.GetArtistById(artistId);

            if (artist is null)
            {
                return NotFound();
            }

            newArtist.ArtistId = artistId;
            var result = await _artistRepository.UpdateArtist(newArtist);

            if (!result)
            {
                return BadRequest("could not save data");
            }
            return Ok();
        }

        // DELETE api/<ArtistController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int artistId)
        {
            var artist = await _artistRepository.GetArtistById(artistId);
            if (artist is null)
            {
                return NotFound();
            }

            var result = await _artistRepository.DeleteArtist(artistId);
            if (!result)
            {
                return BadRequest("could not save data");
            }

            return Ok();
        }
    }
}
