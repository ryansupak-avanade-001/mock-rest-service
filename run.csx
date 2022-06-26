#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

using System.Web;

public static OkObjectResult Run(HttpRequest req, ILogger log)
{
    // sample input URL format:
    // curl "https://mock-dsis-api.azurewebsites.net/api/Param001/Param002/Param003/Param004?$filter=(native_uid%20eq%20%27Param005%27)&$format=json"

    string queryTargetID = HttpUtility.UrlDecode(req.QueryString.ToString()).Split("'")[1].ToString();

    MockRestAPIReturn mockRestAPIReturn = new MockRestAPIReturn();
    mockRestAPIReturn.fullUniqueID = req.RouteValues["RestAPIParam1"] + "--" +  req.RouteValues["RestAPIParam2"] + "--" + req.RouteValues["RestAPIParam3"] + "--" + req.RouteValues["RestAPIParam4"] + "--" + queryTargetID.ToString();
    mockRestAPIReturn.description = mockRestAPIReturn.description + mockRestAPIReturn.fullUniqueID.ToString();
 
    log.LogInformation(JsonConvert.SerializeObject(mockRestAPIReturn));
    
    req.HttpContext.Response.Headers.Add("Content-Type", "application/json");
    return new OkObjectResult((Object)mockRestAPIReturn);
}

class MockRestAPIReturn
{
    public string fullUniqueID;
    public string description = "This is a mocked DSIS data record corresponding to the requested ID:";
}
