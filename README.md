# SalesRecords API

This API is designed to get all records and get records based on conditions. Pagination is done to limit the maximum number records returned to the user to 20.

## To get all the records
[localhost](https://localhost:7187/)/Sales/{page}

{page} => Page Number

## To get records based on Greater than or Lesser than condition on TotalProfit
[localhost](https://localhost:7187/)/Sales/totalprofit/{c}/{val}/{page}

c => '>' or '<'

val => The value

{page} => Page Number

## To get records between two values of TotalProfit
[localhost](https://localhost:7187/)/Sales/totalprofit/between/{val1}/{val2}/{page}

val1 => Lower limit

val2 => Upper limit

{page} => Page Number

## To get records which are Not Equal To a value
[localhost](https://localhost:7187/)/Sales/totalprofit/GetNE/{val}/{page}

val => The value

{page} => Page Number

