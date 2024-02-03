using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecordDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _recordRepository;

        public RecordController(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        // GET: api/<RecordController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var records = await _recordRepository.GetRecords();
            return Ok(records);

        }

        // GET: api/<RecordController>/GetRecordList
        [HttpGet]
        [Route("GetRecordList")]
        public async Task<IActionResult> GetRecordList()
        {
            var records = await _recordRepository.GetRecordList();
            return Ok(records);
        }

        // GET: api/<RecordController>/MissingRecordReview
        [HttpGet]
        [Route("MissingRecordReview")]
        public async Task<IActionResult> MissingRecordReview()
        {
            IEnumerable<dynamic> records = await _recordRepository.GetMissingRecordReviews();
            return Ok(records);
        }

        // GET: api/<RecordController>/GetTotals
        [HttpGet]
        [Route("GetTotals")]
        public async Task<IActionResult> GetTotals()
        {
            IEnumerable<dynamic> records = await _recordRepository.GetTotalCosts();
            return Ok(records);
        }

        // GET: api/<RecordController>/TotalArtistDiscs
        [HttpGet]
        [Route("TotalArtistDiscs")]
        public async Task<IActionResult> TotalArtistDiscs()
        {
            IEnumerable<dynamic> records = await _recordRepository.GetTotalDiscsForEachArtist();
            return Ok(records);
        }

        // GET api/<RecordController>/5
        [HttpGet("{recordId}")]
        public async Task<IActionResult> Get(int recordId)
        {
            var record = await _recordRepository.GetRecordById(recordId);
            if (record is null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // GET api/<RecordController>/5
        [HttpGet]
        [Route("GetFormattedRecord/{recordId}")]
        public async Task<IActionResult> GetFormattedRecord(int recordId)
        {
            var record = await _recordRepository.GetFormattedRecord(recordId);
            if (record is null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // GET api/<RecordController>/GetArtistRecord/5
        [HttpGet]
        [Route("GetArtistRecord/{recordId}")]
        public async Task<IActionResult> GetArtistRecord(int recordId)
        {
            var record = await _recordRepository.GetArtistRecordEntity(recordId);
            if (record is null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // GET: api/<RecordController>/GetCDTotal
        [HttpGet]
        [Route("GetCDTotal")]
        public async Task<IActionResult> GetCDTotal()
        {
            int count = await _recordRepository.GetTotalNumberOfCDs();
            return Ok(count);
        }

        // GET: api/<RecordController>/GetTotalDiscs
        [HttpGet]
        [Route("GetTotalDiscs")]
        public async Task<IActionResult> GetTotalDiscs()
        {
            int count = await _recordRepository.GetTotalNumberOfDiscs();
            return Ok(count);
        }

        // GET: api/<RecordController>/GetTotalRecords
        [HttpGet]
        [Route("GetTotalRecords")]
        public async Task<IActionResult> GetTotalRecords()
        {
            int count = await _recordRepository.GetTotalNumberOfRecords();
            return Ok(count);
        }

        // GET: api/<RecordController>/GetTotalRecords
        [HttpGet]
        [Route("GetTotalBlurays")]
        public async Task<IActionResult> GetTotalBlurays()
        {
            int count = await _recordRepository.GetTotalNumberOfBlurays();
            return Ok(count);
        }

        // GET: api/<RecordController>/CountAllDiscs/{media}
        [HttpGet]
        [Route("CountAllDiscs")]
        public async Task<IActionResult> CountAllDiscs(string media = "")
        {
            int count = await _recordRepository.CountAllDiscs(media);
            return Ok(count);
        }

        // GET: api/<RecordController>/GetYearDiscCount/1974
        [HttpGet]
        [Route("GetYearDiscCount/{year}")]
        public async Task<IActionResult> GetYearDiscCount(int year)
        {
            int count = await _recordRepository.GetYearDiscCount(year);
            return Ok(count);
        }

        // GET: api/<RecordController>/GetBoughtYearDiscCount/1974
        [HttpGet]
        [Route("GetBoughtYearDiscCount/{year}")]
        public async Task<IActionResult> GetBoughtYearDiscCount(int year)
        {
            int count = await _recordRepository.GetBoughtYearDiscCount(year);
            return Ok(count);
        }

        // GET: api/<RecordController>/GetTotalRecords
        [HttpGet]
        [Route("NoReviewCount")]
        public async Task<IActionResult> NoReviewCount()
        {
            int count = await _recordRepository.GetNoReviewCount();
            return Ok(count);
        }

        // GET: api/<RecordController>/GetArtistDiscCount/{artistId}
        [HttpGet]
        [Route("GetArtistDiscCount/{artistId}")]
        public async Task<IActionResult> GetArtistDiscCount(int artistId)
        {
            dynamic result = await _recordRepository.GetNumberOfArtistRecords(artistId);
            return Ok(result);
        }

        // GET api/<ArtistController>/GetArtistName/5
        [HttpGet]
        [Route("GetArtistName/{recordId}")]
        public async Task<IActionResult> GetArtistName(int recordId)
        {
            var record = await _recordRepository.GetArtistName(recordId);
            if (record is null)
            {
                return NotFound();
            }
            return Ok(record);
        }


        // POST api/<RecordController>
        [HttpPost]
        public async Task<IActionResult> Post(Record record)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var result = await _recordRepository.AddRecord(record);
            if (!result)
            {
                return BadRequest("could not save data");
            }

            return Ok();
        }

        // PUT api/<RecordController>/5
        [HttpPut("{recordId}")]
        public async Task<IActionResult> Put(int recordId, [FromBody] Record newRecord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var record = await _recordRepository.GetRecordById(recordId);

            if (record is null)
            {
                return NotFound();
            }

            newRecord.RecordId = recordId;
            var result = await _recordRepository.UpdateRecord(newRecord);

            if (!result)
            {
                return BadRequest("could not save data");
            }
            return Ok();
        }

        // DELETE api/<RecordController>/5
        [HttpDelete("{recordId}")]
        public async Task<IActionResult> Delete(int recordId)
        {
            var artist = await _recordRepository.GetRecordById(recordId);
            if (artist is null)
            {
                return NotFound();
            }

            var result = await _recordRepository.DeleteRecord(recordId);
            if (!result)
            {
                return BadRequest("could not save data");
            }

            return Ok();
        }
    }
}
