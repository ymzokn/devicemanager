using DeviceManager.Hubs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DeviceManager.Services
{
    public class RabbitMQConsumer : IHostedService
    {
        protected readonly IConnection _connection;
        protected readonly IModel _channel;
        protected readonly ConnectionFactory _factory;
        private readonly CountHub _hub;
        private readonly string _queueName;
        protected readonly IServiceProvider _serviceProvider;

        public RabbitMQConsumer(string rabbitMQConnectionString, string queueName, IServiceProvider serviceProvider, CountHub hub)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMQConnectionString)
            };

            _factory.AutomaticRecoveryEnabled = true;
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            _queueName = queueName;
            _serviceProvider = serviceProvider;
            _hub = hub;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
                _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false);

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (model, eventArgs) =>
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var measurementService = scope.ServiceProvider.GetRequiredService<IMeasurementService>();

                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        await _hub.SendCount(measurementService.GetMeasurementCount().ToString());
                    }
                };

                _channel.BasicConsume(queue: _queueName, consumer: consumer);
                
                return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _channel.Close();
            _connection.Close();

            return Task.CompletedTask;
        }
    }
}
