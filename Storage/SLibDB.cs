/**************************************************************************************
Solution: StorageLibBenchmark
Project: SLib
Class: SLibDB
=============================

Implements ITestDB for a StorageLib based db

Written in 2021 by Jürgpeter Huber 
Contact: https://github.com/PeterHuberSg/StorageLibBenchmark

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using StorageLib;
using System;
using System.Collections.Generic;
using System.IO;


namespace StorageLibBenchmark {

  /// <summary>
  /// Implements ITestDB for a StorageLib based db
  /// </summary>
  public class SLibDB: ITestDB {
    // StorageLib stores all data in RAM. The data can be accessed through DC.Data.SLibTestItems[key]. 

    #region Constructor
    //      -----------

    readonly DirectoryInfo dbDirectory;
    readonly CsvConfig csvConfig;


    /// <summary>
    /// Constructor, creates a new CsvData directory at dbPath and empty CSV files to store the data.
    /// </summary>
    public SLibDB(string dbPath) {
      dbDirectory = new (dbPath + @"\CsvData");
      if (dbDirectory.Exists) dbDirectory.Delete(recursive: true);

      dbDirectory.Create();

      csvConfig = new(dbDirectory.FullName);
      _ = new DC(csvConfig); //this creates empty CSV file
      DC.DisposeData();
    }
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Creates data context. Data can be accessed by using DC.Data.SLibTestItems[key]
    /// </summary>
    public void OpenDB() {
      _ = new DC(csvConfig);
    }


    /// <summary>
    /// Closes data context. Ensures that all data is written to the disk.
    /// </summary>
    public void CloseDB() {
      DC.DisposeData();
    }


    /// <summary>
    /// Creates and stores new SLibTestItem.
    /// </summary>
    public ITestItem Insert(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      return new SLibTestItem(name, date, aDecimal, aBool);
    }


    /// <summary>
    /// Updates the values of a SLibTestItem and stores them on the disk
    /// </summary>
    public void Update(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      DC.Data.SLibTestItems[key].Update(name, date, aDecimal, aBool);
    }


    /// <summary>
    /// Removes a SLibTestItem permanently for data context and disk
    /// </summary>
    public void Delete(int key) {
      DC.Data.SLibTestItems[key].Release();
    }


    /// <summary>
    /// Reads on particular SLibTestItem from data context
    /// </summary>
    public ITestItem? GetTestItem(int key) {
      return DC.Data.SLibTestItems[key];
    }


    /// <summary>
    /// Enumerates over all SLibTestItems stored in data context
    /// </summary>
    public IEnumerable<ITestItem> GetTestItems() {
      foreach (var testItem in DC.Data.SLibTestItems) {
        yield return testItem!;
      }
    }


    /// <summary>
    /// Starts a transaction
    /// </summary>
    public void BeginTransaction() {
      DC.Data.StartTransaction();
    }


    /// <summary>
    /// Completes a transaction
    /// </summary>
    public void CommitTransaction() {
      DC.Data.CommitTransaction();
    }


    /// <summary>
    /// Undoes a transaction
    /// </summary>
    public void RollbackTransaction() {
      DC.Data.RollbackTransaction();
    }
    #endregion
  }
}
