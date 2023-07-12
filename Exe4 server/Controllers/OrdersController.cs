using Exe_1.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exe_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        [HttpGet]
        public IActionResult Get()
        {
            Vacation vacation = new Vacation();
            List<Vacation> OrdersList = vacation.Read();

            if (OrdersList.Count > 0)
            {
                return Ok(OrdersList);
            }
            else
            {
                return NotFound("No such orders");
            }
        }

        [HttpGet("{month}")]
        public Object getAvgPricePerCityByMonth(int month)
        {
            Vacation vacation = new Vacation();
           
            return vacation.getAvgPricePerCityByMonth(month);
        }

        // POST api/<OrdersController>
        [HttpPost]
        public bool Post([FromBody] Vacation vacation)
        {
            return vacation.Insert();

        }

        // GET: api/<OrdersController>
        [HttpGet("/startDate/{startDate}/endDate/{endDate}")]
        public IActionResult getByDates(DateTime startDate, DateTime endDate)
        {
            Vacation vacation=new Vacation();
            List<Vacation> ordersByDatesList = vacation.ordersByDates(startDate, endDate);

            if (ordersByDatesList.Count > 0)
            {
                return Ok(ordersByDatesList);
            }
            else
            {
                return NotFound("no such flats");
            }
        }
        
    }
}
