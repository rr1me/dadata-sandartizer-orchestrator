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
        // var resultField = JArray.Parse("[\n  {\n    \"source\": \"350000, Краснодарский край, г Краснодар\",\n    \"result\": \"null\",\n    \"postal_code\": \"350000\",\n    \"country\": \"Россия\",\n    \"country_iso_code\": \"RU\",\n    \"federal_district\": \"Южный\",\n    \"region_fias_id\": \"d00e1013-16bd-4c09-b3d5-3cb09fc54bd8\",\n    \"region_kladr_id\": \"2300000000000\",\n    \"region_iso_code\": \"RU-KDA\",\n    \"region_with_type\": \"Краснодарский край\",\n    \"region_type\": \"край\",\n    \"region_type_full\": \"край\",\n    \"region\": \"Краснодарский\",\n    \"area_fias_id\": null,\n    \"area_kladr_id\": null,\n    \"area_with_type\": null,\n    \"area_type\": null,\n    \"area_type_full\": null,\n    \"area\": null,\n    \"city_fias_id\": \"7dfa745e-aa19-4688-b121-b655c11e482f\",\n    \"city_kladr_id\": \"2300000100000\",\n    \"city_with_type\": \"г Краснодар\",\n    \"city_type\": \"г\",\n    \"city_type_full\": \"город\",\n    \"city\": \"Краснодар\",\n    \"city_area\": null,\n    \"city_district_fias_id\": null,\n    \"city_district_kladr_id\": null,\n    \"city_district_with_type\": null,\n    \"city_district_type\": null,\n    \"city_district_type_full\": null,\n    \"city_district\": null,\n    \"settlement_fias_id\": null,\n    \"settlement_kladr_id\": null,\n    \"settlement_with_type\": null,\n    \"settlement_type\": null,\n    \"settlement_type_full\": null,\n    \"settlement\": null,\n    \"street_fias_id\": null,\n    \"street_kladr_id\": null,\n    \"street_with_type\": null,\n    \"street_type\": null,\n    \"street_type_full\": null,\n    \"street\": null,\n    \"house_fias_id\": null,\n    \"house_kladr_id\": null,\n    \"house_cadnum\": null,\n    \"house_type\": null,\n    \"house_type_full\": null,\n    \"house\": null,\n    \"block_type\": null,\n    \"block_type_full\": null,\n    \"block\": null,\n    \"entrance\": null,\n    \"floor\": null,\n    \"flat_fias_id\": null,\n    \"flat_cadnum\": null,\n    \"flat_type\": null,\n    \"flat_type_full\": null,\n    \"flat\": null,\n    \"flat_area\": null,\n    \"square_meter_price\": null,\n    \"flat_price\": null,\n    \"postal_box\": null,\n    \"fias_id\": \"7dfa745e-aa19-4688-b121-b655c11e482f\",\n    \"fias_code\": \"23000001000000000000000\",\n    \"fias_level\": \"4\",\n    \"fias_actuality_state\": \"0\",\n    \"kladr_id\": \"2300000100000\",\n    \"capital_marker\": \"2\",\n    \"okato\": \"03401000000\",\n    \"oktmo\": \"03701000001\",\n    \"tax_office\": \"2300\",\n    \"tax_office_legal\": \"2300\",\n    \"timezone\": \"UTC+3\",\n    \"geo_lat\": \"45.0401604\",\n    \"geo_lon\": \"38.9759647\",\n    \"beltway_hit\": null,\n    \"beltway_distance\": null,\n    \"qc_geo\": 4,\n    \"qc_complete\": 3,\n    \"qc_house\": 10,\n    \"qc\": 0,\n    \"unparsed_parts\": null,\n    \"metro\": null\n  }\n]").First["result"].ToString();

        if (string.IsNullOrEmpty(resultField))
            return StatusCode(418, "No information has been provided at this address");
        
        return Ok(jTokenAddressInfo.ToString());
    }
}