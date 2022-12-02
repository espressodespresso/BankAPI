namespace BankApp.Models;

public class Customer : User
{
    private bool unique;
    private float salary;
    private float overdraftPercentage;

    public bool Unique
    {
        get => unique;
        set => unique = value;
    }

    public float Salary
    {
        get => salary;
        set => salary = value;
    }

    public float OverdraftPercentage
    {
        get => overdraftPercentage;
        set => overdraftPercentage = value;
    }
}