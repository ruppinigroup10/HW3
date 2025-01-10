using Server_HW3.Models;
using System.Text.Json.Serialization;

namespace Server.Models
{
    public class Game
    {
        private int appID;
        private string name;
        private DateTime releaseDate;
        private double price;
        private string description;
        private string headerImage;
        private string website;
        private bool windows;
        private bool mac;
        private bool linux;
        private int scoreRank;
        private string recommendations;
        private string publisher;

        private static List<Game> gameList = new List<Game>();

        [JsonPropertyName("AppID")]
        public int AppID { get => appID; set => appID = value; }
        [JsonPropertyName("Name")]
        public string Name { get => name; set => name = value; }
        [JsonPropertyName("ReleaseDate")]
        public DateTime ReleaseDate { get => releaseDate; set => releaseDate = value; }
        [JsonPropertyName("Price")]
        public double Price { get => price; set => price = value; }
        [JsonPropertyName("Description")]
        public string Description { get => description; set => description = value; }
        [JsonPropertyName("HeaderImage")]
        public string HeaderImage { get => headerImage; set => headerImage = value; }
        [JsonPropertyName("Website")]
        public string Website { get => website; set => website = value; }
        [JsonPropertyName("Windows")]
        public bool Windows { get => windows; set => windows = value; }
        [JsonPropertyName("Mac")]
        public bool Mac { get => mac; set => mac = value; }
        [JsonPropertyName("Linux")]
        public bool Linux { get => linux; set => linux = value; }
        [JsonPropertyName("ScoreRank")]
        public int ScoreRank { get => scoreRank; set => scoreRank = value; }
        [JsonPropertyName("Recommendations")]
        public string Recommendations { get => recommendations; set => recommendations = value; }
        [JsonPropertyName("Publisher")]
        public string Publisher { get => publisher; set => publisher = value; }

        public Game()
        {
            this.appID = 0;
            this.name = string.Empty;
            this.releaseDate = new DateTime();
            this.price = 0;
            this.description = string.Empty;
            this.headerImage = string.Empty;
            this.website = string.Empty;
            this.windows = false;
            this.mac = false;
            this.linux = false;
            this.scoreRank = 0;
            this.publisher = string.Empty;
            this.recommendations = string.Empty;
            this.publisher = string.Empty;
        }

        public Game(int appID, string name, DateTime releaseDate, double price, string description, string headerImage, string website, bool windows, bool mac, bool linux, int scoreRank, string recommendations, string publisher)
        {
            this.appID = appID;
            this.name = name;
            this.releaseDate = releaseDate;
            this.price = price;
            this.description = description;
            this.headerImage = headerImage;
            this.website = website;
            this.windows = windows;
            this.mac = mac;
            this.linux = linux;
            this.scoreRank = scoreRank;
            this.recommendations = recommendations;
            this.publisher = publisher;
        }

        public bool insertGame(GameUser gameUser)
        {
            DBservices dbs = new DBservices();
            int res = dbs.InsertGame(gameUser);
            if (res > 0)
                return true;
            return false;

            //if (gameList.Exists(x => (x.AppID == this.AppID || x.Name == this.Name)))
            //{
            //    return false;
            //}
            //else
            //{
            //    gameList.Add(this);
            //    return true;
            //}
        }
        public static List<Game> readGame()
        {
            DBservices dbs = new DBservices();
            Console.WriteLine("in BL before return");
            return dbs.getAllGames();

            //return gameList;

        }

        // public static List<Game> readMyGame(User user)
        // {
        //     DBservices dbs = new DBservices();
        //     Console.WriteLine("in BL before return");
        //     return dbs.getAllMyGames(user);

        //     //return gameList;

        // }

        public static List<Game> GetByPrice(double minPrice)
        {
            List<Game> returnList = new List<Game>();
            foreach (var game in gameList)
            {
                if (game.Price >= minPrice)
                    returnList.Add(game);
            }
            return returnList;
        }

        public static List<Game> GetByRankScore(int minScore)
        {
            List<Game> returnList = new List<Game>();
            foreach (var game in gameList)
            {
                if (game.scoreRank >= minScore)
                    returnList.Add(game);
            }
            return returnList;
        }

        public static int DeleteById(int id)
        {
            if (id < 0)
            {
                throw new Exception($"Invalid ID: {id}");
            }
            return gameList.RemoveAll(x => x.AppID == id);// RemoveAll returns the number of elements removed
        }
    }
}
