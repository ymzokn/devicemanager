using RabbitMQ.Client;
using System.Text;

namespace DeviceManager.Services
{
    public class RabbitMQProducer : IDisposable
    {
        protected readonly ConnectionFactory _factory;
        protected readonly IConnection _connection;
        protected readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _queueName;

        public RabbitMQProducer(string rabbitMQConnectionString, string exchangeName, string queueName)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMQConnectionString)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchangeName = exchangeName;
            _queueName = queueName;

            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: "");
        }
        public void Publish(string message)
        {
            try
            {
                var body = Encoding.UTF8.GetBytes(message);
                _channel.BasicPublish(exchange: _exchangeName, routingKey: "", basicProperties: null, body: body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}