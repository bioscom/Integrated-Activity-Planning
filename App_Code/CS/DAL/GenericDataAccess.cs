﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;


/// <summary>
/// Class contains generic data access functionality to be accessed from
/// the business tier
/// </summary>

public static class GenericDataAccess
{
    // static constructor
    static GenericDataAccess()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static OracleCommand CreateCommand()
    {
        // Obtain the database provider name
        string dataProviderName = AppConfiguration.dbProviderName;

        // Obtain the database connection string
        string connectionString = AppConfiguration.dbConnectionString;

        // Create a new data provider factory
        DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

        // Obtain a database-specific connection object
        OracleConnection conn = (OracleConnection)factory.CreateConnection();
        //OracleConnection conn = new OracleConnection(AppConfiguration.dbConnectionString);

        // Set the connection string
        conn.ConnectionString = connectionString;

        // Create a database-specific command object
        OracleCommand comm = conn.CreateCommand();

        comm.BindByName = true;

        // Set the command type to stored procedure
        comm.CommandType = CommandType.Text;

        // Return the initialized command object
        return comm;
    }

    /// Energy Components
    /// 
    //public static OracleCommand ECCreateCommand()
    //{
    //    // Obtain the database provider name
    //    string dataProviderName = AppConfiguration.dbProviderName;

    //    // Obtain the database connection string
    //    string connectionString = AppConfiguration.dbECConnectionString;

    //    // Create a new data provider factory
    //    DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

    //    // Obtain a database-specific connection object
    //    //OracleConnection conn = new OracleConnection();
    //    OracleConnection conn = (OracleConnection)factory.CreateConnection();

    //    // Set the connection string
    //    conn.ConnectionString = connectionString;

    //    // Create a database-specific command object
    //    OracleCommand comm = conn.CreateCommand();

    //    // Set the command type to stored procedure
    //    comm.CommandType = CommandType.Text;

    //    // Return the initialized command object
    //    return comm;
    //}

    public static DataTable ExecuteSelectCommand(OracleCommand command)
    {
        // The DataTable to be returned
        DataTable table = new DataTable();
        // Execute the command, making sure the connection gets closed in the
        // end
        try
        {
            // Open the data connection
            command.Connection.Open();

            // Execute the command and save the results in a DataTable
            OracleDataReader reader = command.ExecuteReader();
            table = new DataTable();
            table.Load(reader);

            // Close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            //appMonitor.logAppExceptions(ex);
            //Utilities.LogError(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        return table;
    }

    public static int ExecuteNonQuery(OracleCommand command)
    {
        // The number of affected rows
        int affectedRows = -1;
        // Execute the command making sure the connection gets closed in the end
        try
        {
            // Open the connection of the command
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            affectedRows = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            //// Log eventual errors and rethrow them
            //Utilities.LogError(ex);
            //throw;
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        // return the number of affected rows
        return affectedRows;
    }

    // execute a select command and return a single result as a string
    public static string ExecuteScalar(OracleCommand command)
    {
        // The value to be returned
        string value = "";
        // Execute the command making sure the connection gets closed in the end
        try
        {
            // Open the connection of the command
            command.Connection.Open();
            // Execute the command and get the number of affected rows
            value = command.ExecuteScalar().ToString();
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            //// Log eventual errors and rethrow them
            //Utilities.LogError(ex);
            throw;
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        // return the result
        return value;
    }

    //execute an update, delete, or insert command and return the number of affected rows
    public static DataSet ExecuteSelectCommand(string sql)
    {
        OracleConnection oCN = new OracleConnection();
        OracleCommand cmd = new OracleCommand();
        DataSet ds = new DataSet();
        //
        try
        {
            oCN.ConnectionString = AppConfiguration.dbConnectionString;
            oCN.Open();

            cmd = new OracleCommand(sql, oCN);
            OracleDataAdapter da = new OracleDataAdapter(cmd);
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        finally
        {
            if (cmd.Connection.State != ConnectionState.Closed)
            {
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
        }
        return ds;
    }

    // executes a command and returns the results as a DataTable object
    public static DataSet dsExecuteSelectCommand(OracleCommand command)
    {
        // The DataTable to be returned
        DataTable table = new DataTable();
        // The DataSet to be returned
        DataSet ds = new DataSet();
        // Execute the command, making sure the connection gets closed in the
        // end
        try
        {
            // Open the data connection
            command.Connection.Open();

            IDataReader MyReader;

            // Execute the command and save the results in a DataTable
            OracleDataReader reader = command.ExecuteReader();
            MyReader = reader;
            table = new DataTable();
            table.Load(reader);

            LoadOption ME = new LoadOption();

            ds.Load(MyReader, ME, table);

            // Close the reader
            reader.Close();
        }
        catch (Exception ex)
        {
            appMonitor.logAppExceptions(ex);
            //Utilities.LogError(ex);
            System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
        }
        finally
        {
            // Close the connection
            command.Connection.Close();
        }
        return ds;
    }






    //// executes a command and returns the results as a DataTable object
    //public static DataTable ExecuteSelectCommand(OracleCommand command)
    //{
    //    // The DataTable to be returned
    //    DataTable table = new DataTable();
    //    // Execute the command, making sure the connection gets closed in the
    //    // end
    //    try
    //    {
    //        // Open the data connection
    //        command.Connection.Open();

    //        // Execute the command and save the results in a DataTable
    //        DbDataReader reader = command.ExecuteReader();
    //        table = new DataTable();
    //        table.Load(reader);

    //        // Close the reader
    //        reader.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        //Utilities.LogError(ex);
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //    }
    //    finally
    //    {
    //        // Close the connection
    //        command.Connection.Close();
    //    }
    //    return table;
    //}

    //// creates and prepares a new OracleCommand object on a new connection
    //public static OracleCommand CreateCommand()
    //{
    //    // Obtain the database provider name
    //    string dataProviderName = AppConfiguration.DbProviderName;

    //    // Obtain the database connection string
    //    string connectionString = AppConfiguration.DbConnectionString;

    //    // Create a new data provider factory
    //    DbProviderFactory factory = DbProviderFactories.GetFactory(dataProviderName);

    //    // Obtain a database-specific connection object
    //    DbConnection conn = factory.CreateConnection();

    //    // Set the connection string
    //    conn.ConnectionString = connectionString;

    //    // Create a database-specific command object
    //    OracleCommand comm = conn.CreateCommand();

    //    // Set the command type to stored procedure
    //    comm.CommandType = CommandType.Text;

    //    // Return the initialized command object
    //    return comm;
    //}

    //// execute an update, delete, or insert command and return the number of affected rows
    //public static int ExecuteNonQuery(OracleCommand command)
    //{
    //    // The number of affected rows
    //    int affectedRows = -1;
    //    // Execute the command making sure the connection gets closed in the end
    //    try
    //    {
    //        // Open the connection of the command
    //        command.Connection.Open();
    //        // Execute the command and get the number of affected rows
    //        affectedRows = command.ExecuteNonQuery();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log eventual errors and rethrow them
    //        //Utilities.LogError(ex);
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //        throw;
    //    }
    //    finally
    //    {
    //        // Close the connection
    //        command.Connection.Close();
    //    }
    //    // return the number of affected rows
    //    return affectedRows;
    //}

    //// execute a select command and return a single result as a string
    //public static string ExecuteScalar(OracleCommand command)
    //{
    //    // The value to be returned
    //    string value = "";
    //    // Execute the command making sure the connection gets closed in the end
    //    try
    //    {
    //        // Open the connection of the command
    //        command.Connection.Open();
    //        // Execute the command and get the number of affected rows
    //        value = command.ExecuteScalar().ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        // Log eventual errors and rethrow them
    //        System.Diagnostics.Debug.Fail(ex.TargetSite.Name + "\n \n" + ex.StackTrace + "\n \n" + ex.Message.ToString());
    //        throw;
    //    }
    //    finally
    //    {
    //        // Close the connection
    //        command.Connection.Close();
    //    }
    //    // return the result
    //    return value;
    //}
}

