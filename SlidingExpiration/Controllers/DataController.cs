using Microsoft.AspNetCore.Mvc;
using SlidingExpiration.Services;

namespace SlidingExpiration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;

        public DataController(DataService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            var data = _dataService.GetDate(key);
            return Ok(data);
        }
    }
}
