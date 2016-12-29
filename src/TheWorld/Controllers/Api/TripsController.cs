using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    public class TripsController : Controller
    {
        //[HttpGet("api/trips")]
        //public JsonResult Get()
        //{
        //    return Json(new Trip() { Name = "My Trip"});
        //}

        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            //if (true) return BadRequest("Bad things happened");

            return Ok(new Trip() { Name = "My Trip" });
        }
    }
}
