/**************************************************************************************
Solution: StorageLibBenchmark
Project: DbDefinitions
Class: ITestDB
=============================

Interface defining the database operations to be tested

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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StorageLibBenchmark {

  /// <summary>
  /// Interface defining the database operations to be tested
  /// </summary>
  public interface ITestDB {

    /// <summary>
    /// Opens db, after which data can be written and read
    /// </summary>
    void OpenDB();

    /// <summary>
    /// Closes db, insures that all changes are written to the disk
    /// </summary>
    void CloseDB();

    /// <summary>
    /// Insert a new TestItem into db
    /// </summary>
    ITestItem Insert(int key, string name, DateTime date, decimal aDecimal, bool aBool);

    /// <summary>
    /// Updates the values or one particular TestItem
    /// </summary>
    void Update(int key, string name, DateTime date, decimal aDecimal, bool aBool);

    /// <summary>
    /// Remove one particular TestItem from db
    /// </summary>
    void Delete(int key);

    /// <summary>
    /// Returns one particular TestItem stored in db
    /// </summary>
    ITestItem? GetTestItem(int key);

    /// <summary>
    /// Enumerates over all TestItems stored in db
    /// </summary>
    IEnumerable<ITestItem> GetTestItems();

    /// <summary>
    /// Starts a db transaction
    /// </summary>
    void BeginTransaction();

    /// <summary>
    /// Completes a db transaction
    /// </summary>
    void CommitTransaction();

    /// <summary>
    /// Undoes a db transaction
    /// </summary>
    void RollbackTransaction();
  }
}
