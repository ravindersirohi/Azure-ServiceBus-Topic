using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.UI.Models;
using ServiceBus.UI.Services;

namespace ServiceBus.UI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ServiceMessage> Get()
        {
            var result = new SenderService().Receive();
            return result;
        }

        [HttpPost]
        public ServiceMessage Post(ServiceMessage dto)
        {
            var result = new SenderService().Send(dto);
            return result;
        }
    }
}
