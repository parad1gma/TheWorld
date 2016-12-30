using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using System.Collections;
using TheWorld.ViewModels;
using AutoMapper;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("/api/trips/{tripNAme}/stops")]
    public class StopsController : Controller
    {
        private ILogger<StopsController> _logger;
        private IWorldRepository _repository;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger) 
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string tripName)
        {

            try
            {
                var trip = _repository.GetTripByName(tripName);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to get stops: {ex.Message}");
            }

            return BadRequest("Failed to get stops");

        }

        [HttpPost]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel model)
        {

            try
            {
                // if the model is valid

                if (ModelState.IsValid)
                {
                    var newStop = Mapper.Map<Stop>(model);
                    // Lookup the Geocodes

                    // save to the Database

                    _repository.AddStop(tripName, newStop);

                    if (await _repository.SaveChangesAsync())
                    {

                        return Created($"/api/trips{tripName}/stops/{newStop.Name}", Mapper.Map<StopViewModel>(newStop));
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Failed to save new stop: {ex.Message}");
            }

            return BadRequest("Failed to save new stop");
        }
    }
}
