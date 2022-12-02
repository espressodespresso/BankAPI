using MongoDB.Bson;

namespace BankAPI;

internal class Card
{
    private int accountId;
    private long number;
    private bool active;

    public int AccountID
    {
        get => accountId;
        set => accountId = value;
    }

    public long Number
    {
        get => number;
        set => number = value;
    }
    
    public bool Active
    {
        get => active;
        set => active = value;
    }

    public int Pin
    {
        get => Program.MongoDb.LoadCard<BsonDocument>("Cards", "number", number).GetValue("pin").AsInt32;
        set => Program.MongoDb.UpdateCardRecord<BsonDocument>("Cards", "number", number, "pin", value);
    }

    // New card constructor
    public Card(int accountId, bool active)
    {
        long newNumber = new Random().NextInt64(1000000000000000, 10000000000000000);
        int newPin = new Random().Next(1000, 10000);
        var document = new BsonDocument()
        {
            { "accountID", accountId },
            { "number", newNumber },
            { "pin", newPin },
            { "active", active }
        };
        
        Program.MongoDb.InsertRecord("Cards", document);
    }

    public Card(int accountId)
    {
        try
        {
            var document = Program.MongoDb.LoadRecordByID<BsonDocument>("Cards", "accountID", accountId);
            this.accountId = document.GetValue("accountID").AsInt32;
            number = document.GetValue("number").AsInt64;
            active = document.GetValue("active").AsBoolean;
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("ID provided does not exist!");
        }
    }
}