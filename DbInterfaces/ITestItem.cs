/**************************************************************************************
Solution: StorageLibBenchmark
Project: DbDefinitions
Class: ITestItem
=============================

Interface defining the properties of a test item

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
  /// Interface defining the properties of a test item
  /// </summary>
  public interface ITestItem {
    /// <summary>
    /// Unique key identifying the test item
    /// </summary>
    public int Key { get; }
    public string Name { get; }
    public DateTime Date { get; }
    public decimal ADecimal { get; }
    public bool ABool { get;}
  }
}
