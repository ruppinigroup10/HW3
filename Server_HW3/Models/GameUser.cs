using Server.Models;

namespace Server_HW3.Models
{
    public class GameUser
    {
        public Game game { get; set; }
        public User user { get; set; }

        public GameUser(Game game, User user)
        {
            this.game = game;
            this.user = user;
        }
    }
}
