namespace SandlotWizards.MessageBroker
{
    public interface IMessageBrokerService
    {
        void Publish(string queue, string eventName, string message);
        void PublishJson(string jsonCommandString);
        void Consume(string queue);
    }
}
