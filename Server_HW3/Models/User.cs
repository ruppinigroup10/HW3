namespace Server.Models
{
    public class User
    {
        private int ID;
        private string Name;
        private string Email;
        private string Password;
        private static List<User> UsersList = new List<User>();



        public int id { get => ID; set => ID = value; }
        public string name { get => Name; set => Name = value; }
        public string email { get => Email; set => Email = value; }
        public string password { get => Password; set => Password = value; }

        public User()
        {
            this.ID = 0;
            this.Name = "";
            this.Email = "";
            this.Password = "";
        }
        public User(int ID, string Name, string Email, string Password)
        {
            this.ID = ID;
            this.Name = Name;
            this.Email = Email;
            this.Password = Password;
        }

        public Boolean insertUser()
        {
            if (UsersList.Exists(x => x.ID == this.ID))
            {
                return false;
            }
            else
            {
                UsersList.Add(this);
                return true;
            }
        }
        public static List<User> readUser()
        {
            return UsersList;
        }

        //User? is a nullable type, it can be either User or null
        public static User? Register(string name, string email, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.RegisterUser(name, email, password);
        }
        public static User? Login(string email, string password)
        {
            DBservices dbs = new DBservices();
            return dbs.LoginUser(email, password);
        }

        //old func before DB:

        // Static global counter
        //private static int GlobalUserIdCounter = 1;

        // public static int Register(string Name, string Email, string Password)
        // {
        //     if (UsersList.Exists(x => x.Email == Email))
        //     {
        //         return -1;
        //     }

        //     User newUser = new User(GlobalUserIdCounter++, Name, Email, Password);
        //     if (newUser.insertUser())
        //     {
        //         return 1;
        //     }
        //     else
        //     {
        //         return 0;
        //     }
        // }

        // public static int Login(string email, string password)
        // {
        //     // Find the user by email
        //     User? user = UsersList.FirstOrDefault(u => u.Email == email);

        //     if (user == null)
        //     {
        //         return -1; // Email not found
        //     }

        //     // Verify password
        //     if (user.Password != password)
        //     {
        //         return 0; // Incorrect password
        //     }

        //     return 1; // Successful login
        // }
    }
}
