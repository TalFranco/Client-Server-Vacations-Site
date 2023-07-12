using Exe_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Exe_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IActionResult Get()
        {
            Flat flat = new Flat();
            List<Flat> flatList = flat.Read();

            if (flatList.Count > 0)
            {
                return Ok(flatList);
            }
            else
            {
                return NotFound("No such flats");
            }
        }

        // POST api/<ValuesController>
        [HttpPost]
        public bool Post([FromBody] Flat flat)
        {
            return flat.Insert();
        }

        // GET: api/<ValuesController>
        [HttpGet("/Price")]
        public IActionResult GetByPrice(string city,double maxPrice)
        {
            Flat flat= new Flat();
            List<Flat> flatByPriceList = flat.FlatByPrice(city, maxPrice);

            if (flatByPriceList.Count>0)
            {
                return Ok(flatByPriceList);
            }
            else
            {
                return NotFound("No such flats");
            }
        }
    }
}
