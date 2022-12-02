namespace BankApp.Models;

public class Employee : User
{
    private string password;

    public string Password
    {
        get => password;
        set => password = value;
    }
}