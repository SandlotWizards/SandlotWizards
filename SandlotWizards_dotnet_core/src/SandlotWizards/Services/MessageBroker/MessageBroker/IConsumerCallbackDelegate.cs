namespace SandlotWizards.MessageBroker
{
    public interface IConsumerCallbackDelegate
    {
        public void Consume(string eventName, string message);
    }
}
