using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;
using IModel = RabbitMQ.Client.IModel;
using Microsoft.Extensions.Logging;

namespace Application.MqService
{
    public class PublisherService : IDisposable
    {
        private readonly ILogger<PublisherService> logger;

        private IConnection connection;
        private IModel channel;

        private const string PublicshQueueName = "pub_queue";
        private string rabbitMQConnectionString;

        public PublisherService(IConfiguration configuration, ILogger<PublisherService> logger)
        {
            this.logger = logger;

            rabbitMQConnectionString = configuration.GetSection("RabbitMQConfig")
                                       .GetSection("ConnectionString").Value;

            if (string.IsNullOrEmpty(rabbitMQConnectionString))
                throw new Exception("RabbitMQConfig.ConnectionString is not defined in appsettings.json");
        }

        private void CreateRabbitMqConnection()
        {
            if (channel != null)
                return;
            try
            {
                var factory = new ConnectionFactory { Uri = new Uri(rabbitMQConnectionString) };
                connection = factory.CreateConnection();
                channel = connection.CreateModel();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to create PublisherService connection: ");
            }
        }

        public void SendMessage(string message)
        {
            CreateRabbitMqConnection();
            var body = Encoding.UTF8.GetBytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;

            channel.BasicPublish(exchange: "",
                                 routingKey: PublicshQueueName,
                                 basicProperties: properties,
                                 body: body);
        }

        public void Dispose()
        {
            connection?.Dispose();
            channel?.Dispose();
        }
    }
}
