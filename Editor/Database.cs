/* Team Python          Last edit made by: Stephen Galyas           Created on: November 24, 2016         Last modified on: November 24, 2016            Published on: N/A
 * This class manages all connections to the database. All methods are static and may be called from anywhere in the system.
 * 
 * Public methods:
 *      CheckConnection()
 *      LogIn()
 *      
*/
using UnityEngine;
using System.Data;
using System.Collections;

/*
/// <summary>
/// This class manages connections to the database.
/// </summary>
public class Database : MonoBehaviour {

    /// <summary>
    /// The connection string for the database.
    /// </summary>
    private const string CONNSTRING = @"server=127.0.0.1;port=3306;database=test_data;uid=root;pwd=TeamPython";
    
    /// <summary>
    /// The connection to the database.
    /// </summary>
    private static MySqlConnection conn = null;


    /// <summary>
    /// This method checks whether or not the device can connect to the database.
    /// </summary>
    /// <returns>True if the connection is strong enough, otherwise false.</returns>
    public static string CheckConnection()
    {
        try // Try connecting to the database.
        {
            conn = new MySqlConnection();
            conn.ConnectionString = CONNSTRING;
            conn.Open();
            conn.Close();
            return System.String.Empty; // Connection successful, do not return an error message.
        }
        catch(System.Exception exc)
        {
            return exc.Message; // Error talking to the database. Return the error message.
        }
    } // Close CheckConnection().

    /// <summary>
    /// This method attempts to log in to the database.
    /// </summary>
    /// <param name="userName">The username of the account.</param>
    /// <param name="password">The password affiliated with the account.</param>
    /// <returns>True if the login information was correct, otherwise false.</returns>
    public static bool LogIn(string userName, string password)
    {
        //TODO: finish the login code once we have figured out how to connect to the database.

        // Execue the command and get the save data.
        conn = new MySqlConnection();
        conn.ConnectionString = CONNSTRING;
        conn.Open();

        MySqlCommand loginCommand = new MySqlCommand();
        loginCommand.CommandText = @"SELECT * FROM acct_info WHERE user_name=" + userName + " AND password=" + password + ";";
        loginCommand.CommandTimeout = 3000;
        loginCommand.CommandType = CommandType.Text;
        loginCommand.Connection = conn;

        MySqlDataReader data = loginCommand.ExecuteReader(); // Get the data from the database.
        if(data != null) // The database returned data.
        {
            bool status = UpdateXMLFiles(); // Update the account information and save data on the device.
            conn.Close();
            return status; // Return whether or not the XML files were successfully updated.
        }
        else // Did not get data from the database.
        {
            conn.Close();
            return false; // Return to the calling class.
        }
    } // Close LogIn().

    /// <summary>
    /// This method updates the XML files containing account information and save data on the device.
    /// </summary>
    /// <returns>True if successfully updated, otherwise false.</returns>
    private static bool UpdateXMLFiles()
    {
        //TODO: write the code to update the XML files once we have agreed on their schema.
        return false;
    } // Close UpdateXMLFiles().
}
*/
