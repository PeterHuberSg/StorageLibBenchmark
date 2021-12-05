/**************************************************************************************
Solution: StorageLibBenchmark
Project: SQLite
Class: SQLiteDb
=============================

Implements ITestDB for a SQLite based db

Written in 2021 by Jürgpeter Huber 
Contact: https://github.com/PeterHuberSg/StorageLibBenchmark

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace StorageLibBenchmark {

  /// <summary>
  /// Implements ITestDB for a SQLite based db
  /// </summary>
  public class SQLiteDB: ITestDB {

    #region Constructor
    //      -----------

    readonly string dbFileName;


    /// <summary>
    /// Constructor, creates an empty database.db file in dbPath directory, containing an empty table
    /// for storing TestItems
    /// </summary>
    public SQLiteDB(string dbPath) {
      dbFileName = dbPath + @"\database.db";

      var connection = new SQLiteConnection($"DataSource={dbFileName};");
      connection.Open();

      var command = connection.CreateCommand();
      command.CommandText = "CREATE TABLE SampleTable(Id INTEGER PRIMARY KEY ASC, Name TEXT, Date TEXT, ADecimal REAL, ABool INT)";
      command.ExecuteNonQuery();

      connection.Close();
      GC.Collect();
    }
    #endregion


    #region Methods
    //      -------

    SQLiteConnection? connection;


    /// <summary>
    /// Creates connection to SQLite db
    /// </summary>
    public void OpenDB() {
      connection = new SQLiteConnection($"DataSource={dbFileName};");
      connection.Open();
    }


    /// <summary>
    /// Closes connection to SQLite db
    /// </summary>
    public void CloseDB() {
      connection!.Close();
      connection = null;
      insertCommand = null;
      updateCommand = null;
      deleteCommand = null;
      getTestItemCommand = null;
      getTestItemsCommand = null;
      GC.Collect();
    }


    SQLiteCommand? insertCommand;

    /// <summary>
    /// Creates a new TestItem and stores it in SQLite db
    /// </summary>
    public ITestItem Insert(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      if (insertCommand is null) {
        insertCommand = connection!.CreateCommand();
        insertCommand.CommandText = "INSERT INTO SampleTable(Id, Name, Date, ADecimal, ABool) " +
          "VALUES($Id, $Name, $Date, $ADecimal, $ABool);";

      }
      insertCommand.Parameters.Clear();
      insertCommand.Parameters.AddWithValue("$Id", key);
      insertCommand.Parameters.AddWithValue("$Name", name);
      insertCommand.Parameters.AddWithValue("$Date", $"{date:yyyy-MM-dd}");
      insertCommand.Parameters.AddWithValue("$ADecimal", aDecimal);
      insertCommand.Parameters.AddWithValue("$ABool", aBool);
      insertCommand.ExecuteNonQuery();
      return new TestItem(key, name, date, aDecimal, aBool);
    }


    SQLiteCommand? updateCommand;


    /// <summary>
    /// Updates a TestItem in SQLite db
    /// </summary>
    public void Update(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      if (updateCommand is null) {
        updateCommand = connection!.CreateCommand();
        updateCommand.CommandText = "UPDATE SampleTable SET Name=$Name, Date=$Date, ADecimal=$ADecimal, " +
          $"ABool = $ABool WHERE ID=$ID";
      }
      updateCommand.Parameters.Clear();
      updateCommand.Parameters.AddWithValue("$Name", name);
      updateCommand.Parameters.AddWithValue("$Date", $"{date:yyyy-MM-dd}");
      updateCommand.Parameters.AddWithValue("$ADecimal", aDecimal);
      updateCommand.Parameters.AddWithValue("$ABool", aBool);
      updateCommand.Parameters.AddWithValue("$Id", key);
      updateCommand.ExecuteNonQuery();
    }


    SQLiteCommand? deleteCommand;


    /// <summary>
    /// Removes a TestItem from SQLite db
    /// </summary>
    public void Delete(int key) {
      if (deleteCommand is null) {
        deleteCommand = connection!.CreateCommand();
        deleteCommand.CommandText = "DELETE FROM SampleTable WHERE ID=$ID";
      }
      deleteCommand.Parameters.Clear();
      deleteCommand.Parameters.AddWithValue("$Id", key);
      deleteCommand.ExecuteNonQuery();
    }


    SQLiteCommand? getTestItemCommand;


    /// <summary>
    /// Returns one particular TestItem from SQLite db
    /// </summary>
    public ITestItem? GetTestItem(int key) {
      if (getTestItemCommand is null) {
        getTestItemCommand = connection!.CreateCommand();
        getTestItemCommand.CommandText = "SELECT * FROM SampleTable WHERE Id=$Id";
      }
      getTestItemCommand.Parameters.Clear();
      getTestItemCommand.Parameters.AddWithValue("$Id", key);
      var datareader = getTestItemCommand.ExecuteReader();
      var col = 0;
      return new TestItem(
        datareader.GetInt32(col++), 
        datareader.GetString(col++),
        datareader.GetDateTime(col++),
        datareader.GetDecimal(col++), 
        datareader.GetBoolean(col++));
    }


    SQLiteCommand? getTestItemsCommand;


    /// <summary>
    /// Enumerates all TestItems from SQLite db
    /// </summary>
    public IEnumerable<ITestItem> GetTestItems() {
      if (getTestItemsCommand is null) {
        getTestItemsCommand = connection!.CreateCommand();
        getTestItemsCommand.CommandText = $"SELECT * FROM SampleTable";
      }
      var datareader = getTestItemsCommand.ExecuteReader();
      while (datareader.Read()) {
        var col = 0;
        yield return new TestItem(
          datareader.GetInt32(col++),
          datareader.GetString(col++),
          datareader.GetDateTime(col++),
          datareader.GetDecimal(col++),
          datareader.GetBoolean(col++));
      }
    }


    SQLiteTransaction? transaction;


    /// <summary>
    /// Starts a SQLite db transaction
    /// </summary>
    public void BeginTransaction() {
      transaction = connection!.BeginTransaction();
    }


    /// <summary>
    /// Completes a SQLite db transaction
    /// </summary>
    public void CommitTransaction() {
      transaction!.Commit();
      transaction = null;
    }


    /// <summary>
    /// Undoes a SQLite db transaction
    /// </summary>
    public void RollbackTransaction() {
      transaction!.Rollback();
      transaction = null;
    }
    #endregion
  }
}
