namespace BankApp.Models;

public class CurrentAccount : Account
{
    private float overdraftLimit;

    public float OverdraftLimit
    {
        get => overdraftLimit;
        set => overdraftLimit = value;
    }
}