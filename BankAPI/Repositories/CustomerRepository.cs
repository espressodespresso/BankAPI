using MongoDB.Bson;

namespace BankAPI.Repositories;

internal class CustomerRepository
{
    public Customer GetCustomer(int customerID)
    {
        try
        {
            var customer = new Customer(customerID);
            return customer;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }

    public float GetOverdraftLimit(int customerID)
    {
        try
        {
            var customer = new Customer(customerID);
            return customer.Salary / 100 * customer.OverdraftPercentage;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }

    public List<Transaction> CreateStatement(int customerID)
    {
        var statement = Program.MongoDb.LoadStatement<BsonDocument>("accountID", customerID);
        List<Transaction> tStatementList = new List<Transaction>();
        foreach (var i in statement)
        {
            int id = i.GetValue("id").AsInt32;
            int accountid = i.GetValue("accountID").AsInt32;
            string accountType = i.GetValue("accountType").AsString;
            string type = i.GetValue("type").AsString;
            float amount = float.Parse(i.GetValue("amount").ToString());
            DateTime time = i.GetValue("time").ToUniversalTime();
            float balance = float.Parse(i.GetValue("balance").ToString());
            tStatementList.Add(new Transaction(id, accountid, accountType, type, amount, time, balance));

        }
        return tStatementList;
    }

    public void UpdateValue(int customerid, string value, string insert)
    {
        try
        {
            var customer = new Customer(customerid);
            switch (value)
            {
                case "firstName":
                {
                    Program.MongoDb.UpdateStringRecord<BsonDocument>("Customers", "id", customerid, "firstName",
                        insert);
                    break;
                }
                case "lastName":
                {
                    Program.MongoDb.UpdateStringRecord<BsonDocument>("Customers", "id", customerid, "lastName",
                        insert);
                    break;
                }
                case "dob":
                {
                    var insertParsed = DateTime.Parse(insert);
                    Program.MongoDb.UpdateDateTimeRecord<BsonDocument>("Customers", "id", customerid, "dob",
                        insertParsed);
                    break;
                }
                case "lastActivity":
                {
                    var insertParsed = DateTime.Parse(insert);
                    Program.MongoDb.UpdateDateTimeRecord<BsonDocument>("Customers", "id", customerid,
                        "lastActivity",
                        insertParsed);
                    break;
                }
                case "unique":
                {
                    var insertParsed = bool.Parse(insert);
                    Program.MongoDb.UpdateBoolRecord<BsonDocument>("Customers", "id", customerid, "unique",
                        insertParsed);
                    break;
                }
                case "salary":
                {
                    var insertParsed = float.Parse(insert);
                    Program.MongoDb.UpdateRecord<BsonDocument>("Customers", "id", customerid, "salary",
                        insertParsed);
                    break;
                }
                case "overdraftPercentage":
                {
                    var insertParsed = float.Parse(insert);
                    Program.MongoDb.UpdateRecord<BsonDocument>("Customers", "id", customerid,
                        "overdraftPercentage",
                        insertParsed);
                    break;
                }
                default:
                {
                    throw new ArgumentException("Invalid value");
                }
            }
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }
}