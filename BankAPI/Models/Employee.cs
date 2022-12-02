using MongoDB.Bson;

namespace BankAPI;

public class Employee : User
{
    private string password;

    public Employee(string firstName, string lastName, string address, DateTime dob, DateTime lastActivity, int id
        , string password) : base(firstName, lastName, address, dob, lastActivity, id)
    {
        try
        {
            Program.MongoDb.LoadRecordByID<BsonDocument>("Employees", "id", ID);
            throw new ArgumentException("Employee already exists!");
        }
        catch (Exception)
        {
            var employee = new BsonDocument()
            {
                { "id", ID },
                { "firstName", FirstName },
                { "lastName", LastName },
                { "address", address},
                { "dob", DOB },
                { "lastActivity", LastActivity },
                { "password", password }
            };
        
            Program.MongoDb.InsertRecord("Employees", employee);
        }
    }

    public Employee(int id, string password)
    {
        try
        {
            var document = Program.MongoDb.LoadRecordByID<BsonDocument>("Employees", "id", id);
            string accPassword = document.GetValue("password").AsString;
            if (accPassword == password)
            {
                FirstName = document.GetValue("firstName").AsString;
                LastName = document.GetValue("lastName").AsString;
                DOB = document.GetValue("dob").ToUniversalTime();
                LastActivity = document.GetValue("lastActivity").ToUniversalTime();
                Address = document.GetValue("address").AsString;
                ID = document.GetValue("id").AsInt32;
            }
            else
            {
                throw new ArgumentException("Incorrect password");
            }
        }
        catch (InvalidOperationException)
        {
            throw new ArgumentException("Employee ID provided does not exist");
        }
    }
    
    public Employee() {}
}