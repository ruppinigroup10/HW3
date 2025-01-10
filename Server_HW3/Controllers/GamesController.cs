using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server_HW3.Models;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        // GET: api/<GamesController>
        [HttpGet]
        public ActionResult<IEnumerable<Game>> Get()
        {
            try
            {
                Console.WriteLine("API Get request received");
                var games = Game.readGame();
                Console.WriteLine($"Retrieved {games.Count} games");
                return Ok(games);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in Get: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetByPrice")] // query string
        public IEnumerable<Game> GetByPrice(double minPrice)
        {
            return Game.GetByPrice(minPrice);
        }

        [HttpGet("GetByRankScore/minRankScore/{minRankScore}")] // resource routing
        public IEnumerable<Game> GetByScoreRank(int minRankScore)
        {
            return Game.GetByRankScore(minRankScore);
        }


        // [HttpGet("GetGamesByUserId/userID/{ID}")]
        // public ActionResult<IEnumerable<Game>> GetGamesByUserId(int ID)
        // {
        //     try
        //     {
        //         Console.WriteLine($"API GetGamesByUserId request received for user {ID}");
        //         User user = new User { id = ID };
        //         var games = Game.readMyGame(user);
        //         Console.WriteLine($"Retrieved {games.Count} games for user {ID}");
        //         return Ok(games);
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Error in GetGamesByUserId: {ex.Message}");
        //         return StatusCode(500, ex.Message);
        //     }
        // }


        // POST api/<GameController>
        [HttpPost]
        public IActionResult Post([FromBody] GameUser gameUser)
        {
            if (gameUser.game == null)
            {
                return BadRequest("Game data is null");
            }
            bool result = gameUser.game.insertGame(gameUser);
            if (result)
            {
                return Ok(new { message = "Game added successfully" });
            }
            else
            {
                return BadRequest(new { message = "Failed to add game - duplicate game" });
            }
        }


        // PUT api/<GamesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<GamesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                int result = Game.DeleteById(id);
                if (result == 0)
                {
                    return BadRequest(new { message = $"Game with ID {id} not found." });
                }
                else
                {
                    return Ok(new { message = $"Game with ID {id} deleted successfully." });
                }
            }
            catch (Exception)
            {
                return BadRequest(new { message = $"Invalid ID: {id}" });
            }
        }
    }
}
