using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MovieAPI
{
    public class RabbitMQService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQService()
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbitmq",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(queue: "movie_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                  routingKey: "movies_queue",
                                  basicProperties: null,
                                  body: body);

            Console.WriteLine($"Sent message: {message}");
        }

        public void ReceiveMessage()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"Received message: {message}");
            };

            _channel.BasicConsume(queue: "movies_queue",
                                  autoAck: true,
                                  consumer: consumer);
        }

        public void CloseConnection()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
