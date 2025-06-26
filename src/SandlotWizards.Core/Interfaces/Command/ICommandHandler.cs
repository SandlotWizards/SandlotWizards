using System.Threading;
using System.Threading.Tasks;

namespace SandlotWizards.Core.Interfaces.Command
{
    /// <summary>
    /// Defines a handler for a command of type <typeparamref name="TCommand"/>.
    /// Implementations should encapsulate the execution logic for the specified command.
    /// </summary>
    /// <typeparam name="TCommand">The type of command to handle.</typeparam>
    /// <remarks>
    /// This interface is typically used to support command execution in CLI or service layers,
    /// and can be used with dependency injection to decouple command logic from invocation logic.
    /// </remarks>
    public interface ICommandHandler<TCommand>
    {
        /// <summary>
        /// Handles the execution of the given command.
        /// </summary>
        /// <param name="request">The command to be processed.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Handle(TCommand request, CancellationToken cancellationToken);
    }
}
