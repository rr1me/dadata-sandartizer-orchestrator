using System.Text;
using Newtonsoft.Json;

namespace dadataStandartizer.Handlers;

public class RequestHandler
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly IConfiguration _config;
    private readonly ILogger<RequestHandler> _logger;

    public RequestHandler(IHttpClientFactory clientFactory, IConfiguration config, ILogger<RequestHandler> logger)
    {
        _clientFactory = clientFactory;
        _config = config;
        _logger = logger;
    }

    public async Task<string> HandleAddress(string address)
    {
        _logger.LogInformation("Handling address: " + address);
        // var responseBody = "[{\"source\":\"vcx\",\"result\":null,\"postal_code\":null,\"country\":null,\"country_iso_code\":null,\"federal_district\":null,\"region_fias_id\":null,\"region_kladr_id\":null,\"region_iso_code\":null,\"region_with_type\":null,\"region_type\":null,\"region_type_full\":null,\"region\":null,\"area_fias_id\":null,\"area_kladr_id\":null,\"area_with_type\":null,\"area_type\":null,\"area_type_full\":null,\"area\":null,\"city_fias_id\":null,\"city_kladr_id\":null,\"city_with_type\":null,\"city_type\":null,\"city_type_full\":null,\"city\":null,\"city_area\":null,\"city_district_fias_id\":null,\"city_district_kladr_id\":null,\"city_district_with_type\":null,\"city_district_type\":null,\"city_district_type_full\":null,\"city_district\":null,\"settlement_fias_id\":null,\"settlement_kladr_id\":null,\"settlement_with_type\":null,\"settlement_type\":null,\"settlement_type_full\":null,\"settlement\":null,\"street_fias_id\":null,\"street_kladr_id\":null,\"street_with_type\":null,\"street_type\":null,\"street_type_full\":null,\"street\":null,\"house_fias_id\":null,\"house_kladr_id\":null,\"house_cadnum\":null,\"house_type\":null,\"house_type_full\":null,\"house\":null,\"block_type\":null,\"block_type_full\":null,\"block\":null,\"entrance\":null,\"floor\":null,\"flat_fias_id\":null,\"flat_cadnum\":null,\"flat_type\":null,\"flat_type_full\":null,\"flat\":null,\"flat_area\":null,\"square_meter_price\":null,\"flat_price\":null,\"postal_box\":null,\"fias_id\":null,\"fias_code\":null,\"fias_level\":\"-1\",\"fias_actuality_state\":\"0\",\"kladr_id\":null,\"capital_marker\":\"0\",\"okato\":null,\"oktmo\":null,\"tax_office\":null,\"tax_office_legal\":null,\"timezone\":null,\"geo_lat\":null,\"geo_lon\":null,\"beltway_hit\":null,\"beltway_distance\":null,\"qc_geo\":5,\"qc_complete\":1,\"qc_house\":10,\"qc\":1,\"unparsed_parts\":\"ВСКС\",\"metro\":null}]";
        var responseBody =
            "[\n    {\n        \"source\": \"мск сухонска 11/-89\",\n        \"result\": \"г Москва, ул Сухонская, д 11, кв 89\",\n        \"postal_code\": \"127642\",\n        \"country\": \"Россия\",\n        \"country_iso_code\": \"RU\",\n        \"federal_district\": \"Центральный\",\n        \"region_fias_id\": \"0c5b2444-70a0-4932-980c-b4dc0d3f02b5\",\n        \"region_kladr_id\": \"7700000000000\",\n        \"region_iso_code\": \"RU-MOW\",\n        \"region_with_type\": \"г Москва\",\n        \"region_type\": \"г\",\n        \"region_type_full\": \"город\",\n        \"region\": \"Москва\",\n        \"area_fias_id\": null,\n        \"area_kladr_id\": null,\n        \"area_with_type\": null,\n        \"area_type\": null,\n        \"area_type_full\": null,\n        \"area\": null,\n        \"city_fias_id\": null,\n        \"city_kladr_id\": null,\n        \"city_with_type\": null,\n        \"city_type\": null,\n        \"city_type_full\": null,\n        \"city\": null,\n        \"city_area\": \"Северо-восточный\",\n        \"city_district_fias_id\": null,\n        \"city_district_kladr_id\": null,\n        \"city_district_with_type\": \"р-н Северное Медведково\",\n        \"city_district_type\": \"р-н\",\n        \"city_district_type_full\": \"район\",\n        \"city_district\": \"Северное Медведково\",\n        \"settlement_fias_id\": null,\n        \"settlement_kladr_id\": null,\n        \"settlement_with_type\": null,\n        \"settlement_type\": null,\n        \"settlement_type_full\": null,\n        \"settlement\": null,\n        \"street_fias_id\": \"95dbf7fb-0dd4-4a04-8100-4f6c847564b5\",\n        \"street_kladr_id\": \"77000000000283600\",\n        \"street_with_type\": \"ул Сухонская\",\n        \"street_type\": \"ул\",\n        \"street_type_full\": \"улица\",\n        \"street\": \"Сухонская\",\n        \"house_fias_id\": \"5ee84ac0-eb9a-4b42-b814-2f5f7c27c255\",\n        \"house_kladr_id\": \"7700000000028360004\",\n        \"house_cadnum\": \"77:02:0004008:1017\",\n        \"house_type\": \"д\",\n        \"house_type_full\": \"дом\",\n        \"house\": \"11\",\n        \"block_type\": null,\n        \"block_type_full\": null,\n        \"block\": null,\n        \"entrance\": null,\n        \"floor\": null,\n        \"flat_fias_id\": \"f26b876b-6857-4951-b060-ec6559f04a9a\",\n        \"flat_cadnum\": \"77:02:0004008:4143\",\n        \"flat_type\": \"кв\",\n        \"flat_type_full\": \"квартира\",\n        \"flat\": \"89\",\n        \"flat_area\": \"34.6\",\n        \"square_meter_price\": \"239953\",\n        \"flat_price\": \"8302374\",\n        \"postal_box\": null,\n        \"fias_id\": \"f26b876b-6857-4951-b060-ec6559f04a9a\",\n        \"fias_code\": \"77000000000000028360004\",\n        \"fias_level\": \"9\",\n        \"fias_actuality_state\": \"0\",\n        \"kladr_id\": \"7700000000028360004\",\n        \"capital_marker\": \"0\",\n        \"okato\": \"45280583000\",\n        \"oktmo\": \"45362000\",\n        \"tax_office\": \"7715\",\n        \"tax_office_legal\": \"7715\",\n        \"timezone\": \"UTC+3\",\n        \"geo_lat\": \"55.8782557\",\n        \"geo_lon\": \"37.65372\",\n        \"beltway_hit\": \"IN_MKAD\",\n        \"beltway_distance\": null,\n        \"qc_geo\": 0,\n        \"qc_complete\": 0,\n        \"qc_house\": 2,\n        \"qc\": 0,\n        \"unparsed_parts\": null,\n        \"metro\": [\n            {\n                \"distance\": 1.1,\n                \"line\": \"Калужско-Рижская\",\n                \"name\": \"Бабушкинская\"\n            },\n            {\n                \"distance\": 1.2,\n                \"line\": \"Калужско-Рижская\",\n                \"name\": \"Медведково\"\n            },\n            {\n                \"distance\": 2.5,\n                \"line\": \"Калужско-Рижская\",\n                \"name\": \"Свиблово\"\n            }\n        ]\n    }\n]";
        _logger.LogDebug(responseBody);
        var uri = _config.GetSection("DadataURI").Value;
        var keySection = _config.GetSection("ApiKeys");
        var authKey = keySection.GetSection("AuthKey").Value;
        var secretKey = keySection.GetSection("SecretKey").Value;

        Console.WriteLine(authKey);
        Console.WriteLine(secretKey);
        Console.WriteLine(uri);
        return responseBody;
        var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var body = JsonConvert.SerializeObject(new List<string> { address });
        request.Content = new StringContent(
            body,
            Encoding.UTF8,
            "application/json"
        );
        
        request.Headers.Add("Authorization", "Token " + authKey);
        request.Headers.Add("X-Secret", secretKey);
        
        var httpClient = _clientFactory.CreateClient();
        var r = await httpClient.SendAsync(request);

        return await r.Content.ReadAsStringAsync();
    }
}