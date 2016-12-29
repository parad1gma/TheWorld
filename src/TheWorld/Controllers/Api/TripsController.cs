using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    public class TripsController : Controller
    {
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            //if (true) return BadRequest("Bad things happened");

            return Ok(_repository.GetAllTrips());
        }
    }
}
