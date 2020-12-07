using Newtonsoft.Json;
using Q42.HueApi;
using Q42.HueApi.ColorConverters.Gamut;
using Q42.HueApi.ColorConverters.Original;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Thanos.Adapter.ServiceBus;
using Thanos.Models;

namespace Thanos.WatchDog.Events
{
    public class HueResponse
    {
        private HttpClient client = null;

        private ILocalHueClient HueClientGround;

        private ILocalHueClient HueClientFirst;

        public HueResponse()
        {
            var urlParams = State.State.Instance.EdgeConfiguration.HueGroundBaseURL.Split("/");
            HueClientGround = new LocalHueClient(urlParams[2], urlParams[4]);
            urlParams = State.State.Instance.EdgeConfiguration.HueTopBaseURL.Split("/");
            HueClientFirst = new LocalHueClient(urlParams[2], urlParams[4]);

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            // Pass the handler to httpclient(from you are calling api)
            client = new HttpClient(clientHandler);
        }

        public void ProcessHueRequest(Thanos.Models.Task task)
        {
            string response = GetState(task.Properties).Result;
            if (!task.Properties.ContainsKey(Statics.TASK_RESPONSE_STRING))
            {
                task.Properties.Add(Statics.TASK_RESPONSE_STRING, response);
            }
            else
            {
                task.Properties[Statics.TASK_RESPONSE_STRING] = response;
            }
            Dispatcher.SendMessagesToBrainAsync(JsonConvert.SerializeObject(task), MESSAGE_TYPE.TASK_RESPONSE).Wait();
        }
        public void ProcessHueFlow(Thanos.Models.Task task)
        {
            Flow flow = (Flow)task.Properties[Statics.FLOW_STRING];
            if (flow.Timeline.Steps.Count > 0)
            {

                //Loop Flow
                if (flow.Current_Step_Index >= flow.Timeline.Steps.Count)
                {
                    if (flow.Loop == true)
                    {
                        flow.Current_Step_Index = 0;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("*******   Exiting Flow   *******");
                        Console.WriteLine("Time: " + DateTime.Now + ", FlowID :" + task.ID + " Tick : " + flow.Current_Duration_Tick);

                        Dispatcher.SendMessagesToNodeAsync(JsonConvert.SerializeObject(task), MESSAGE_TYPE.TASK_END).Wait();

                        return;
                    }
                }


                //Call Hue Logic here
                //For every asset in the current step
                if (flow.Current_Duration_Tick == 1)
                {
                    var assetList = flow.Timeline.Steps[flow.Current_Step_Index].Assets;


                    foreach (var a in assetList)
                    {
                        int transTime = 0;
                        if (flow.Timeline.Steps[flow.Current_Step_Index].Transition == TANSITION_TYPE.MORPH)
                        {
                            transTime = flow.Timeline.Steps[flow.Current_Step_Index].Persistance_Duration;
                        }
                        string response = SetState(a.Properties, TimeSpan.FromSeconds(transTime), a.ColorLoop).Result;
                    }
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("*******   Executing Flow   *******");
                    Console.WriteLine("Time: " + DateTime.Now + ", FlowID :" + task.ID + " Tick : " + flow.Current_Duration_Tick);
                }
                //Move to next step
                if (flow.Current_Duration_Tick >= flow.Timeline.Steps[flow.Current_Step_Index].Persistance_Duration)
                {
                    flow.Current_Duration_Tick = 0;
                    flow.Current_Step_Index++;
                }

                flow.Current_Duration_Tick++;
            }
            task.Properties[Statics.FLOW_STRING] = flow;

        }

        private async Task<string> GetState(Dictionary<string, object> properties)
        {
            string resString = null;
            var path = properties[Statics.URL_STRING].ToString();
            var hueHub = properties[Statics.HUE_HUB_STRING].ToString();
            try
            {
                if (hueHub == Statics.HUE_HUB_GROUND)
                {
                    client.BaseAddress = new Uri(State.State.Instance.EdgeConfiguration.HueGroundBaseURL);
                }
                else if (hueHub == Statics.HUE_HUB_FIRST)
                {
                    client.BaseAddress = new Uri(State.State.Instance.EdgeConfiguration.HueTopBaseURL);
                }
                else
                {
                    throw new Exception("No Hue Hub specified");
                }

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(path.ToLower());

                if (response.IsSuccessStatusCode)
                {
                    resString = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                //log error
            }
            return resString;
        }


        private async Task<string> SetState(Dictionary<string, object> properties, TimeSpan transition, bool colorLoop)
        {
            string resString = null;
            try
            {
                var path = properties[Statics.URL_STRING].ToString();
                var hueHub = properties[Statics.HUE_HUB_STRING].ToString();

                ILocalHueClient HueClient;

                if (hueHub == Statics.HUE_HUB_GROUND)
                {
                    HueClient = HueClientGround;
                }
                else if (hueHub == Statics.HUE_HUB_FIRST)
                {
                    HueClient = HueClientFirst;
                }
                else
                {
                    throw new Exception("No Hue Hub specified");
                }

                var command = new LightCommand();
                command = JsonConvert.DeserializeObject<LightCommand>(properties[Statics.PAYLOAD_STRING].ToString());
                command.TransitionTime = transition;

                

                var color = new Q42.HueApi.ColorConverters.RGBColor(properties[Statics.COLOR_STRING].ToString());

                float X = (float)color.R * 0.649926f + (float)color.G * 0.103455f + (float)color.B * 0.197109f;
                float Y = (float)color.R * 0.234327f + (float)color.G * 0.743075f + (float)color.B * 0.022598f;
                float Z = (float)color.R * 0.0000000f + (float)color.G * 0.053077f + (float)color.B * 1.035763f;
                float x = X / (X + Y + Z);
                float y = Y / (X + Y + Z);
                command.Effect = Effect.None;
                command.Alert = Alert.None;
                if (properties.ContainsKey(Statics.ALERT_MODE) && properties[Statics.ALERT_MODE] != null)
                {
                    int alertmode = int.Parse(properties[Statics.ALERT_MODE].ToString());
                    switch(alertmode)
                    {
                        case 1:
                            {
                                command.Alert = Alert.Once;
                                break;
                            }
                        case 2:
                            {
                                command.Alert = Alert.Multiple;
                                break;
                            }
                    }
                }
                command.SetColor(color);

                await HueClient.SendCommandAsync(command, new List<string> { path });

                if (colorLoop)
                {
                    command.Effect = Effect.ColorLoop;

                    await HueClient.SendCommandAsync(command, new List<string> { path });
                }
            }
            catch (Exception e)
            {
                var error = e.Message;
                //log error
            }
            return resString;
        }

    }
}
