namespace SandlotWizards.AiPipelines.Abstractions
{
    /// <summary>
    /// Defines a pipeline capable of executing a specific type of AI contract.
    /// </summary>
    public interface IAiPipeline
    {
        /// <summary>
        /// The name or identifier of the supported contract type (typically the class name).
        /// </summary>
        string ContractType { get; }

        /// <summary>
        /// Executes the AI pipeline logic based on the given contract.
        /// </summary>
        /// <param name="contract">The contract instance containing execution context and metadata.</param>
        /// <param name="ct">Optional cancellation token for graceful shutdown.</param>
        Task ExecuteAsync(IAiPipelineContract contract, CancellationToken ct);
    }
}
