/**************************************************************************************
Solution: StorageLibBenchmark
Project: SQLite
Class: SQLiteDb
=============================

Implements ITestItem for PetaPoco using a SQLite db

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


namespace StorageLibBenchmark {


  [TableName("SampleTable")]
  [PrimaryKey("ID", autoIncrement = false)]
  public class PetaPocoTestItem: ITestItem {

    #region Properties
    //      ----------

    public const string TableName = "SampleTable";
    public const string PrimaryKeyName = "ID";


    [Ignore]
    public int Key {get { return ID; } set { ID = value; } }

    public int ID { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public decimal ADecimal { get; set; }

    public bool ABool { get; set; }
    #endregion


    #region Methods
    //      -------

    public override string ToString() => 
      $"ID: {ID}; Name: {Name}; Date: {Date:dd.MM.yyyy}; ADecimal: {ADecimal}; ABool: {ABool}";
    #endregion
  }

}
