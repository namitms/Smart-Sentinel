using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using Thanos.Models;

namespace Thanos.Adapter.ServiceBus
{
    public static class Receiver
    {
        /// <summary>
        /// The Q connection manager
        /// </summary>
        private static ConfigurationManager congMgr = new ConfigurationManager();

        public static EventPublisher eventPub = new EventPublisher();


        public static void RegisterOnMessageHandlerAndReceiveMessages(bool IsSentinel = true)
        {
            // Configure the MessageHandler Options in terms of exception handling, number of concurrent messages to deliver etc.
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                // Maximum number of Concurrent calls to the callback `ProcessMessagesAsync`, set to 1 for simplicity.
                // Set it according to how many messages the application wants to process in parallel.
                MaxConcurrentCalls = 1,

                // Indicates whether MessagePump should automatically complete the messages after returning from User Callback.
                // False below indicates the Complete will be handled by the User Callback as in `ProcessMessagesAsync` below.
                AutoComplete = false
            };

            // Register the function that will process messages
            if (IsSentinel)
            {
                congMgr.BrainQueueClient.RegisterMessageHandler(ProcessMessagesForBrainAsync, messageHandlerOptions);
            }
            else
            {
                congMgr.NodeQueueClient.RegisterMessageHandler(ProcessMessagesForNodeAsync, messageHandlerOptions);
            }
        }


        static async System.Threading.Tasks.Task ProcessMessagesForBrainAsync(Message message, CancellationToken token)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            ParseAndSend(message);
            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
            await congMgr.BrainQueueClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
            // to avoid unnecessary exceptions.
        }

        static async System.Threading.Tasks.Task ProcessMessagesForNodeAsync(Message message, CancellationToken token)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");

            ParseAndSend(message);
            // Complete the message so that it is not received again.
            // This can be done only if the queueClient is created in ReceiveMode.PeekLock mode (which is default).
            await congMgr.NodeQueueClient.CompleteAsync(message.SystemProperties.LockToken);

            // Note: Use the cancellationToken passed as necessary to determine if the queueClient has already been closed.
            // If queueClient has already been Closed, you may chose to not call CompleteAsync() or AbandonAsync() etc. calls 
            // to avoid unnecessary exceptions.
        }

        private static void ParseAndSend(Message msg)
        {
            if (msg.Label == MESSAGE_TYPE.HEART_BEAT.ToString())
            {
                var hbArg = JsonConvert.DeserializeObject<HeartBeat>(Encoding.UTF8.GetString(msg.Body));
                eventPub.OnHearBeatReceived(hbArg);
            }
            else if (msg.Label == MESSAGE_TYPE.EDGE_CONFIGUATION.ToString())
            {
                var ecArg = JsonConvert.DeserializeObject<EdgeConfiguration>(Encoding.UTF8.GetString(msg.Body));
                eventPub.OnEdgeConfigurationReceived(ecArg);
            }
            else if (msg.Label == MESSAGE_TYPE.TASK_START.ToString())
            {
                var taskArg = JsonConvert.DeserializeObject<Thanos.Models.Task>(Encoding.UTF8.GetString(msg.Body));
                eventPub.OnTaskStartRequestReceived(taskArg);
            }
            else if (msg.Label == MESSAGE_TYPE.TASK_END.ToString())
            {
                var taskArg = JsonConvert.DeserializeObject<Thanos.Models.Task>(Encoding.UTF8.GetString(msg.Body));
                eventPub.OnTaskEndRequestReceived(taskArg);
            }
            else if (msg.Label == MESSAGE_TYPE.TASK_RESPONSE.ToString())
            {
                var taskArg = JsonConvert.DeserializeObject<Thanos.Models.Task>(Encoding.UTF8.GetString(msg.Body));
                eventPub.OnTaskResponseReceived(taskArg);
            }
        }

        static System.Threading.Tasks.Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return System.Threading.Tasks.Task.CompletedTask;
        }


    }

    public class EventPublisher
    {
        public virtual void OnHearBeatReceived(HeartBeat e)
        {
            EventHandler<HeartBeat> handler = HearBeatReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnEdgeConfigurationReceived(EdgeConfiguration e)
        {
            EventHandler<EdgeConfiguration> handler = EdgeConfigurationReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnTaskStartRequestReceived(Thanos.Models.Task e)
        {
            EventHandler<Thanos.Models.Task> handler = TaskStartRequestReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnTaskEndRequestReceived(Thanos.Models.Task e)
        {
            EventHandler<Thanos.Models.Task> handler = TaskEndRequestReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public virtual void OnTaskResponseReceived(Thanos.Models.Task e)
        {
            EventHandler<Thanos.Models.Task> handler = TaskResponseReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<HeartBeat> HearBeatReceived;

        public event EventHandler<EdgeConfiguration> EdgeConfigurationReceived;

        public event EventHandler<Thanos.Models.Task> TaskStartRequestReceived;

        public event EventHandler<Thanos.Models.Task> TaskEndRequestReceived;

        public event EventHandler<Thanos.Models.Task> TaskResponseReceived;
    }

}
