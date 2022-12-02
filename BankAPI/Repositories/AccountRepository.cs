using MongoDB.Bson;

namespace BankAPI.Repositories;

internal class AccountRepository
{
    public bool Withdraw(float amount, int customerID)
    {
        try
        {
            var account = new Account(customerID);
            if (account.Balance - amount < 0)
            {
                return false;
            }
            
            account.Balance -= amount;
            new Transaction(account.ID, account.Type, "Withdraw", amount, DateTime.Now, account.Balance);
            Program.MongoDb.UpdateRecord<BsonDocument>("Accounts","id", account.ID, "balance", account.Balance);
            return true;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }

    public bool Transfer(int customerID, float amount, int recieveID)
    {
        try
        {
            var account = new Account(customerID);
            Account recieveAccount = null;
            if (account.Balance - amount < 0)
            {
                return false;
            }
            
            if (customerID == recieveID)
            {
                throw new ArgumentException("You cannot transfer to this account");
            }

            foreach (var i in Program.MongoDb.LoadAccounts<BsonDocument>("customerID", customerID))
            {
                if (i.GetValue("id").AsInt32 == recieveID)
                {
                    switch (i.GetValue("type").AsString)
                    {
                        case "DepositAccount":
                            recieveAccount = new Account(customerID);
                            break;
                        case "CurrentAccount":
                            recieveAccount = new CurrentAccount(customerID);
                            break;
                        case "LTDepositAccount":
                            recieveAccount = new LTDepositAccount(customerID);
                            break;
                    }
                }
                else
                {
                    throw new ArgumentException("You cannot transfer to this account");
                }
            }

            account.Balance -= amount;
            recieveAccount.Balance += amount;
            new Transaction(account.ID, account.Type, "Transfer", -amount, DateTime.Now, account.Balance);
            new Transaction(recieveID, recieveAccount.Type, "Recieved", amount, DateTime.Now, recieveAccount.Balance);
            Program.MongoDb.UpdateRecord<BsonDocument>("Accounts", "id", account.ID, "balance",
                account.Balance);
            Program.MongoDb.UpdateRecord<BsonDocument>("Accounts", "id", recieveAccount.ID, "balance",
                recieveAccount.Balance);
            return true;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }

    public Account GetAccount(int customerID)
    {
        try
        {
            Account account = new Account(customerID);
            return account;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }
}