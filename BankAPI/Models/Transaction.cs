using MongoDB.Bson;

namespace BankAPI;

internal class Transaction
{
    private int id;
    private int accountID;
    private string accountType;
    private string type;
    private float amount;
    private DateTime time;
    private float balance;
    
    public int ID
    { 
        get => id; 
        set => id = value;
    }
        
    public int AccountID
    { 
        get => accountID; 
        set => accountID = value;
    }
    
    public string AccountType
    { 
        get => accountType; 
        set => accountType = value;
    }
    
    public string Type 
    {
        get => type;
        set => type = value;
    }
    
    public float Amount
    {
        get => amount; 
        set => amount = value;
    }
    
    public DateTime Time
    { 
        get => time; 
        set => time = value;
    }
    
    public float Balance
    { 
        get => balance; 
        set => balance = value;
    }
    
    public Transaction(int accountID, string accountType, string type, float amount, DateTime time, float balance)
    {
        id = new Random().Next(100000, 1000000);
        this.accountID = accountID;
        this.accountType = accountType;
        this.type = type;
        this.amount = amount;
        this.time = time;
        this.balance = balance;
        SaveToDB();
    }
    
    public Transaction(int id, int customerid, string accountType, string type, float amount, DateTime time, float balance)
    {
        this.id = id;
        accountID = customerid;
        this.accountType = accountType;
        this.type = type;
        this.amount = amount;
        this.time = time;
        this.balance = balance;
    }

    private void SaveToDB()
    {
        var transaction = new BsonDocument()
        {
            { "id", id },
            { "accountID", accountID },
            { "accountType", accountType },
            { "type", type },
            { "amount", amount },
            { "time", time },
            { "balance", balance }
        };
        
        Program.MongoDb.InsertRecord("Transactions", transaction);
    }
}