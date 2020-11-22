using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;

namespace Thanos.Sentinel.Workers
{
    public class TaskResponseAction
    {
        private HttpClient client = null;
        public TaskResponseAction()
        {
            Receiver.eventPub.TaskResponseReceived += EventPub_TaskResponseReceived; ;
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            client = new HttpClient(clientHandler);
        }

        private void EventPub_TaskResponseReceived(object sender, Models.Task e)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var json = JsonConvert.SerializeObject(e);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(e.Properties[Statics.REPONSE_URL_STRING].ToString(), stringContent).Result;
            response.EnsureSuccessStatusCode();

        }
    }
}
