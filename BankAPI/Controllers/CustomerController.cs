using BankAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CustomerController : Controller
{
    private CustomerRepository _customerRepository = new CustomerRepository();

    [HttpGet("{customerid}")]
    public IActionResult GetCustomer(int customerid)
    {
        try
        {
            var customer = _customerRepository.GetCustomer(customerid);
            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("overdraftlimit/{customerid}")]
    public IActionResult GetOverdraftLimit(int customerid)
    {
        try
        {
            var overdraftLimit = _customerRepository.GetOverdraftLimit(customerid);
            return Ok(overdraftLimit);
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPost]
    public IActionResult CreateCustomer(Customer model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }

        try
        {
            new Customer(model.FirstName, model.LastName, model.Address, model.DOB
                , model.LastActivity, model.ID, model.Unique, model.Salary, model.OverdraftPercentage);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("statement/{customerid}")]
    public IActionResult GetStatement(int customerid)
    {
        return Ok(_customerRepository.CreateStatement(customerid));
    }

    [HttpPut("{customerid}")]
    public IActionResult UpdateValue(int customerid, [FromBody] string body)
    {
        try
        {
            var bodySplit = body.Split("/");
            string value = bodySplit[0];
            string insert = bodySplit[1];
            _customerRepository.UpdateValue(customerid, value, insert);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }
}