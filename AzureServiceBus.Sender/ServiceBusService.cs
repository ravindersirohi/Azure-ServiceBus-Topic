
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Azure.ServiceBus;
using AzureServiceBus.Domain;

namespace AzureServiceBus.Sender
{
    public class ServiceBusService
    {
        const string MSG_TYPE = "User Message";
        const string ServiceBusConnectionString = "Endpoint=sb://hvguserservice.servicebus.windows.net/;SharedAccessKeyName=SendPolicy;SharedAccessKey=/i7X0LTElGzKmstTbQq+c903mzWViiCiVZpwRlJ2r20=";
        const string TopicName = "usermessagetopic";
        public async Task SendMessage(MessageModel dto)
        {
            try
            {
                string messageString = JsonSerializer.Serialize(dto);

                ITopicClient topicClient = new TopicClient(ServiceBusConnectionString, TopicName);

                var serviceBusMessage = new ServiceBusMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = MSG_TYPE,
                    Content = messageString
                };

                var message = new Message(Encoding.UTF8.GetBytes(messageString));
                message.UserProperties.Add("Type", serviceBusMessage.Type);
                message.MessageId = serviceBusMessage.Id;

                await topicClient.SendAsync(message);
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
