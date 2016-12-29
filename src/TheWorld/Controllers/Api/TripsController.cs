using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //if (true) return BadRequest("Bad things happened");

            return Ok(_repository.GetAllTrips());
        }

        [HttpPost]
        public IActionResult Post([FromBody]Trip trip)
        {
            return Ok(true);
        }
    }
}
