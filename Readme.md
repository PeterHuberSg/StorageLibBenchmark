# Introduction
*StorageLibBenchmark* measures the performance of *StorageLib* and compares it to
other dbs like *SQLite*.

*StorageLib* is a single user C# library for lightening fast object oriented data storage in RAM and 
long term storage on a local hard disk. No database required. It's code is on:
[github.com/PeterHuberSg/StorageLib](https://github.com/PeterHuberSg/StorageLib)

*StorageLibBenchmark* has its own repository on github so that the *StorageLib* 
doesn't get too big.

*StorageLibBenchmark* is a C# console application. You can just run it to do 
a measurement on your computer and get the measurement results, which can look
something like that:

```
StorageLib         SQLite
  80.803     100%   220.020     272%  Create empty DB
   2.970     100%     8.006     270%  Open empty DB
   6.094     100%    16.545     271%  Add items with transaction
   1.200     100%  2714.105 226'119%  Add items without transaction
   0.247     100%     1.410     571%  Close filled DB
   8.864     100%     7.072      80%  Open filled DB
   0.868     100%    30.489   3'515%  Read Items
   2.145     100%     6.978     325%  Update items with transaction
   1.049     100%  2431.455 231'788%  Update items without transaction
   4.769     100%     2.061      43%  Close updated DB
   6.302     100%     4.767      76%  Open updated DB
   0.418     100%    16.934   4'054%  Calculate sum of aDecimal
   1.282     100%     6.136     479%  Delete items with transaction
   0.448     100%  2359.245 526'265%  Delete items without transaction
   1.617     100%     1.127      70%  Close DB
```

The first column shows the measurements for *StorageLib* in milliseconds.
The second column shows the measurements for *StorageLib* in percent.
The third column shows the measurements for SQLite in milliseconds.
The forth column shows the measurements for SQLite in percent compared to StorageLib.
The last column describes the database operation performed.

## Main takeaway for performance measurement

Compared to *SQLite*, *StorageLib* is 30 to 40 times faster querying data than 
*SQLite*. This is not surprising, because *StorageLib* stores all data in RAM as 
C# collection, while *SQLite* might need to access the disk if some data is 
missing in RAM.

Compared to *SQLite*, *StorageLib* is 2 to 5 times faster inserting, updating 
and deleting data.

Strange is the transaction behavior of *SQLite*. When no transacation is used, 
every insert or update statement creates its own transaction and *SQLite* 
becomes 2000 (!) times slower than *StorageLib*. In *StorageLib*, not using 
transaction is 2 to 5 time faster than using transactions. In *StorageLib*, one 
does not need to create a transaction for a single insert or update, 
*StorageLib* ensures that the data gets properly written to disk. A transaction 
is only used if one or several operations like insert, etc. might need to 
get undone for some reason later on.

##Installation
Clone first *StorageLib* from Github. See 
[github.com/PeterHuberSg/StorageLib/blob/master/Setup.md](https://github.com/PeterHuberSg/StorageLib/blob/master/Setup.md)
for details.

Clone *StorageLibBenchmark* into the same parent directory like *StorageLib*.
The directory structure should look like this:

```
ParentDirectory
  StorageLib
  StorageLibBenchmark
```