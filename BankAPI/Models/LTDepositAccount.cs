using MongoDB.Bson;

namespace BankAPI;

public class LTDepositAccount : Account
{
    public LTDepositAccount(int customerID)
    {
        try
        {
            Type = "LTDepositAccount";
            foreach (var i in Program.MongoDb.LoadAccounts<BsonDocument>("customerID", customerID))
            {
                if (i.GetValue("type").AsString == Type)
                {
                    ID = i.GetValue("id").AsInt32;
                    CustomerID = i.GetValue("customerID").AsInt32;
                    Balance = float.Parse(i.GetValue("balance").ToString());
                    Active = i.GetValue("active").AsBoolean;
                }
            }

        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Customer ID provided does not exist");
        }
    }
}