# BankAPI
University Project, API built for Banking App Project using ASP .NET Web API type

## Instructions
* Add the BankAPI to your solution
* Add the API & Models folders to your existing solution
* Run the API project & have fun interacting with the service classes (See below on how to call)

## Important
All functions require a try catch statement, as they throw ArgumentExceptions if an issue occurs (See example below)
All functions require a you to add .Result to the end of each to get the actual model
```
try 
{
     Program.user = UserService.GetEmployeeAsync(int.Parse(inputId), inputPassword).Result;
     ...
}
catch (ArgumentException ex) 
{
     Console.WriteLine(ex);
}     
```

## Available Calls
> T is defined by the model classes Account(Deposit Account), CurrentAccount or LTDepositAccount. AccountTypes are either DEPOSITACCOUNT, CURRENTACCOUNT or LTDEPOSITACCOUNT. Both are required... sorry

#### AccountService

* **.GetAccountAsync<T>(int customerid, AccountType type)** -> Returns an object of whatever is specified as T (Get Request)
* **.UpdateBalanceAsync(int customerid, float withdrawAmount, AccountType type)** -> Withdrawls the amount specified, retuns a string (Put Request)
* **.TransferFundsAsync(int customerid, float amount, int recieveid, AccountType type)** -> Transfered amount specified between accounts, returns a string (Put Request)

#### CardService
* **.GetCardAsync(int customerid)** -> Returns a Card object (Get Request)
* **.GetCustomerScanAsync(long number, int pin)** -> Returns a Customer object (Get Request)
* **.UpdateActiveAsync(int customer, bool active)** -> Update whether a card is active (Put Request)

#### UserService
* **.CustomerGetRequestAsync<T>(int customerid, string additonalPath)** -> Multi-role function, see below (Get Request)
  * <Customer>(int ..., string "") -> Returns a Customer object
  * <float>(int ..., string "overdraftlimit/") -> Returns the overdraftlimit for the specified customer 
  * <List<Transaction>>(int ..., string "statement/"> -> Returns a list (maximum of 10) Transaction objects
* **.GetEmployeeAsync(int employeeid, string password)** -> Returns a Employee object (Get Request)
* **.UpdateCustomerValueAsync(int customerid, string value, string insert)** -> Updates a customers value in the database (See below for values) (Put Request)
  * firstName -> string
  * lastName -> string
  * dob -> DateTime
  * lastActivity -> DateTime
  * unique -> bool
  * salary -> float
  * overdraftPercentage -> float
* **.CreateCustomerAsync(Customer customer)** -> Creates a new customer in the database (Post Request)
* **.CreateEmployeeAsync(Employee employee)** -> Creates a new employee in the database (Post Request)
 
## To Do
Clean up classes ( Some are a mess )
