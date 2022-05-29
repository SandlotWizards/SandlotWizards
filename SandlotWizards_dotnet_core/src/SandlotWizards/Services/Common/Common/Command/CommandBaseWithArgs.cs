namespace SandlotWizards.Common.Command
{
    public class CommandBaseWithArgs<T> : CommandBase
    {
        public T AppArgs { get; set; }
    }
}
