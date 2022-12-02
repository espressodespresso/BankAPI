using MongoDB.Bson;

namespace BankAPI;

public class Customer : User
{
    private bool unique;
    private float salary;
    private float overdraftPercentage;
    
    private CurrentAccount _currentAccount;
    private Account _simpleDeposit;
    private LTDepositAccount _ltDepositAccount;

    public bool Unique
    {
        get => unique;
        set => unique = value;
    }
    
    public float Salary
    {
        get => salary;
        set {
            if (value > 30000)
            {
                unique = true;
            }
            else
            {
                unique = false;
            }

            salary = value;
        }
    }

    public float OverdraftPercentage
    {
        get => overdraftPercentage;
        set => overdraftPercentage = value;
    }

    // Create Customer Constructor
    public Customer(string firstName, string lastName, string address, DateTime dob, DateTime lastActivity, int id
        , bool unique, float salary, float overdraftPercentage) : base(firstName, lastName, address, dob, lastActivity, id)
    {
        this.unique = unique;
        Salary = salary;
        this.overdraftPercentage = overdraftPercentage;
        try
        {
            Program.MongoDb.LoadRecordByID<BsonDocument>("Customers", "id", ID);
            throw new ArgumentException("Customer already exists!");
        }
        catch (Exception)
        {
            var customer = new BsonDocument()
            {
                { "id", ID },
                { "firstName", FirstName },
                { "lastName", LastName },
                { "dob", DOB },
                { "lastActivity", LastActivity },
                { "unique", unique },
                { "salary", salary },
                { "overdraftPercentage", overdraftPercentage }
            };

            Random random = new Random();

            var sDepositAccount = new BsonDocument()
            {
                { "id", random.Next(100000, 1000000) },
                { "customerID", ID },
                { "balance", 0.0f },
                { "active", true },
                { "type", "DepositAccount" }
            };
            
            var GetOverdraftLimit = salary / 100 * overdraftPercentage;
            var currentAccount = new BsonDocument()
            {
                { "id", random.Next(100000, 1000000) },
                { "customerID", ID },
                { "balance", 0.0f },
                { "active", false },
                { "overdraftLimit", GetOverdraftLimit },
                { "type", "CurrentAccount" }
            };

            var ltDepositAccount = new BsonDocument()
            {
                { "id", random.Next(100000, 1000000) },
                { "customerID", ID },
                { "balance", 0.0f },
                { "active", false },
                { "type", "LTDepositAccount" }
            };

            new Card(ID, true);
            Program.MongoDb.InsertRecord("Customers", customer);
            Program.MongoDb.InsertRecord("Accounts", sDepositAccount);
            Program.MongoDb.InsertRecord("Accounts", currentAccount);
            Program.MongoDb.InsertRecord("Accounts", ltDepositAccount);
        }
    }
    
    // Get Customer Constructor
    public Customer(int id)
    {
        ID = id;
        try
        {
            var document = Program.MongoDb.LoadRecordByID<BsonDocument>("Customers", "id", ID);
            FirstName = document.GetValue("firstName").AsString;
            LastName = document.GetValue("lastName").AsString;
            DOB = document.GetValue("dob").ToUniversalTime();
            LastActivity = document.GetValue("lastActivity").ToUniversalTime();
            unique = document.GetValue("unique").AsBoolean;
            salary = float.Parse(document.GetValue("salary").ToString());
            overdraftPercentage = float.Parse(document.GetValue("overdraftPercentage").ToString());
            _currentAccount = new CurrentAccount(ID);
            _simpleDeposit = new Account(ID);
            _ltDepositAccount = new LTDepositAccount(ID);
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Customer ID provided does not exist");
        }
    }
}