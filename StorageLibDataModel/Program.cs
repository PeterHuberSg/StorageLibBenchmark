using StorageLib;
using System;
using System.IO;

namespace StorageLibBenchmark {


  class Program {
    static void Main(string[] args) {
      //here are the .cs file(s) stored with the data model
      var sourceDirectory = new DirectoryInfo(Environment.CurrentDirectory).Parent!.Parent!.Parent!;
      //path to VS solution
      var solutionDirectory = sourceDirectory.Parent!;
      //path of the VS project where the created code should be generated
      var targetDirectoryPath = solutionDirectory.FullName + @"\Storage";

      _ = new StorageClassGenerator(
        sourceDirectoryString: sourceDirectory.FullName,
        targetDirectoryString: targetDirectoryPath,
        context: "DC"); //>Name of Context class, which gives static access to all data stored.
    }
  }
}
