namespace BankAPI;

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

    public User(string firstName, string lastName, string address, DateTime dob, DateTime lastActivity, int id)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.address = address;
        this.dob = dob;
        this.lastActivity = lastActivity;
        this.id = id;
    }
    
    public User() {}
}