using MongoDB.Bson;

namespace BankAPI.Repositories;

internal class CurrentAccountRepository
{
    public bool Withdraw(float amount, int customerID)
    {
        try
        {
            var account = new CurrentAccount(customerID);
            if (account.Balance + account.OverdraftLimit - amount < 0)
            {
                return false;
            }

            account.Balance -= amount;
            new Transaction(account.ID, account.Type, "Withdraw", amount, DateTime.Now, account.Balance);
            Program.MongoDb.UpdateRecord<BsonDocument>("Accounts", "id", account.ID, "balance",
                account.Balance);
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
            var account = new CurrentAccount(customerID);
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
    
    public CurrentAccount GetAccount(int customerID)
    {
        try
        {
            CurrentAccount account = new CurrentAccount(customerID);
            return account;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }
    
    public string UpdateActive(int customerID, bool value)
    {
        try
        {
            CurrentAccount account = new CurrentAccount(customerID);
            if (account.Active && value)
            {
                throw new ArgumentException("Account is already activated!");
            }

            if (!account.Active && !value)
            {
                throw new ArgumentException("Account is already deactivated!");
            }

            Program.MongoDb.UpdateBoolRecord<BsonDocument>("Accounts", "id", account.ID, "active", value);
            return "Successfully updated active value";
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Account does not exist");
        }
    }
}