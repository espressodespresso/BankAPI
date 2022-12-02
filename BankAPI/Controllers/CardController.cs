using BankAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace BankAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CardController : Controller
{
    private CardRepository _cardRepository = new CardRepository();
    
    [HttpGet("{customerid}")]
    public IActionResult GetCard([FromRoute] int customerid)
    {
        try
        {
            var card = _cardRepository.GetCard(customerid);
            return Ok(card);
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpGet("{number}/{pin}")]
    public IActionResult ScanCard([FromRoute] long number, [FromRoute] int pin)
    {
        try
        {
            var customer = _cardRepository.ScanCard(number, pin);
            return Ok(customer);
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }

    [HttpPut("{customerid}")]
    public IActionResult UpdateActive([FromRoute] int customerid, [FromBody] bool active)
    {
        try
        {
            _cardRepository.UpdateActive(customerid, active);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return Problem(ex.Message);
        }
    }
}