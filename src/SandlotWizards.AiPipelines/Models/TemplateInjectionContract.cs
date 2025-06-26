namespace SandlotWizards.AiPipelines.Models
{
    /// <summary>
    /// Defines the contract for executing a template injection using AI-generated logic.
    /// </summary>
    public class TemplateInjectionContract
    {
        // 🧠 Injection Context

        /// <summary>
        /// The named code block to inject into, marked by <copilot:inject name="...">.
        /// </summary>
        public string InjectBlockName { get; set; } = "ExecuteCoreLogic";

        /// <summary>
        /// The full path to the file containing the target injection block.
        /// </summary>
        public string InjectFilePath { get; set; } = string.Empty;

        /// <summary>
        /// The path to the prompt that will seed the AI's system context.
        /// </summary>
        public string PromptPath { get; set; } = string.Empty;

        /// <summary>
        /// Defines how the AI-generated logic should be applied (e.g. replace, append).
        /// </summary>
        public string Mode { get; set; } = "replace";

        // 📄 Documentation Inputs

        /// <summary>
        /// Path to the markdown design document that describes the requested logic.
        /// </summary>
        public string DesignDocPath { get; set; } = string.Empty;

        /// <summary>
        /// Path to the canonical architectural spec document used for AI context.
        /// </summary>
        public string SpecDocPath { get; set; } = string.Empty;

        // 🏗️ Identity and Target Context

        /// <summary>
        /// The solution identifier.
        /// </summary>
        public string SolutionId { get; set; } = string.Empty;

        /// <summary>
        /// The project under which this template lives.
        /// </summary>
        public string ProjectName { get; set; } = string.Empty;

        /// <summary>
        /// Logical name of the service or class being injected.
        /// </summary>
        public string ServiceName { get; set; } = string.Empty;
    }
}
