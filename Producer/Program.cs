using Data;
using Data.Configurations;
using Data.Repos;
using Domain.Entities;
using Domain.IRepos;
using Cotracts.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            //DI Area
            string Connection = "Server=.;Database=Task_DB;Trusted_Connection=True;";
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ProjectDbContext>(options =>
                options.UseSqlServer(Connection))
                .AddScoped<TeacherServices>()
                .BuildServiceProvider();

            var teacherServices = serviceProvider.GetService<TeacherServices>();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "TeacherVM",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string jsonString = Encoding.UTF8.GetString(body);
                    var json = JsonConvert.DeserializeObject<JObject>(jsonString);
                    var id = Int32.Parse(json.Property("id").Value.ToString());
                    switch (json.Property("process").Value.ToString())
                    {
                        case "add":
                            await teacherServices.AddTeacher(id);break;
                        case "update":
                            await teacherServices.UpdateTeacher(id); break;
                        case "delete":
                            teacherServices.DeleteTeacher(id); break;
                    }
                };
                channel.BasicConsume(queue: "TeacherVM",
                                     autoAck: true,
                                     consumer: consumer);
                /////////////////////////////////////////////////////
                channel.QueueDeclare(queue: "Harvest",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var harvest = new EventingBasicConsumer(channel);
                harvest.Received += async (model, ea) =>
                {
                    await teacherServices.Harvest();
                };
                channel.BasicConsume(queue: "Harvest",
                                     autoAck: true,
                                     consumer: harvest);

                Console.ReadKey();
            }
        }
    }
}
