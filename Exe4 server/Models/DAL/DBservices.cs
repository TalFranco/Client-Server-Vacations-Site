using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using Exe_1.Models;
using Exe_1.Controllers;

/// <summary>
/// DBServices is a class created by me to provides some DataBase Services
/// </summary>
public class DBservices
{
    public SqlDataAdapter da;
    public DataTable dt;

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
    // Handel Read Flats & Vacations & Users & Other objects
    //--------------------------------------------------------------------------------------------------

    public List<Flat> ReadFlats()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Flat> Flatslist = new List<Flat>();

        cmd = CreateReadCommandSP("spReadFlats_Tal", con);// create the command
        
        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            
            while (dataReader.Read())
            {
                Flat f = new Flat();
                f.Id = Convert.ToInt32(dataReader["Id"]);
                f.Price = Convert.ToInt32(dataReader["Price"]);
                f.City = dataReader["City"].ToString();
                f.Address = dataReader["Address"].ToString();
                f.NumOfRooms = Convert.ToInt32(dataReader["NumOfRooms"]);
                Flatslist.Add(f);
            }
            return Flatslist;   

        }
        catch (Exception ex)
        {
            // write to log
            return Flatslist;
            throw (ex);
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
    public List<Vacation> ReadVacations()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Vacation> Vacationslist = new List<Vacation>(); 

        cmd = CreateReadCommandSP("spReadVacations_Tal", con);// create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Vacation v = new Vacation();    
                v.Id = Convert.ToInt32(dataReader["Id"]);
                v.UserId = (dataReader["UserId"]).ToString();
                v.FlatId = Convert.ToInt32(dataReader["FlatId"]);
                v.StartDate = Convert.ToDateTime(dataReader["StartDate"]);
                v.EndDate = Convert.ToDateTime(dataReader["EndDate"]);

                Vacationslist.Add(v);
            }
            return Vacationslist;

        }
        catch (Exception ex)
        {
            // write to log
            return Vacationslist;
            throw (ex);
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
    public List<User> ReadUsers()
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<User> Userslist = new List<User>();

        cmd = CreateReadCommandSP("spReadUsers_Tal", con);// create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User u=new User();
                u.FirstName= (dataReader["FirstName"]).ToString();
                u.FamilyName= (dataReader["FamilyName"]).ToString();
                u.Email= (dataReader["Email"]).ToString();
                u.Password= (dataReader["Password"]).ToString();
                u.IsAdmin= Convert.ToBoolean((dataReader["isAdmin"]));
                u.IsActive = Convert.ToBoolean((dataReader["isActive"]));
                Userslist.Add(u);
            }
            return Userslist;

        }
        catch (Exception ex)
        {
            // write to log
            return Userslist;
            throw (ex);
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
    public Object getAvgPricePerCityByMonth(int month)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        List<Object> ObjectList = new List<Object>();

        cmd = CreateReadCommandForAvgSP("spAvgPriceByMonth", con,month);// create the command

        try
        {
            SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                ObjectList.Add(new
                {
                City = dataReader["City"].ToString(),
                avgPrice= Convert.ToInt32(dataReader["avgPrice"])

            });
                
            }
            return ObjectList;

        }
        catch (Exception ex)
        {
            // write to log
            return ObjectList;
            throw (ex);
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
    private SqlCommand CreateReadCommandSP(String spName, SqlConnection con)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        return cmd;
    }

    private SqlCommand CreateReadCommandForAvgSP(String spName, SqlConnection con,int month)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure
        cmd.Parameters.AddWithValue("@month", month);


        return cmd;
    }


    //--------------------------------------------------------------------------------------------------
    // Handel Post Flats & Vacations & Users
    //--------------------------------------------------------------------------------------------------
    public int InsertFlat(Flat flat)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertFlatCommandSP("spInsertFlat_tal", con,flat);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
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
    private SqlCommand CreateInsertFlatCommandSP(String spName, SqlConnection con, Flat flat)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@Price", flat.Price);

        cmd.Parameters.AddWithValue("@City", flat.City);

        cmd.Parameters.AddWithValue("@Address", flat.Address);

        cmd.Parameters.AddWithValue("@NumOfRooms", flat.NumOfRooms);


        return cmd;
    }
    //--------------------------------------------------------------------------------------------------
    public int InsertVacation(Vacation vacation)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertVacationCommandSP("spInsertVacation_tal", con, vacation);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
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
    private SqlCommand CreateInsertVacationCommandSP(String spName, SqlConnection con, Vacation vacation)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@UserId", vacation.UserId);

        cmd.Parameters.AddWithValue("@FlatId", vacation.FlatId);

        cmd.Parameters.AddWithValue("@StartDate", vacation.StartDate);

        cmd.Parameters.AddWithValue("@EndDate", vacation.EndDate);


        return cmd;
    }
    //--------------------------------------------------------------------------------------------------

    public int InsertUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertAndUpdateUserCommandSP("spInsertUser_tal", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
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
    private SqlCommand CreateInsertAndUpdateUserCommandSP(String spName, SqlConnection con, User user)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure

        cmd.Parameters.AddWithValue("@firstName", user.FirstName);

        cmd.Parameters.AddWithValue("@familyName", user.FamilyName);

        cmd.Parameters.AddWithValue("@email", user.Email);

        cmd.Parameters.AddWithValue("@password", user.Password);

        cmd.Parameters.AddWithValue("@isActive", user.IsActive);

        cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);


        return cmd;
    }

    //--------------------------------------------------------------------------------------------------
    // Handel Update User & User's status
    //--------------------------------------------------------------------------------------------------
    public int UpdateUser(User user)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateInsertAndUpdateUserCommandSP("spUpdateUser_tal", con, user);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
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

    public int UpdateStatus(string email)
    {

        SqlConnection con;
        SqlCommand cmd;

        try
        {
            con = connect("myProjDB"); // create the connection
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
        }

        cmd = CreateUpdateUserStatusCommandSP("spUpdateUserStatus_tal", con, email);             // create the command

        try
        {
            int numEffected = cmd.ExecuteNonQuery(); // execute the command
            return numEffected;
        }
        catch (Exception ex)
        {
            // write to log
            throw (ex);
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

    private SqlCommand CreateUpdateUserStatusCommandSP(String spName, SqlConnection con, string email)
    {

        SqlCommand cmd = new SqlCommand(); // create the command object

        cmd.Connection = con;              // assign the connection to the command object

        cmd.CommandText = spName;      // can be Select, Insert, Update, Delete 

        cmd.CommandTimeout = 10;           // Time to wait for the execution' The default is 30 seconds

        cmd.CommandType = System.Data.CommandType.StoredProcedure; // the type of the command, can also be stored procedure


        cmd.Parameters.AddWithValue("@email", email);

        return cmd;
    }



}
