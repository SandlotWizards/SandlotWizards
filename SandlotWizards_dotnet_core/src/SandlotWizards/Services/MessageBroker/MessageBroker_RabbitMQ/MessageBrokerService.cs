using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SandlotWizards.Common.Command;
using SandlotWizards.MessageBroker;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageBroker_RabbitMQ
{
    public class MessageBrokerService : IMessageBrokerService
    {
        private readonly IConsumerCallbackDelegate _callbackDelegate;
        private readonly IConfiguration _configuration;
        private readonly ILogger<MessageBrokerService> _logger;

        public MessageBrokerService(IConsumerCallbackDelegate callbackDelegate, IConfiguration configuration, ILogger<MessageBrokerService> logger)
        {
            _callbackDelegate = callbackDelegate ?? throw new ArgumentNullException(nameof(callbackDelegate));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Publish(string queue, string eventName, string message)
        {
            using IConnection connection = CreateFactory().CreateConnection();
            using IModel channel = connection.CreateModel();

            var basicProperties = channel.CreateBasicProperties();
            basicProperties.ContentType = "text/plain";
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.Headers.Add("EventName", Encoding.UTF8.GetBytes(eventName));

            _logger.LogInformation("{" + eventName + "} command has been published to the {" + queue + "} queue.");
            channel.QueueDeclare(queue, true, false, false, null);
            channel.BasicPublish("", queue, basicProperties, Encoding.UTF8.GetBytes(message));
        }

        public void PublishJson(string jsonCommandString)
        {
            CommandBase cmd = Newtonsoft.Json.JsonConvert.DeserializeObject<CommandBase>(jsonCommandString);
            using IConnection connection = CreateFactory().CreateConnection();
            using IModel channel = connection.CreateModel();

            var basicProperties = channel.CreateBasicProperties();
            basicProperties.ContentType = "text/plain";
            basicProperties.Headers = new Dictionary<string, object>();
            basicProperties.Headers.Add("EventName", Encoding.UTF8.GetBytes(cmd.EventName));

            _logger.LogInformation("{" + cmd.EventName + "} command has been published to the {" + cmd.QueueName + "} queue.");
            channel.QueueDeclare(cmd.QueueName, true, false, false, null);
            channel.BasicPublish("", cmd.QueueName, basicProperties, Encoding.UTF8.GetBytes(jsonCommandString));
        }

        public void Consume(string queue)
        {
            IConnection connection = CreateFactory().CreateConnection();
            IModel channel = connection.CreateModel();

            BasicGetResult result;
            try
            {
                _logger.LogInformation("Check for new messages on the {" + queue + "} queue.");
                result = channel.BasicGet(queue, true);
            }
            catch (RabbitMQ.Client.Exceptions.OperationInterruptedException)
            {
                _logger.LogInformation("{" + queue + "} queue was not found on the message broker. The {" + queue + "} queue is being created.");

                connection = CreateFactory().CreateConnection();
                channel = connection.CreateModel();
                channel.QueueDeclare(queue, true, false, false, null);

                _logger.LogInformation($"Check for new messages on the {" + queue + "} queue.");
                result = channel.BasicGet(queue, true);
            }

            channel.Close();
            connection.Close();

            if (result != null)
            {
                object eventNameObj = "";
                result.BasicProperties.Headers.TryGetValue("EventName", out eventNameObj);
                string eventName = Encoding.ASCII.GetString((byte[])eventNameObj);
                string message = Encoding.UTF8.GetString(result.Body.ToArray());
                _callbackDelegate.Consume(eventName, message);
            }
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            object eventNameObj = "";
            e.BasicProperties.Headers.TryGetValue("EventName", out eventNameObj);
            string eventName = Encoding.ASCII.GetString((byte[])eventNameObj);
            string message = Encoding.UTF8.GetString(e.Body.ToArray());
            _callbackDelegate.Consume(eventName, message);
        }

        private ConnectionFactory CreateFactory()
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.HostName = _configuration["MessageBroker_RabbitMQ:HostName"];
            factory.Port = int.Parse(_configuration["MessageBroker_RabbitMQ:Port"]);
            factory.UserName = _configuration["MessageBroker_RabbitMQ:UserName"];
            factory.Password = _configuration["MessageBroker_RabbitMQ:Password"];
            factory.VirtualHost = _configuration["MessageBroker_RabbitMQ:VirtualHost"];
            return factory;
        }


    }
}

