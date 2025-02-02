using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Application.MqService
{
    public class SubscriberService : IDisposable
    {
        private readonly ILogger<PublisherService> logger;
        private readonly IApplicationDbContext context;
        private IConnection connection;
        private IModel channel;

        private const string SubscriberQueueName = "pub_queue";
        private string rabbitMQConnectionString;

        public SubscriberService(IConfiguration configuration, ILogger<PublisherService> logger
            , IServiceProvider serviceProvider, IApplicationDbContext context)
        {
            this.context = context;
            this.logger = logger;

            rabbitMQConnectionString = configuration.GetSection("RabbitMQConfig")
                                       .GetSection("ConnectionString").Value;

            if (string.IsNullOrEmpty(rabbitMQConnectionString))
                throw new Exception("RabbitMQConfig.ConnectionString is not defined in appsettings.json");

            SubscribeChannel();
        }

        private void SubscribeChannel()
        {
            try
            {
                var factory = new ConnectionFactory { Uri = new Uri(rabbitMQConnectionString) };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();

                channel.QueueDeclare(queue: SubscriberQueueName, exclusive: false, autoDelete: false, durable: true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
            }

        }

        public void StartListening()
        {
            try
            {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($" [x] Received {message}");

                    await NotifyUsers(message);
                };

                channel.BasicConsume(queue: SubscriberQueueName, autoAck: true, consumer: consumer);
            }
            catch (Exception ex)
            {
                logger.LogError (ex.Message);
            }
            
        }

        private async Task NotifyUsers(string message)
        {
            var parts = message.Split('|');
            var taskId = int.Parse(parts[0]);
            var newStatus = parts[1];

            var taskAssignments = context.TaskAssignments.Where(ta => ta.TaskId == taskId).ToList();
            foreach (var assignment in taskAssignments)
            {
                var user = await context.Users.FindAsync(assignment.UserId);
                if (user != null)
                {
                    //ارسال پیام به کاربر
                    //برای راحتی و سریع نوشتن کد خارج از عملکرد برنامه فعلا کنسول لاگ شده
                    //اما با استفاده از ابزارهایی مثل signalR میشود نوتیف برای کلاینت ارسال کرد
                    Console.WriteLine($"{user.Username}: تسک تغییر وضعیت داده شده به {newStatus}");
                }
            }
        }

        public void Dispose()
        {
            try
            {
                channel?.Dispose();
                connection?.Dispose();
            }
            catch { }
        }
    }
}
