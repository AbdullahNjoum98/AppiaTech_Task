using Domain.VMs;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TaskAPI
{
    public static class HelperMethods
    {
        public static string getException(Exception exception) {
            string formattedException=exception.Message;
            if (exception.InnerException != null)
            {
                formattedException+= getException(exception.InnerException);
            }
            return formattedException;
        }
        public static void Producer(byte[] bytesObject) {
            string jsonString = Encoding.UTF8.GetString(bytesObject);
            var josnObject = JsonConvert.DeserializeObject<StudentVM>(jsonString);
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: josnObject.GetType().Name,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = josnObject.Name;
                var body = Encoding.UTF8.GetBytes(message);
                Console.WriteLine(message);
                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
