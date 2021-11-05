using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AF.ChromeDriver.Helper
{
    public static class AF
    {
        public static async Task<string> GetDownloadUrl(string Google_Browser_Version)
        {
            string ChromeBrowserDownloadUrl = "https://chromedriver.storage.googleapis.com/{CHROME_DRIVER_VERSION}/chromedriver_linux64.zip";
            string ChromeDriverVersion = await GetDownloadVersion(Google_Browser_Version);
            if (!string.IsNullOrEmpty(ChromeDriverVersion))
                return ChromeBrowserDownloadUrl.Replace("{CHROME_DRIVER_VERSION}", ChromeDriverVersion);
            else
                return "Error";

        }
        public static async Task<string> GetDownloadVersion(string Google_Browser_Version)
        {
            string ChromeBrowserDownloadUrl = "https://chromedriver.storage.googleapis.com/{CHROME_DRIVER_VERSION}/chromedriver_linux64.zip";
            string ChromeDriverVersionHelperUrl = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE{Version}";

            // Create a New HttpClient object.
            HttpClient client = new HttpClient();

            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                //Remove all spaces leading or trailing
                Google_Browser_Version = Google_Browser_Version?.Trim();

                if (!string.IsNullOrEmpty(Google_Browser_Version))
                {

                    Google_Browser_Version = Google_Browser_Version.Replace("Google Chrome ", "", StringComparison.OrdinalIgnoreCase);

                    Google_Browser_Version = "_" + Google_Browser_Version.Substring(0, Google_Browser_Version.LastIndexOf("."));
                }

                HttpResponseMessage response = await client.GetAsync(ChromeDriverVersionHelperUrl.Replace("{Version}", Google_Browser_Version));
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
                return "Error";
            }

            // Need to call dispose on the HttpClient object
            // when done using it, so the app doesn't leak resources
            client.Dispose();
        }
    }
}
