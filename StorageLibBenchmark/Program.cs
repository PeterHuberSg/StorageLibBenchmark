/**************************************************************************************
Solution: StorageLibBenchmark
Project: StorageLibBenchmark
Class: Program
=============================

This console program measures the performance of StorageLib and SQLite db

It loops over each database type supported

For each database it executes insert, update, delect and select statements and stores how much time that needed.

In the end, it produces a table comparing the various databases and showing how much time each database nedded for
each operation.

Pragram deals only with ITestDB which store ITestItem. Each db type has its own project which maps ITestDB 
methods to database specific commands.

Written in 2021 by Jürgpeter Huber 
Contact: https://github.com/PeterHuberSg/StorageLibBenchmark

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;


namespace StorageLibBenchmark {

  /// <summary>
  /// Console Main program
  /// </summary>
  class Program {

    #region Configuration data
    //      ------------------

    const int testDataCount = 100000;

    /// <summary>
    /// enumeration of all measurements taken
    /// </summary>
    public enum ResultEnum {
      createEmptyDB,
      openEmptyDB,
      addItemsWithTransaction,
      addItemsWithoutTransaction,
      closeInsertedDB,
      openInsertedDB2,
      readItems,
      updateItemsWithTransaction,
      updateItemsWithoutTransaction,
      closeUpdatedDB,
      openUpdatedDB2,
      calculateSumOfADecimal,
      deleteItemsWithTransaction,
      deleteItemsWithoutTransaction,
      closeDeletedDB,
      Count
    }


    static readonly string[] resultTexts = {
      "Create empty DB",
      "Open empty DB",
      "Add items with transaction",
      "Add items without transaction",
      "Close filled DB",
      "Open filled DB",
      "Read Items",
      "Update items with transaction",
      "Update items without transaction",
      "Close updated DB",
      "Open updated DB",
      "Calculate sum of aDecimal",
      "Delete items with transaction",
      "Delete items without transaction",
      "Close DB",
    };
    #endregion


    #region Constructor
    //      -----------

    //measurement results
    static readonly TimeSpan[,] results = new TimeSpan[(int)DBEnum.Count, (int)ResultEnum.Count];
    static DBEnum dBEnum;


    static void Main(string[] args) {
      /*
      structure of Main:
      loop over all db types
        loop over all measurements
          store time needed in results

      display results in table
      */

      Console.WriteLine("StorageLib Benchmark");
      Console.WriteLine("====================");
      Console.WriteLine();
      //create empty directory for databases
      var testDataPath = Environment.CurrentDirectory + @"\DbTestData";
      Console.WriteLine("Test data directory: " + testDataPath);
      var testDataDirectory = new DirectoryInfo(testDataPath);
      Console.WriteLine();
      if (testDataDirectory.Exists) testDataDirectory.Delete(recursive: true);
      
      testDataDirectory.Create();

      //loop through different db software libraries
      ITestDB testDb;
      for (dBEnum = 0; dBEnum < DBEnum.Count; dBEnum++) {

        switch (dBEnum) {
        case DBEnum.PetaPoco:
          Console.WriteLine("PetaPoco with SQLite");
          Console.WriteLine("--------------------");
          break;
        case DBEnum.StorageLib:
          Console.WriteLine("StorageLib");
          Console.WriteLine("----------");
          break;
        case DBEnum.SQLite:
          Console.WriteLine("SQLite");
          Console.WriteLine("------");
          break;
        default:
          throw new NotSupportedException();
        }
        Console.WriteLine();

        traceStart("Create a new database with an empty table for test data");
        GC.Collect();
        testDb = dBEnum switch {
          DBEnum.PetaPoco => new PetaPocoDB(testDataPath),
          DBEnum.StorageLib => new SLibDB(testDataPath),
          DBEnum.SQLite => new SQLiteDB(testDataPath),
          _ => throw new NotSupportedException(),
        };

        traceStart("Open DB");
        testDb.OpenDB();

        traceStart($"add {testDataCount} test items to db with transaction");
        var aDecimal = 0m;
        var date = DateTime.Now.Date;
        var expectedDate = date;
        const decimal decIncrement = 0.00001m;
        testDb.BeginTransaction();
        for (int i = 0; i < testDataCount; i++) {
          testDb.Insert(key: i, name: i.ToString(), date: date, aDecimal: aDecimal, aBool: i%2==0);
          aDecimal += decIncrement;
          date = date.AddDays(1);
        }
        testDb.CommitTransaction();

        traceStart($"add {testDataCount} test items to db without transaction");
        for (int i = testDataCount; i < 2*testDataCount; i++) {
          testDb.Insert(key: i, name: i.ToString(), date: date, aDecimal: aDecimal, aBool: i%2==0);
          aDecimal += decIncrement;
          date = date.AddDays(1);
        }

        //close and open db
        traceStart("Close filled DB");
        testDb.CloseDB();
        traceStart("Open filled DB");
        testDb.OpenDB();

        traceStart($"Read {2*testDataCount} testItems");
        date = expectedDate;
        aDecimal = 0m;
        foreach (var testItem in testDb.GetTestItems()) {
          if (testItem.ADecimal!=aDecimal) throw new Exception();
          if (testItem.Date!=date) throw new Exception();

          aDecimal += decIncrement;
          date = date.AddDays(1);
        }

        traceStart($"update {testDataCount} test items to db with transaction");
        aDecimal = 1m;
        expectedDate = date = expectedDate.AddYears(1);
        testDb.BeginTransaction();
        for (int i = 0; i < testDataCount; i++) {
          testDb.Update(key: i, name: i.ToString() + '_', date: date, aDecimal: aDecimal, aBool: i%2==1);
          aDecimal += decIncrement;
          date = date.AddDays(1);
        }
        testDb.CommitTransaction();

        traceStart($"update {testDataCount} test items to db without transaction");
        for (int i = testDataCount; i < 2*testDataCount; i++) {
          testDb.Update(key: i, name: i.ToString() + '_', date: date, aDecimal: aDecimal, aBool: i%2==1);
          aDecimal += decIncrement;
          date = date.AddDays(1);
        }

        //close and open db
        traceStart("Close updated DB");
        testDb.CloseDB();
        traceStart("Open updated DB");
        testDb.OpenDB();

        traceStart($"calculate sum of aDecimal");
        var aDecimalSum = testDb.GetTestItems().Sum(ti => ti.ADecimal);
        if (aDecimalSum!=testDataCount*(1+aDecimal-decIncrement)) throw new Exception();

        traceStart($"delete {testDataCount} test items from db with transaction");
        testDb.BeginTransaction();
        for (int i = 0; i < testDataCount; i++) {
          testDb.Delete(key: i);
        }
        testDb.CommitTransaction();

        traceStart($"delete {testDataCount} test items from db without transaction");
        for (int i = testDataCount; i < 2*testDataCount; i++) {
          testDb.Delete(key: i);
        }

        traceStart("Final close DB");
        testDb.CloseDB();
        traceStop();
        Console.WriteLine();
      }

      //display result table
      var sb = new StringBuilder();
      sb.AppendLine($"Iterations: 2*{testDataCount}");
      sb.AppendLine("StorageLib\t\tSQLite\t\tPetaPoco\t\tActivity");
      Console.WriteLine($"Iterations: 2*{testDataCount}");
      Console.WriteLine();
      Console.WriteLine("StorageLib            SQLite            PetaPoco");
      Console.WriteLine("      ms                  ms                  ms");
      for (int resultIndex = 0; resultIndex < (int)ResultEnum.Count; resultIndex++) {
        for (int dbIndex = 0; dbIndex < (int)DBEnum.Count; dbIndex++) {
          var result = results[dbIndex, resultIndex].TotalMilliseconds;
          var percent100 = results[0, resultIndex].TotalMilliseconds;
          var percent = result /percent100 * 100;
          Console.Write($"{result, 8:0.000} {percent, 7:N0}% | ");
          sb.Append($"{result:N2}\t{percent:N0}\t");
        }
        Console.WriteLine(resultTexts[resultIndex]);
        sb.AppendLine(resultTexts[resultIndex]);
        if (resultIndex==(int)ResultEnum.createEmptyDB ||
          resultIndex==(int)ResultEnum.closeInsertedDB ||
          resultIndex==(int)ResultEnum.closeUpdatedDB ||
          resultIndex==(int)ResultEnum.closeDeletedDB) 
        {
          Console.WriteLine();
        }
      }
      var s = sb.ToString();
    }
    #endregion


    #region Methods
    //      -------

    static Stopwatch stopwatch = new Stopwatch();
    static (int Left,int Top) cursorPosition;
    static string lastLine;


    private static void traceStart(string line) {
      if (stopwatch.IsRunning) {
        traceStop();
      }

      cursorPosition = Console.GetCursorPosition();
      Console.SetCursorPosition(10, cursorPosition.Top);
      Console.WriteLine(line);
      cursorPosition = Console.GetCursorPosition();
      lastLine = line;
      stopwatch.Restart();
    }


    static ResultEnum resultEnum;


    private static void traceStop() {
      stopwatch.Stop();
      var cursorPositionNew = Console.GetCursorPosition();
      Console.SetCursorPosition(0, cursorPosition.Top-1);
      Console.WriteLine(stopwatch.Elapsed.TotalSeconds.ToString("N6") + ' ' + lastLine);
      Console.SetCursorPosition(cursorPositionNew.Left, cursorPositionNew.Top);
      results[(int)dBEnum, (int)resultEnum++] = stopwatch.Elapsed;
      if (resultEnum==ResultEnum.Count) resultEnum = 0;

    }
    #endregion
  }
}
