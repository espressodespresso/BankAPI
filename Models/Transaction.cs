using BankApp.API;

namespace BankApp.Models;

public class Transaction
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
}