﻿using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server_HW3.Models;


namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        // GET: api/<GamesController> Get all games
        [HttpGet("GetAllGames")]
        public ActionResult<IEnumerable<Game>> GetAllGames()
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

        //GET: api/<GamesController>/5 by user id
        [HttpGet("GetGamesByUserId/userID/{ID}")]
        public ActionResult<IEnumerable<Game>> GetGamesByUserId(int ID)
        {
            try
            {
                Console.WriteLine($"API GetGamesByUserId request received for user {ID}");
                User user = new User { id = ID };
                var games = Game.readMyGame(user);
                Console.WriteLine($"Retrieved {games.Count} games for user {ID}");
                return Ok(games);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetGamesByUserId: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }


        // GET api/<GamesController>/5 filter by price
        [HttpGet("FilterMyGamesByPrice/{ID}/{minPrice}")]
        public ActionResult<IEnumerable<Game>> FilterMyGamesByPrice(int ID, double minPrice)
        {
            try
            {
                Console.WriteLine($"API FilterMyGamesByPrice request received for user {ID}");
                User user = new User { id = ID };
                var games = Game.filterMyGamesByPrice(user, minPrice);
                return Ok(games);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FilterMyGamesByPrice: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<GamesController>/5 filter by rank
        [HttpGet("FilterMyGamesByRank/{ID}/{minRank}")]
        public ActionResult<IEnumerable<Game>> FilterMyGamesByRank(int ID, int minRank)
        {
            try
            {
                Console.WriteLine($"API FilterMyGamesByRank request received for user {ID}");
                User user = new User { id = ID };
                var games = Game.filterMyGamesByRank(user, minRank);
                return Ok(games);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FilterMyGamesByRank: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }



        // POST api/<GameController> Add a game
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
        // [HttpPut("{id}")]
        // public void Put(int id, [FromBody] string value)
        // {
        // }

        // DELETE api/<GamesController>/5 by user id and game id
        [HttpDelete("{userId}/{gameId}")]
        public IActionResult Delete(int userId, int gameId)
        {
            try
            {
                int result = Game.DeleteById(userId, gameId);

                switch (result)
                {
                    case 1:  // Success
                        return Ok(new { message = $"Game with ID {gameId} deleted successfully." });
                    case -1: // Not found or failed
                        return NotFound(new { message = $"Game with ID {gameId} not found for this user." });
                    default: // Unexpected result
                        return BadRequest(new { message = "An unexpected error occurred." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
