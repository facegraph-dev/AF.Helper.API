using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AF.ChromeDriver.Helper;

namespace GetChromeDriverDownloadUrl
{
    public static class ChromeDriverHelper
    {
        [FunctionName("GetChromeVersion")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string versionGoogleBrowser = req.Query["version"];
            string versionVhromeDriver = await AF.ChromeDriver.Helper.AF.GetDownloadVersion(versionGoogleBrowser);

            return new OkObjectResult(versionVhromeDriver);
        }
    }

}
