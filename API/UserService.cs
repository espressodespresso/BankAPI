using BankApp.Models;

namespace BankApp.API;

public class UserService
{
    private static string basePath(UserType type)
    {
        string path = "/api/";
        switch (type)
        {
            case UserType.CUSTOMER:
                return path + "Customer/";
            case UserType.EMPLOYEE:
                return path + "Employee/";
        }

        return null;
    }
    
    // Get Request
    public static async Task<T> CustomerGetRequestAsync<T>(int customerid, string additionalPath)
    {
        string path = basePath(UserType.CUSTOMER) + additionalPath + customerid;
        HttpResponseMessage response = await Program.client.GetAsync(path).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        { 
            return await response.Content.ReadAsAsync<T>();
        } 
        
        throw new ArgumentException(await response.Content.ReadAsStringAsync());
    }
    
    // Get Request
    public static async Task<Employee> GetEmployeeAsync(int employeeid, string password)
    {
        string path = basePath(UserType.EMPLOYEE) + employeeid + "/" + password; 
        HttpResponseMessage response = await Program.client.GetAsync(path).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<Employee>();
        } 
        
        throw new ArgumentException(await response.Content.ReadAsStringAsync());
    }

    // Put Request
    public static async Task<String> UpdateCustomerValueAsync(int customerid, string value, string insert)
    {
        string path = basePath(UserType.CUSTOMER) + customerid;
        string senderBody = value + "/" + insert;
        HttpResponseMessage response = await Program.client.PutAsJsonAsync(
            path, senderBody).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
    
    // Post Request
    public static async Task<String> CreateCustomerAsync(Customer customer)
    {
        HttpResponseMessage response = await Program.client.PostAsJsonAsync(
            "/api/Customer", customer).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
    
    // Post Rwquest
    public static async Task<String> CreateEmployeeAsync(Employee employee, string password)
    {
        HttpResponseMessage response = await Program.client.PostAsJsonAsync(
            "/api/Employee/" + password, employee).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
}