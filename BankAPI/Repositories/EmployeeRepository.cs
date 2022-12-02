namespace BankAPI.Repositories;

public class EmployeeRepository
{
    public Employee GetEmployee(int employeeid, string password)
    {
        try
        {
            var employee = new Employee(employeeid, password);
            return employee;
        }
        catch (ArgumentException ex)
        {
            throw ex;
        }
    }
}