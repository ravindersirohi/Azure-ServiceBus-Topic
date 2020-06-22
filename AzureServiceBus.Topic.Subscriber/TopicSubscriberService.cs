using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using AzureServiceBus.Domain;
using System.Threading;

namespace AzureServiceBus.Topic.Subscriber
{
    public class TopicSubscriberService
    {
        const string ServiceBusConnectionString = "Endpoint=sb://hvguserservice.servicebus.windows.net/;SharedAccessKeyName=ListenPolicy;SharedAccessKey=/xljJVxlXJaVAW1hNQaJAbPHpIwdtJd5MTep75R3O8I=";
        const string TopicName = "usermessagetopic";
        const string SubscriptionName = "UserMessageTopic-Subscription";
        static ISubscriptionClient subscriptionClient;
        public async Task ReadMessage()
        {
            try
            {
                subscriptionClient = new SubscriptionClient(ServiceBusConnectionString, TopicName, SubscriptionName);
                RegisterOnMessageHandlerAndReceiveMessages();
                await subscriptionClient.CloseAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        static void RegisterOnMessageHandlerAndReceiveMessages()
        {
            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler);
            subscriptionClient.RegisterMessageHandler(ProcessMessagesAsync, messageHandlerOptions);
        }

       static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            Console.WriteLine($"Received message: SequenceNumber:{message.SystemProperties.SequenceNumber} Body:{Encoding.UTF8.GetString(message.Body)}");
            await subscriptionClient.CompleteAsync(message.SystemProperties.LockToken);
        }
        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
            Console.WriteLine("Exception context for troubleshooting:");
            Console.WriteLine($"- Endpoint: {context.Endpoint}");
            Console.WriteLine($"- Entity Path: {context.EntityPath}");
            Console.WriteLine($"- Executing Action: {context.Action}");
            return Task.CompletedTask;
        }
    }
}
