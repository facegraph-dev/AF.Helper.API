using System;
using System.Net.Http;

namespace AF.ChromeDriver.Helper
{
    class Program
    {
         static void Main(string[] args)
        {
            string ChromeBrowserDownloadUrl = "https://chromedriver.storage.googleapis.com/{CHROME_DRIVER_VERSION}/chromedriver_linux64.zip";
            string ChromeDriverVersionHelperUrl = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_{Version}";

            // Create a New HttpClient object.
            HttpClient client = new HttpClient();

            // Call asynchronous network methods in a try/catch block to handle exceptions
            try
            {
                string Google_Browser_Version = "72.0.3626.81";
                //string Google_Browser_Version = "Google Chrome 95.0.4638.69";

                Google_Browser_Version = Google_Browser_Version.Replace("Google Chrome ", "");

                Google_Browser_Version = Google_Browser_Version.Substring(0, Google_Browser_Version.LastIndexOf("."));

                HttpResponseMessage response = client.GetAsync(ChromeDriverVersionHelperUrl.Replace("{Version}", Google_Browser_Version)).Result;
                response.EnsureSuccessStatusCode();
                string responseBody = response.Content.ReadAsStringAsync().Result;
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);

                Console.WriteLine(responseBody);
                Console.WriteLine(ChromeBrowserDownloadUrl.Replace("{CHROME_DRIVER_VERSION}", responseBody));
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }

            // Need to call dispose on the HttpClient object
            // when done using it, so the app doesn't leak resources
            client.Dispose();
        }
    }
}
