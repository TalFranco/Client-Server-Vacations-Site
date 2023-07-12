using Exe_1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Exe_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        User user = new User();
        [HttpPost]
        public bool Post([FromBody] User user)
        {
            return user.Insert();
        }


        [HttpPut]
        public bool Put( [FromBody] User user)
        {

            return user.Update(user);

        }

        [HttpPut("{email}")]
        public bool UpdateStatus(string email)
        {

            return user.UpdateStatus(email);
           

        }

        [HttpGet]

        public IActionResult Get()
        {
            User user = new User();
            List<User> usersList = user.Read();

            if (usersList.Count > 0)
            {
                return Ok(usersList);
            }
            else
            {
                return NotFound("No Users");
            }
        }



    }
}
