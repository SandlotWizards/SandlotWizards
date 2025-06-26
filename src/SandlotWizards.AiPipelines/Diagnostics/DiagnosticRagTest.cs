using SandlotWizards.AiPipelines.Interfaces;

namespace SandlotWizards.AiPipelines.Diagnostics
{
    public class DiagnosticRagTest
    {
        private readonly IRagRetriever _rag;

        public DiagnosticRagTest(IRagRetriever rag)
        {
            _rag = rag;
        }

        public async Task RunTestAsync()
        {
            var results = await _rag.QueryAsync("What is a design pattern?");
            foreach (var result in results)
            {
                Console.WriteLine($"[{result.Section}] from {result.File}:\n{result.Content}");
            }
        }
    }

}
