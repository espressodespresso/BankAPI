using System.Net.Http.Headers;
using System.Text.Json;
using BankApp.Models;

namespace BankApp.API;

public class AccountService
{
    private static string basePath(AccountType type)
    {
        string path = "/api/";
        switch (type)
        {
            case AccountType.DEPOSITACCOUNT:
                return path + "Account/";
            case AccountType.CURRENTACCOUNT:
                return path + "CurrentAccount/";
            case AccountType.LTDEPOSITACCOUNT:
                return path + "LTDepositAccount/";
        }

        return null;
    }
    
    // Get Request
    public static async Task<T> GetAccountAsync<T>(int customerid, AccountType type)
    {
        string path = basePath(type) + customerid;
        HttpResponseMessage response = await Program.client.GetAsync(path).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<T>();
        } 
        
        throw new ArgumentException(await response.Content.ReadAsStringAsync());
    }
    
    // Put Request
    public static async Task<String> UpdateBalanceAsync(int customerid, float withdrawAmount, AccountType type)
    {
        if (type == AccountType.LTDEPOSITACCOUNT)
        {
            throw new InvalidDataException("Please contact your system administrator");
        }
        string path = basePath(type) + customerid;
        HttpResponseMessage response = await Program.client.PutAsJsonAsync(
            path, withdrawAmount).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
    
    //Put Request
    public static async Task<String> TransferFundsAsync(int customerid, float amount, int recieveid, AccountType type)
    {
        string path = basePath(type) + customerid;
        string senderBody = amount + "/" + recieveid;
        HttpResponseMessage response = await Program.client.PutAsJsonAsync(
            path, senderBody).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
    
    //Put Request
    public static async Task<String> UpdateActiveAsync(int customerid, bool value, AccountType type)
    {
        string path = basePath(type) + "active/" + customerid;
        HttpResponseMessage response = await Program.client.PutAsJsonAsync(
            path, value).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
}