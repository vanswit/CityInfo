using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return new JsonResult(CitiesDataStore.Current.Cities);
            var cities = CitiesDataStore.Current.Cities;
           
            return Ok(cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            return Ok(cityToReturn);
        }

        [HttpPost("{id}")]
        public IActionResult CreateCity([FromBody] CityForCreationDto city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityToAdd = new CityForCreationDto()
            {
                Description = city.Description,
                Name = city.Name
            };

            CitiesDataStore.Current.Cities.Add(cityToAdd);

            return NoContent();
        } 
    }
}
