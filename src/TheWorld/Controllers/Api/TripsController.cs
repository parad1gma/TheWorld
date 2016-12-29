using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TheWorld.Models;
using TheWorld.ViewModels;

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
            try
            {
                var result = _repository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(result));
            }
            catch (Exception ex)
            {
                // TODO Logging

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
