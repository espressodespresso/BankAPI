namespace BankApp.Models;

public class Card
{
    private int accountId;
    private long nunber;
    private bool active;

    public int accountID
    {
        get => accountId;
        set => accountId = value;
    }

    public long Number
    {
        get => nunber;
        set => nunber = value;
    }

    public bool Active
    {
        get => active;
        set => active = value;
    }
}