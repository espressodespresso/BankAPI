using BankApp.Models;

namespace BankApp.API;

public class CardService
{
    private static string basePath = "/api/Card/";
    
    // Get Request
    public static async Task<Card> GetCardAsync(int customerid)
    {
        string path = basePath + customerid;
        HttpResponseMessage response = await Program.client.GetAsync(path).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<Card>();
        }
        
        throw new ArgumentException(await response.Content.ReadAsStringAsync());
    }
    
    // Get Request
    public static async Task<Customer> GetCustomerScanAsync(long number, int pin)
    {
        string path = basePath + number + "/" + pin;
        HttpResponseMessage response = await Program.client.GetAsync(path).ConfigureAwait(false);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsAsync<Customer>();
        }
      
        throw new ArgumentException(await response.Content.ReadAsStringAsync());
    }
    
    // Put Request
    public static async Task<String> UpdateActiveAsync(int customerid, bool active)
    {
        string path = basePath + customerid;
        HttpResponseMessage response = await Program.client.GetAsync(path).ConfigureAwait(false);
        string responseBody = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return responseBody;
        }

        throw new ArgumentException(responseBody);
    }
}