using System.Runtime.InteropServices;
using dadataStandartizer.Handlers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        JToken jTokenAddressInfo;
        try
        {
            jTokenAddressInfo = JArray.Parse(addressInfo).First;
        }
        catch (JsonReaderException)
        {
            var errorObj = JObject.Parse(addressInfo); // if we'll get error even here, it will be intercepted by middleware
            var status = errorObj["status"].ToString();
            var error = errorObj["error"].ToString();
            var message = errorObj["message"].ToString();

            throw new ExternalException($"Dadata exception. Status: {status}. Error: {error}. Message: {message}");
        }
        
        var resultField = jTokenAddressInfo["result"].ToString();
        return string.IsNullOrEmpty(resultField) ? StatusCode(418, "No information has been provided at this address") : Ok(jTokenAddressInfo.ToString());
    }
}