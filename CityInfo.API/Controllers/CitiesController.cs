using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
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
        private ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            //return new JsonResult(CitiesDataStore.Current.Cities);
            var cityEntities = _cityInfoRepository.GetCities();

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities);

            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest = false)
        {
            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityResult = Mapper.Map<IEnumerable<CityDto>>(city);
                return Ok(cityResult);
            }

            var cityWithoutPointsOfInterestResult = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(city);
            return Ok(cityWithoutPointsOfInterestResult);

            //return new JsonResult(CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id));
            //var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            //if (cityToReturn == null)
            //{
            //    return NotFound();
            //}

            //return Ok(cityToReturn);


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

            

            return NoContent();
        } 
    }
}
 