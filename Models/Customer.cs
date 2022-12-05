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

    public Customer(string firstName, string lastName, string address, DateTime dob, DateTime lastActivity, int id
        , bool unique, float salary, float overdraftPercentage)
    {
        FirstName = firstName;
        LastName = lastName;
        Address = address;
        DOB = dob;
        LastActivity = lastActivity;
        ID = id;
        Unique = unique;
        Salary = salary;
        OverdraftPercentage = overdraftPercentage;
    }
}