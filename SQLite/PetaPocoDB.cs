/**************************************************************************************
Solution: StorageLibBenchmark
Project: SQLite
Class: SQLiteDb
=============================

Implements ITestDB for PetaPoco using a SQLite db

Written in 2021 by Jürgpeter Huber 
Contact: https://github.com/PeterHuberSg/StorageLibBenchmark

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Data.SQLite;


namespace StorageLibBenchmark {


  /// <summary>
  /// Implements ITestDB for PetaPoco using a SQLite db
  /// </summary>
  public class PetaPocoDB: ITestDB {


    #region Constructor
    //      -----------

    readonly string dbFileName;
    SQLiteConnection? sQLiteConnection;
    Database? petaPocoDatabase;


    public PetaPocoDB(string dbPath) {
      dbFileName = dbPath + @"\database.db3";
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

    /// <summary>
    /// Opens a connection to PetaPoco Database 
    /// </summary>
    public void OpenDB() {
      sQLiteConnection = new SQLiteConnection($"Data Source={dbFileName};");
      sQLiteConnection.Open();
      petaPocoDatabase = new Database(sQLiteConnection);
    }


    /// <summary>
    /// Closes the connection to PetaPoco Database 
    /// </summary>
    public void CloseDB() {
      petaPocoDatabase!.Dispose();
      petaPocoDatabase = null;
      sQLiteConnection!.Close();
      sQLiteConnection = null;
      GC.Collect();
    }


    /// <summary>
    /// Inserts ITestItem into PetaPoco Database 
    /// </summary>
    public ITestItem Insert(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      var petaPocoTestItem = new PetaPocoTestItem { Key=key, Name=name, Date=date, ADecimal=aDecimal, ABool=aBool };
      petaPocoDatabase!.Insert(petaPocoTestItem);
      return petaPocoTestItem;
    }


    /// <summary>
    /// Overwrites the existing ITestItem with new values 
    /// </summary>
    public void Update(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      var petaPocoTestItem = new PetaPocoTestItem {Key=key, Name=name, Date=date, ADecimal=aDecimal, ABool=aBool};
      petaPocoDatabase!.Update(petaPocoTestItem);
    }


    /// <summary>
    /// Removes ITestItem from PetaPoco Database 
    /// </summary>
    public void Delete(int key) {
      petaPocoDatabase!.Delete<PetaPocoTestItem>(key);
    }


    /// <summary>
    /// returns ITestItem with Key==key, if one exists, otherwise null
    /// </summary>
    public ITestItem? GetTestItem(int key) {
      return petaPocoDatabase!.FirstOrDefault<PetaPocoTestItem>("WHERE Id = @0", key);
    }


    /// <summary>
    /// Enumerates over all ITestItems in PetaPoco Database 
    /// </summary>
    public IEnumerable<ITestItem> GetTestItems() {
      foreach (var testItem in petaPocoDatabase!.Query<PetaPocoTestItem>($"SELECT * FROM {PetaPocoTestItem.TableName}")) {
        yield return testItem;
      }
    }


    /// <summary>
    /// Starts a PetaPoco Database transaction
    /// </summary>
    public void BeginTransaction() {
      petaPocoDatabase!.BeginTransaction();
    }


    /// <summary>
    /// Completes a PetaPoco Database transaction
    /// </summary>
    public void CommitTransaction() {
      petaPocoDatabase!.CompleteTransaction();
    }


    /// <summary>
    /// Undoes a PetaPoco Database transaction
    /// </summary>
    public void RollbackTransaction() {
      petaPocoDatabase!.AbortTransaction();
    }
    #endregion
  }
}
