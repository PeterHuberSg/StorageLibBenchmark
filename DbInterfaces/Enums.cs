/**************************************************************************************
Solution: StorageLibBenchmark
Project: DbDefinitions
Class: Enums
=============================

Enumerates databases to be measured

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
  /// Enumerates databases to be measured
  /// </summary>
  public enum DBEnum {
    StorageLib,
    SQLite,
    PetaPoco,
    Count
  }

}
