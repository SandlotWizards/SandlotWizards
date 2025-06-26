using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SandlotWizards.AiPipelines.Interfaces;

namespace SandlotWizards.OpenAi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOpenAiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IAiPipelineService, OpenAiService>()
            .AddHttpMessageHandler<ActionLogHttpHandler>();
        services.AddTransient<ActionLogHttpHandler>();

        return services;
    }
}
