using System;
using System.Collections.Generic;
using System.Text;

namespace AzureServiceBus.Domain
{
    public class ServiceBusMessage
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
    }
}
