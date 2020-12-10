using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Thanos.Sentinel.UI.Data
{
    public class BlogService
    {
        //private static readonly string[] Summaries = new[]
        //{
        //    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        //};

        //public Task<WeatherForecast[]> GetForecastAsync(DateTime startDate)
        //{
        //    var rng = new Random();
        //    return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = startDate.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    }).ToArray());
        //}

        public async Task<List<string>> GetLoops(string filter)
        {
            List<string> files = new List<string>();
            string blobConectionString = "DefaultEndpointsProtocol=https;AccountName=stx15chatops;AccountKey=m3W74xcgtxp7aNZqdN0WiyKYg5dPG0ScDbyY9FPLuF5CC+TXZlpYIXkFER3mC1rmbQpTFwioLkPWiCYj6wVEsw==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobConectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("templates");

            foreach (BlobItem blobItem in blobContainerClient.GetBlobs())
            {
                if (blobItem.Name.ToLower().Contains(filter))
                {
                    files.Add(blobItem.Name);
                }
            }
            return files;
        }

        public async Task<string> GetJson(string blobName)
        {
            List<string> files = new List<string>();
            string blobConectionString = "DefaultEndpointsProtocol=https;AccountName=stx15chatops;AccountKey=m3W74xcgtxp7aNZqdN0WiyKYg5dPG0ScDbyY9FPLuF5CC+TXZlpYIXkFER3mC1rmbQpTFwioLkPWiCYj6wVEsw==;EndpointSuffix=core.windows.net";
            BlobServiceClient blobServiceClient = new BlobServiceClient(blobConectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient("templates");
            var stream = new MemoryStream();
            blobContainerClient.GetBlobClient(blobName).DownloadTo(stream);
            byte[] bytes = stream.ToArray();

            string jsonString = Encoding.UTF8.GetString(bytes);

            var client = new HttpClient();
            string url = "https://thanossentinel.azurewebsites.net/api/task";
            var resultCode = client.PostAsync(url, new StringContent(jsonString, Encoding.UTF8, "application/json"));

            return jsonString;
        }

        public async Task Clear()
        {
            var client = new HttpClient();
            string url = "https://thanossentinel.azurewebsites.net/api/task?id=ALL";
            var resultCode = client.DeleteAsync(url);
            return;
        }

        public async Task Clear(string key)
        {
            var client = new HttpClient();
            string url = "https://thanossentinel.azurewebsites.net/api/task?id="+ key;
            var resultCode = client.DeleteAsync(url);
            return;
        }
    }
}
