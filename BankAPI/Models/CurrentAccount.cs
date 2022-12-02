using MongoDB.Bson;

namespace BankAPI;

internal class CurrentAccount : Account
{
    private float overdraftLimit;

    public float OverdraftLimit
    {
        get => overdraftLimit;
        set => overdraftLimit = value;
    }

    public CurrentAccount(int customerID)
    {
        try
        {
            Type = "CurrentAccount";
            foreach (var i in Program.MongoDb.LoadAccounts<BsonDocument>("customerID", customerID))
            {
                if (i.GetValue("type").AsString == Type)
                {
                    ID = i.GetValue("id").AsInt32;
                    CustomerID = i.GetValue("customerID").AsInt32;
                    Balance = float.Parse(i.GetValue("balance").ToString());
                    Active = i.GetValue("active").AsBoolean;
                    overdraftLimit = float.Parse(i.GetValue("overdraftLimit").ToString());
                }
            }

        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Customer ID provided does not exist");
        }
    }
}