using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskAPI.Controllers
{
    [Route("Harvest")]
    public class HarvestController : Controller
    {
        [HttpGet]
        public void Harvest()
        
        
        
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "Harvest",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                channel.BasicPublish(exchange: "",
                                     routingKey: "Harvest",
                                     basicProperties: null,
                                     body: new byte[1]);
            }
        }
    }
}
