using BankAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]

public class EmployeeController : Controller
{
    private EmployeeRepository _employeeRepository = new EmployeeRepository();

    [HttpGet("{employeeid}/{password}")]
    public IActionResult GetEmployee(string password, int employeeid)
    {
        try
        {
            var employee = _employeeRepository.GetEmployee(employeeid, password);
            return Ok(employee);
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }
    [HttpPost("{password}")]
    public IActionResult CreateEmployee(Employee model, string password)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid data");
        }

        try
        {
            new Employee(model.FirstName, model.LastName, model.Address, model.DOB
                , model.LastActivity, model.ID, password);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }
}