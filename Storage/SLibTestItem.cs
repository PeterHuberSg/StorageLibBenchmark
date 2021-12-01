/**************************************************************************************
Solution: StorageLibBenchmark
Project: SLib
Class: SLibTestItem
=============================

When using StorageLib, classes and their properties are defined in a separate project
StorageLibDataModel. The StorageClassGenerator dll generates based on the data model the
class SLibTestItem.base.cs. In this file SLibTestItem.base.cs, one could add more properties
and methods, but that is not needed for StorageLibBenchmark.

Written in 2021 by Jürgpeter Huber 
Contact: https://github.com/PeterHuberSg/StorageLibBenchmark

To the extent possible under law, the author(s) have dedicated all copyright and 
related and neighboring rights to this software to the public domain worldwide under
the Creative Commons 0 license (details see COPYING.txt file, see also
<http://creativecommons.org/publicdomain/zero/1.0/>). 

This software is distributed without any warranty. 
**************************************************************************************/
using StorageLib;


namespace StorageLibBenchmark {


  public partial class SLibTestItem: IStorageItem<SLibTestItem>, ITestItem {


    #region Properties
    //      ----------

    #endregion


    #region Events
    //      ------

    #endregion


    #region Constructors
    //      ------------

    /// <summary>
    /// Called once the constructor has filled all the properties
    /// </summary>
    //partial void onConstruct() {
    //}


    /// <summary>
    /// Called once the cloning constructor has filled all the properties. Clones have no children data.
    /// </summary>
    //partial void onCloned(SLibTestItem clone) {
    //}


    /// <summary>
    /// Called once the CSV-constructor who reads the data from a CSV file has filled all the properties
    /// </summary>
    //partial void onCsvConstruct() {
    //}
    #endregion


    #region Methods
    //      -------

    /// <summary>
    /// Called before {ClassName}.Store() gets executed
    /// </summary>
    //partial void onStoring(ref bool isCancelled) {
    //}


    /// <summary>
    /// Called after SLibTestItem.Store() is executed
    /// </summary>
    //partial void onStored() {
    //}


    /// <summary>
    /// Called before SLibTestItem gets written to a CSV file
    /// </summary>
    //partial void onCsvWrite() {
    //}


    /// <summary>
    /// Called before any property of SLibTestItem is updated and before the HasChanged event gets raised
    /// </summary>
    //partial void onUpdating(
      //int key, 
      //string name, 
      //decimal aDecimal, 
      //bool aBool, 
      //ref bool isCancelled)
   //{
   //}


    /// <summary>
    /// Called after all properties of SLibTestItem are updated, but before the HasChanged event gets raised
    /// </summary>
    //partial void onUpdated(SLibTestItem old) {
    //}


    /// <summary>
    /// Called after an update for SLibTestItem is read from a CSV file
    /// </summary>
    //partial void onCsvUpdate() {
    //}


    /// <summary>
    /// Called before SLibTestItem.Release() gets executed
    /// </summary>
    //partial void onReleasing() {
    //}


    /// <summary>
    /// Called after SLibTestItem.Release() got executed
    /// </summary>
    //partial void onReleased() {
    //}


    /// <summary>
    /// Called after SLibTestItem.Disconnect() got executed
    /// </summary>
    //partial void onDisconnected() {
    //}


    /// <summary>
    /// Called after 'new SLibTestItem()' transaction is rolled back
    /// </summary>
    //partial void onRollbackItemNew() {
    //}


    /// <summary>
    /// Called after SLibTestItem.Store() transaction is rolled back
    /// </summary>
    //partial void onRollbackItemStored() {
    //}


    /// <summary>
    /// Called after SLibTestItem.Update() transaction is rolled back
    /// </summary>
    //partial void onRollbackItemUpdated(SLibTestItem oldSLibTestItem) {
    //}


    /// <summary>
    /// Called after SLibTestItem.Release() transaction is rolled back
    /// </summary>
    //partial void onRollbackItemRelease() {
    //}


    /// <summary>
    /// Updates returnString with additional info for a short description.
    /// </summary>
    //partial void onToShortString(ref string returnString) {
    //}


    /// <summary>
    /// Updates returnString with additional info for a short description.
    /// </summary>
    //partial void onToString(ref string returnString) {
    //}
    #endregion
  }
}
