using MongoDB.Bson;

namespace BankAPI;

public class Account
{
    private int id;
    private int customerId;
    private float balance;
    private bool active;
    private string type;

    public int ID
    {
        get => id;
        set => id = value;
    }

    public int CustomerID
    {
        get => customerId;
        set => customerId = value;
    }
    
    public float Balance
    {
        get => balance;
        set => balance = value;
    }

    public bool Active
    {
        get => active;
        set => active = value;
    }

    public string Type
    {
        get => type;
        set => type = value;
    }

    public Account(int customerID)
    {
        try
        {
            type = "DepositAccount";
            foreach (var i in Program.MongoDb.LoadAccounts<BsonDocument>("customerID", customerID))
            {
                if (i.GetValue("type").AsString == type)
                {
                    id = i.GetValue("id").AsInt32;
                    customerId = i.GetValue("customerID").AsInt32;
                    balance = float.Parse(i.GetValue("balance").ToString());
                    active = i.GetValue("active").AsBoolean;
                }
            }

        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Customer ID provided does not exist");
        }
    }
    
    public Account() {}
}