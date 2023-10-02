using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Sks365.MessageBrokers.Configuration.RabbitMq;

namespace Sks365.MessageBrokers.Subscribers;

public class RabbitMqSubscriber : ISubscriber
{
    private readonly IConnectionFactory _connectionFactory;
    private IConnection _connection;
    private IModel _channel;

    public event MessageHandler? MessageReceived;
    public event ListenerErrorHandler? ListenerErrorHandler;

    private readonly RabbitMqSubscriberConfiguration _settings;

    private Thread receivingThread;
    private EventingBasicConsumer eventingBasicConsumer;

    public RabbitMqSubscriber(RabbitMqSubscriberConfiguration settings)
    {
        _settings = settings;
        _connectionFactory = _settings.CreateConnectionFactory();
    }

    public void Start()
    {
        receivingThread = new Thread(EstablishSubscriptions);
        receivingThread.Start();
    }

    public void Stop()
    {
        if (_channel != null && _channel.IsOpen)
        {
            _channel.Close();
            _channel.Dispose();
        }

        if (_connection != null && _connection.IsOpen)
        {
            _connection.Close();
            _connection.Dispose();
        }

        if (receivingThread != null && eventingBasicConsumer != null)
        {
            eventingBasicConsumer.Received -= EventingBasicConsumer_Received;
        }
    }

    public void Restart()
    {
        Stop();
        Start();
    }

    public void Dispose()
    {
        Stop();
    }

    private void EventingBasicConsumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        var body = e.Body.Span;
        var message = Encoding.UTF8.GetString(body);
        var consumerResponse = MessageReceived(message);
        _channel.BasicAck(e.DeliveryTag, consumerResponse.Success);
    }

    private void EstablishSubscriptions(object? obj)
    {
        if (_connection == null || !_connection.IsOpen)
        {
            _connection = _connectionFactory.CreateConnection();
        }

        if (_channel == null || !_channel.IsOpen)
        {
            _channel = _connection.CreateModel();
        }

        _channel.QueueDeclare(_settings.QueueName, _settings.Durable, _settings.Exclusive, _settings.AutoDelete, null);
        _channel.ExchangeDeclare(_settings.ExchangeName, "topic", _settings.Durable, _settings.AutoDelete, null);
        _channel.QueueBind(_settings.QueueName, _settings.ExchangeName, _settings.RoutingKey);
        eventingBasicConsumer = new EventingBasicConsumer(_channel);
        _channel.BasicQos(0, _settings.Prefetch, false);
        _channel.BasicConsume(_settings.QueueName, false, eventingBasicConsumer);
        eventingBasicConsumer.Received += EventingBasicConsumer_Received;
    }
}