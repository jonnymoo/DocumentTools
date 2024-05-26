using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using DocumentTools.ZipService;

namespace DocumentTools.AzureFunctions;

public class ZipAPIs
{
    private readonly ILogger _logger;

    public ZipAPIs(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<ZipAPIs>();
    }

    [Function("GetDocumentFromZip")]
    public async Task<IActionResult> GetDocumentFromZip([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        // Parse JSON input
        using var reqStream = new StreamReader(req.Body);
        var jsonString = await reqStream.ReadToEndAsync();

        var inputData = JsonConvert.DeserializeObject<dynamic>(jsonString);


        // Validate input data (replace with your validation logic)
        if (inputData == null || !inputData?.ContainsKey("Zip") || !inputData?.ContainsKey("Index"))
        {
            return new ObjectResult("Invalid input data format")
            {
                StatusCode = 400
            };
        }

        try
        {
            string zipBase64 = inputData!.Zip;
            int index = inputData!.Index;
            using Zip zip = new(zipBase64.Replace("&#13;&#10;", ""));
            return new FileContentResult(zip[index], "application/octet-stream");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred processing the zip file.");

            return new ObjectResult($"Error processing data: {ex.Message}")
            {
                StatusCode = 400
            };
        }
    }

    [Function("Entries")]
    public async Task<IActionResult> Entries([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
    {
        // Parse JSON input
        using var reqStream = new StreamReader(req.Body);
        var jsonString = await reqStream.ReadToEndAsync();

        var inputData = JsonConvert.DeserializeObject<dynamic>(jsonString);


        // Validate input data (replace with your validation logic)
        if (inputData == null || !inputData?.ContainsKey("Zip") )
        {
            return new ObjectResult("Invalid input data format")
            {
                StatusCode = 400
            };
        }

        try
        {
            string zipBase64 = inputData!.Zip;
            int index = inputData!.Index;
            using Zip zip = new(zipBase64.Replace("&#13;&#10;", ""));
            return new OkObjectResult(zip.Files);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred processing the zip file.");

            return new ObjectResult($"Error processing data: {ex.Message}")
            {
                StatusCode = 400
            };
        }
    }
}
