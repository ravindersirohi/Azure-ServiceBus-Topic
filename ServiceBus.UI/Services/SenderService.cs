
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureServiceBus.Sender;
using AzureServiceBus.Domain;
using ServiceBus.UI.Models;
using AzureServiceBus.Topic.Subscriber;

namespace ServiceBus.UI.Services
{
    public class SenderService
    {
        public ServiceMessage Send(ServiceMessage dto)
        {
            var message = new MessageModel
            {
                Name = dto.Name,
                Message = dto.Message
            };
            var result = Task.Run(async () => await new ServiceBusService().SendMessage(message));
            dto.Status = result.Status.ToString();
            return dto;
        }
        public List<ServiceMessage> Receive()
        {

            var result = Task.Run(async () => await new TopicSubscriberService().ReadMessage());
            return new List<ServiceMessage>();
        }
    }
}
