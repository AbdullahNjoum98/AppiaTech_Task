using Domain.Entities;
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
        public static string getException(Exception exception)
        {
            string formattedException = exception.Message;
            if (exception.InnerException != null)
            {
                formattedException += getException(exception.InnerException);
            }
            return formattedException;
        }
        public static void Producer(byte[] bytesObject)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "TeacherVM",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = bytesObject;
                channel.BasicPublish(exchange: "",
                                     routingKey: "TeacherVM",
                                     basicProperties: null,
                                     body: body);
            }
        }
        public static bool CustomContains<T>(List<T> list, T item)
        {
            if (list.Count == 0 || list == null || item == null) return false;
            foreach (var listItem in list)
            {
                if ((int)listItem.GetType().GetProperty("Id").GetValue(listItem) ==
                    (int)item.GetType().GetProperty("Id").GetValue(item))
                    return true;
            }
            return false;
        }
    }
}
