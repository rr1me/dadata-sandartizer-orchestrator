using dadataStandartizer.Handlers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace dadataStandartizer.Controllers;

public class MainController : ControllerBase
{
    private readonly RequestHandler _requestHandler;

    public MainController(RequestHandler requestHandler)
    {
        _requestHandler = requestHandler;
    }

    [HttpGet("getinfo")]
    public async Task<IActionResult> GetInfo(string address)
    {
        if (string.IsNullOrEmpty(address))
            return BadRequest("You must provide address for standartization");

        var addressInfo = await _requestHandler.HandleAddress(address);

        var jTokenAddressInfo = JArray.Parse(addressInfo).First;
        var resultField = jTokenAddressInfo["result"].ToString();

        if (string.IsNullOrEmpty(resultField))
            return StatusCode(418, "No information has been provided at this address");
        
        return Ok(jTokenAddressInfo.ToString());
    }
}