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

        // GET api/<RecordController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _recordRepository.GetRecordById(id);
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
        [HttpPut("{id}")]
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
        [HttpDelete("{id}")]
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
