namespace BankApp.Models;

public class User
{
    private string firstName;
    private string lastName;
    private string address;
    private DateTime dob;
    private DateTime lastActivity;
    private int id;

    public string FirstName
    {
        get => firstName;
        set => firstName = value;
    }
    
    public string LastName
    {
        get => lastName;
        set => lastName = value;
    }

    public string Address
    {
        get => address;
        set => address = value;
    }

    public DateTime DOB
    {
        get => dob;
        set => dob = value;
    }

    public DateTime LastActivity
    {
        get => lastActivity;
        set => lastActivity = value;
    }

    public int ID
    {
        get => id;
        set => id = value;
    }
}