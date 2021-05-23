using Data;
using Data.Configurations;
using Data.Repos;
using Domain.Entities;
using Domain.IRepos;
using Domain.VMs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using TaskAPI;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string Connection = "Server=.;Database=Task_DB;Trusted_Connection=True;";
            var serviceProvider = new ServiceCollection()
                .AddDbContext<ProjectDbContext>(options =>
                options.UseSqlServer(Connection))
                .AddAutoMapper(typeof(AutoMapping))
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
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    string jsonString = Encoding.UTF8.GetString(body);
                    var teacher = JsonConvert.DeserializeObject<TeacherVM>(jsonString);
                    //TeacherServices teacherServices1 = new TeacherServices(serviceProvider);
                    teacherServices.AddTeacher(teacher);
                    //teacherServices.AddTeacher(teacher);
                };
                channel.BasicConsume(queue: "TeacherVM",
                                     autoAck: true,
                                     consumer: consumer);

                Console.ReadKey();
            }
        }
    }
}
