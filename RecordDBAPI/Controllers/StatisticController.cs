using DAL.Models;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecordDBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private readonly IStatisticRepository _statisticRepository;

        public StatisticController(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        // GET: api/<StatisticController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            Statistic statistics = await _statisticRepository.GetStatistics();
            return Ok(statistics);
        }
    }
}
