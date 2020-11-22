﻿using Microsoft.Azure.ServiceBus;
using System;
using System.Text;
using Thanos.Models;

namespace Thanos.Adapter.ServiceBus
{
    public static class Dispatcher
    {
        /// <summary>
        /// The Q connection manager
        /// </summary>
        private static ConfigurationManager congMgr = new ConfigurationManager();

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="numberOfMessagesToSend"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task SendMessagesToBrainAsync(string msg, MESSAGE_TYPE msgType)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(msg));
                message.Label = msgType.ToString();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " - Sending message to Brain : " + msg);
                // Send the message to the queue.
                await congMgr.BrainQueueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
                throw;
            }
        }

        /// <summary>
        /// Send message
        /// </summary>
        /// <param name="numberOfMessagesToSend"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task SendMessagesToNodeAsync(string msg, MESSAGE_TYPE msgType)
        {
            try
            {
                var message = new Message(Encoding.UTF8.GetBytes(msg));
                message.Label = msgType.ToString();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToShortTimeString() + " - Sending message to Node : " + msg);
                // Send the message to the queue.
                await congMgr.NodeQueueClient.SendAsync(message);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
                throw;
            }
        }
    }
}
