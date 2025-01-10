using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Server.Models;
using Server_HW3.Models;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{


    public DBservices()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //--------------------------------------------------------------------------------------------------
    // This method creates a connection to the database according to the connectionString name in the web.config 
    //--------------------------------------------------------------------------------------------------
    public SqlConnection connect(String conString)
    {

        // read the connection string from the configuration file
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json").Build();
        string cStr = configuration.GetConnectionString("myProjDB");
        SqlConnection con = new SqlConnection(cStr);
        con.Open();
        return con;
    }

    //--------------------------------------------------------------------------------------------------
    // This method inserts a car to the cars table 
    //--------------------------------------------------------------------------------------------------
    public int InsertGame(GameUser gameUser)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception)
        {
            // write to log
            throw;
        }

        Dictionary<string, object> paramDic = new Dictionary<string, object>();
        paramDic.Add("@ID", gameUser.user.id);
        paramDic.Add("@AppID", gameUser.game.AppID);

        cmd = CreateCommandWithStoredProcedureGeneral("SP_PurchasedGame", con, paramDic);          // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception)
        {
            // write to log
            throw;
        }

        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }

    }

    public List<Game> getAllGames()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception)
        {
            // write to log
            throw;
        }

        List<Game> gameList = new List<Game>();

        cmd = CreateCommandWithStoredProcedureGeneral("SP_ShowAllGames", con, null);

        try
        {

            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Game g = new Game();
                g.AppID = Convert.ToInt32(dataReader["AppID"]);
                g.Name = dataReader["Name"].ToString() ?? "";
                g.ReleaseDate = Convert.ToDateTime(dataReader["Release_date"]);
                g.Price = Convert.ToDouble(dataReader["Price"]);
                g.Publisher = dataReader["Developers"].ToString() ?? "";
                g.HeaderImage = dataReader["Header_image"].ToString() ?? "";
                gameList.Add(g);
            }
            return gameList;
        }
        catch (Exception)
        {
            // write to log
            throw;
        }
        finally
        {
            if (con != null)
            {
                // close the db connection
                con.Close();
            }
        }
    }

    // public List<Game> getAllMyGames(User user)
    // {

    //     SqlConnection con;
    //     SqlCommand cmd;

    //     try
    //     {
    //         con = connect("myProjDB"); // create the connection
    //     }
    //     catch (Exception)
    //     {
    //         // write to log
    //         throw;
    //     }

    //     List<Game> gameList = new List<Game>();

    //     Dictionary<string, object> paramDic = new Dictionary<string, object>();
    //     paramDic.Add("@ID", user.id);

    //     cmd = CreateCommandWithStoredProcedureGeneral("SP_ShowAllMyGames", con, paramDic);

    //     try
    //     {

    //         SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

    //         while (dataReader.Read())
    //         {
    //             Game g = new Game();
    //             g.AppID = Convert.ToInt32(dataReader["AppID"]);
    //             g.Name = dataReader["Name"].ToString() ?? "";
    //             g.ReleaseDate = Convert.ToDateTime(dataReader["Release_date"]);
    //             g.Price = Convert.ToDouble(dataReader["Price"]);
    //             g.Publisher = dataReader["Developers"].ToString() ?? "";
    //             g.HeaderImage = dataReader["Header_image"].ToString() ?? "";
    //             gameList.Add(g);
    //         }
    //         return gameList;
    //     }
    //     catch (Exception)
    //     {
    //         // write to log
    //         throw;
    //     }
    //     finally
    //     {
    //         if (con != null)
    //         {
    //             // close the db connection
    //             con.Close();
    //         }
    //     }
    // }

    //---------------------------------------------------------------------------------
    // Create the SqlCommand
    //---------------------------------------------------------------------------------
    private SqlCommand CreateCommandWithStoredProcedureGeneral(String spName, SqlConnection con, Dictionary<string, object> paramDic)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be text

        if (paramDic != null)
            foreach (KeyValuePair<string, object> param in paramDic)
            {
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            }


        return cmd;
    }


    //--------------------------------------------------------------------
    // TODO Build the FLight Delete  method
    // DeleteFlight(int id)
    //--------------------------------------------------------------------

}
