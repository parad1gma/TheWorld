using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TheWorld.Models;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Api
{
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
                var result = _repository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get All Trips: {ex}");

                return BadRequest("Something wrong has happen");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                // Save to the Database
                var newTrip = Mapper.Map<Trip>(trip);

                //return Created($"api/trips/{trip.Name}", trip);
                return Created($"api/trips/{trip.Name}", Mapper.Map<TripViewModel>(newTrip));
            }

            return BadRequest(ModelState);
        }
    }
}
