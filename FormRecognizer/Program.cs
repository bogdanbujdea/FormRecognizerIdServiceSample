using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace FormRecognizer
{
    class Program
    {
        private const string ApiKey = "<your key here>";
        private const string FormRecognizerEndpoint = "https://northeurope.api.cognitive.microsoft.com/";
        private static HttpClient _client;

        static async Task Main(string[] args)
        {
            var resultUrl = await StartAnalyzingImage();
            IdentificationResult result;
            do
            {
                await Task.Delay(1000); // wait 1 second before checking, otherwise the API will return 429
                var json = await _client.GetStringAsync(resultUrl);
                result = JsonConvert.DeserializeObject<IdentificationResult>(json);
            } while (result?.Status != "succeeded");
            
            var documentResults = result.AnalyzeResult.DocumentResults;
            if (documentResults != null && documentResults.Length != 0)
            {
                foreach (var documentResult in documentResults)
                {
                    Console.WriteLine($"First name: {documentResult.Fields.FirstName}, confidence: {documentResult.Fields.FirstName.Confidence}");
                    Console.WriteLine($"Last name: {documentResult.Fields.FirstName}, confidence: {documentResult.Fields.LastName.Confidence}");
                    Console.WriteLine($"Address: {documentResult.Fields.Address}, confidence: {documentResult.Fields.Address.Confidence}");
                    Console.WriteLine($"Country: {documentResult.Fields.Country}, confidence: {documentResult.Fields.Country.Confidence}");
                    Console.WriteLine($"DateOfBirth: {documentResult.Fields.DateOfBirth}, confidence: {documentResult.Fields.DateOfBirth.Confidence}");
                    Console.WriteLine($"DateOfExpiration: {documentResult.Fields.DateOfExpiration}, confidence: {documentResult.Fields.DateOfExpiration.Confidence}");
                    Console.WriteLine($"Region: {documentResult.Fields.Region}, confidence: {documentResult.Fields.Region.Confidence}");
                    Console.WriteLine($"Sex: {documentResult.Fields.Sex}, confidence: {documentResult.Fields.Sex.Confidence}");
                }
            }
        }

        private static async Task<string> StartAnalyzingImage()
        {
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            _client = new HttpClient();

            // set the key
            _client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", ApiKey);

            // set the endpoint

            queryString["includeTextDetails"] = "true";

            var uri = $"{FormRecognizerEndpoint}/formrecognizer/v2.1-preview.3/prebuilt/idDocument/analyze?{queryString}";

            var image = System.IO.File.ReadAllBytes(@"d:\driving.jpg");
            using var content = new ByteArrayContent(image);
            content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
            var response = await _client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                // if the response is successful, it means that Form Recognizer started processing our image
                // in order to get the result, we have to make GET requests to an URL
                // that URL is provided in a header named "Operation-Location"
                // below we're going to retrieve that URL and send it back to the caller of the function
                var location = response.Headers.FirstOrDefault(h => h.Key == "Operation-Location");
                return location.Value.FirstOrDefault();
            }

            return string.Empty;
        }
    }
}
