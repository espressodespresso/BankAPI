using BankAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CurrentAccountController : Controller
{
    private CurrentAccountRepository _currentAccountRepository = new CurrentAccountRepository();
        
    [HttpGet("{customerid}")]
    public IActionResult GetAccount(int customerid)
    {
        try
        {
            var account = _currentAccountRepository.GetAccount(customerid);
            return Ok(account);
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPut("{customerid}")]
    public IActionResult Withdraw([FromBody] float amount, int customerid)
    {
        try
        {
            if (_currentAccountRepository.Withdraw(amount, customerid))
            {
                return Ok("Withdrawn " + amount + " successfully");
            }

            return Problem("You do not have enough funds in your account!");
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }
    
    [HttpPut("transfer/{customerid}")]
    public IActionResult Transfer([FromBody] string body, int customerid)
    {
        var bodySplit = body.Split('/');
        float amount = float.Parse(bodySplit[0]);
        int recieveid = int.Parse(bodySplit[1]);
        try
        {
            if (_currentAccountRepository.Transfer(customerid, amount, recieveid))
            {
                return Ok("Transferred " + amount + " to account" + recieveid);
            }

            return Problem("You do not have enough funds in your account!");
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }
}