using MongoDB.Bson;
using MongoDB.Driver;

namespace BankAPI.Repositories;

internal class CardRepository
{
    public Card GetCard(int accountID)
    {
        try
        {
            Card card = new Card(accountID);
            return card;
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("I'm here");
            throw ex;
        }
    }

    public Customer ScanCard(long number, int pin)
    {
        try
        {
            var document = Program.MongoDb.LoadCard<BsonDocument>("Cards", "number", number);
            if (document.GetValue("active").AsBoolean)
            {
                if (document.GetValue("pin").AsInt32 == pin)
                {
                    return new Customer(document.GetValue("accountID").AsInt32);
                }

                throw new ArgumentException("Invalid pin");
            }

            throw new ArgumentException("Card is inactive");
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Card does not exist");
        }
    }

    public void UpdateActive(int customerid, bool active)
    {
        try
        {
            var document = Program.MongoDb.LoadCard<BsonDocument>("Cards", "accountID", customerid);
            var documentActive = document.GetValue("active").AsBoolean;
            if (documentActive && active)
            {
                throw new ArgumentException("Card is already active!");
            }

            if (!documentActive && !active)
            {
                throw new ArgumentException("Card is already deactivated!");
            }

            Program.MongoDb.UpdateBoolRecord<BsonDocument>("Cards", "accountID", customerid, "active", active);
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Card does not exist");
        }
    }
}