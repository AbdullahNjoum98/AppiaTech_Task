using Domain.Entities;
using Cotracts.VMs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
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
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration["elasticsearch:url"];
            var defaultIndex = configuration["elasticsearch:index"];

            var settings = new ConnectionSettings(new Uri(url))
                .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);

            services.AddSingleton(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<Student>(m => m
                .Ignore(p => p.TeacherId)
            );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Student>(x => x.AutoMap())
            );
        }
    }
}
