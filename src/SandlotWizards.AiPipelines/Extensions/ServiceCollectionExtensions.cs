using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SandlotWizards.ActionLogger;
using SandlotWizards.ActionLogger.Interfaces;
using SandlotWizards.ActionLogger.Services;
using SandlotWizards.AiPipelines.Commands;
using SandlotWizards.AiPipelines.Interfaces;
using SandlotWizards.AiPipelines.Services;
using SandlotWizards.Core.Configuration;
using SandlotWizards.Core.Interfaces.Windows;
using SandlotWizards.Core.Services.Windows;

namespace SandlotWizards.AiPipelines.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAiPipelineServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IActionLoggerService, ActionLoggerService>();
        var logger = services.BuildServiceProvider().GetRequiredService<IActionLoggerService>();
        ActionLog.Initialize(logger);

        services.Configure<GitHubConfig>(configuration.GetSection(nameof(GitHubConfig)));
        services.AddScoped<IDeveloperFileSystemService, FileSystemService>();

        services.AddSingleton<RagEmbedCommand>();

        services.AddSingleton<IRagRetriever, RagRetriever>();
        services.AddSingleton<IRagEmbedService, RagEmbedService>();
        services.AddSingleton<IRagEmbedder>(sp => (IRagEmbedder)sp.GetRequiredService<IRagRetriever>());

        //services.AddScoped<IAiExecutionService>(provider =>
        //{
        //    var httpClient = provider.GetRequiredService<IHttpClientFactory>().CreateClient();
        //    var apiKey = configuration["OpenAi:ApiKey"] ?? throw new InvalidOperationException("Missing OpenAi:ApiKey config value");
        //    return new AiExecutionService(httpClient, apiKey);
        //});

        //services.AddScoped<IAiPipelineService, ChatGptService>();
        //services.AddHttpClient<ChatGptService>();


        return services;
    }
}
