namespace SandlotWizards.AiPipelines.Interfaces
{
    public interface IWorkingPathResolver
    {
        string GetPromptPath();
        string GetSpecDocPath();
        string GetDesignDocPath(string serviceName);
        string GetInjectFilePath(string serviceName);
        string GetContractPath(string serviceName);
    }
}