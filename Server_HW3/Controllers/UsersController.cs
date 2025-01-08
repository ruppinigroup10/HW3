using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return Server.Models.User.readUser();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UsersController>
        [HttpPost]
        public Boolean Post([FromBody] User user)
        {
            return user.insertUser();
        }

        [HttpPost("Register")]
        public IActionResult Register([FromBody] User user)
        {
            int result = Server.Models.User.Register(user.name, user.email, user.password);
            if (result == -1)
            {
                return Conflict(new { message = "Email already exists." });
            }
            else
            if (result == 0)
            {
                return BadRequest(new { message = "User registration failed." });
            }
            else
            {
                return Ok(new { message = "Registered successfuly" });
            }
        }

        // [HttpPost("Register")]
        // public IActionResult Register([FromBody] string Name, string Email, string Password)
        // {
        //     User user = new User();
        //     int result = Server.Models.User.Register(Name, Email, Password);
        //     if (result == -1)
        //     {
        //         return Conflict("User already exists.");
        //     }
        //     else
        //     if (result == 0)
        //     {
        //         return BadRequest("User registration failed.");
        //     }
        //     else
        //     {
        //         return Ok("User registered successfully.");
        //     }
        // }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] User user)
        {
            int res = Server.Models.User.Login(user.email, user.password);

            if (res == -1)
            {
                return BadRequest(new { message = "Invalid user" });
            }
            if (res == 0)
            {
                return Conflict(new { message = "Wrong password" });
            }
            else // res == 1
            {
                return Ok(new { message = "Login successful" });
            }
        }



        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}