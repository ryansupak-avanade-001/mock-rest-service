#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

using System.Web;

public static OkObjectResult Run(HttpRequest req, ILogger log)
{
    // sample input URL format:
    // curl "https://mock-dsis-api.azurewebsites.net/api/Blarg001/Blarg002/Blarg003/Blarg004?$filter=(native_uid%20eq%20%271112%27)&$format=json"

    log.LogInformation("query string is as follows:");
    //log.LogInformation(req.Query["$filter"].ToString());
    log.LogInformation(req.QueryString.ToString());
    log.LogInformation(HttpUtility.UrlDecode(req.QueryString.ToString()));
    log.LogInformation(HttpUtility.UrlDecode(req.QueryString.ToString()).Split("'")[1].ToString());

    string queryTargetID = HttpUtility.UrlDecode(req.QueryString.ToString()).Split("'")[1].ToString();
    //string queryTargetID = "1234"; 

    MockRestAPIReturn mockRestAPIReturn = new MockRestAPIReturn();
    mockRestAPIReturn.fullUniqueID = req.RouteValues["RestAPIParam1"] + "--" +  req.RouteValues["RestAPIParam2"] + "--" + req.RouteValues["RestAPIParam3"] + "--" + req.RouteValues["RestAPIParam4"] + "--" + queryTargetID.ToString();
    mockRestAPIReturn.description = mockRestAPIReturn.description + mockRestAPIReturn.fullUniqueID.ToString();
 
    log.LogInformation(JsonConvert.SerializeObject(mockRestAPIReturn));
    //return JsonConvert.SerializeObject(mockRestAPIReturn);

    //req.HttpContext.Response.Headers.Add("Content-Type", "application/json");
    return new OkObjectResult((Object)mockRestAPIReturn);
}

class MockRestAPIReturn
{
    public string fullUniqueID;
    public string description = "This is a mocked DSIS data record corresponding to the requested ID:";
}
