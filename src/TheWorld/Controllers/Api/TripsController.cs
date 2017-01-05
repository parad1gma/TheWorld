using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("/api/trips")]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private IWorldRepository _repository;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                //var result = _repository.GetAllTrips();
                IEnumerable<Trip> result = _repository.GetTripsByUsername(this.User.Identity.Name);


                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get All Trips: {ex}");

                return BadRequest("Something wrong has happen");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                // Save to the Database
                var newTrip = Mapper.Map<Trip>(trip);

                newTrip.UserName = User.Identity.Name;

                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    //return Created($"api/trips/{trip.Name}", trip);
                    return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
                else
                {
                    return BadRequest("Failed to save changes to the Database");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
