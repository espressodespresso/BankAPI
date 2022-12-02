namespace BankApp.Models;

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
}