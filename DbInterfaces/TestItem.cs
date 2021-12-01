/**************************************************************************************
Solution: StorageLibBenchmark
Project: DbDefinitions
Class: ITestItem
=============================

Sample implementation of ITtestItem

Written in 2021 by Jürgpeter Huber 
Contact: https://github.com/PeterHuberSg/StorageLibBenchmark

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using System;


namespace StorageLibBenchmark {

  /// <summary>
  /// Sample implementation of ITtestItem
  /// </summary>
  public class TestItem: ITestItem {

    #region Properties
    //      ----------

    /// <summary>
    /// Unique key identifying the test item
    /// </summary>
    public int Key { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public decimal ADecimal { get; set; }

    public bool ABool { get; set; }
    #endregion


    #region Constructor
    //      -----------

    public TestItem(int key, string name, DateTime date, decimal aDecimal, bool aBool) {
      Key = key;
      Name = name;
      Date = date;
      ADecimal = aDecimal;
      ABool = aBool;
    }
    #endregion


    #region Methods
    //      -------

    public override string ToString() => $"Key: {Key}; Name: {Name}; Date: {Date:dd.MM.yyyy}; ADecimal: {ADecimal}; ABool: {ABool}; ";
    #endregion
  }
}
