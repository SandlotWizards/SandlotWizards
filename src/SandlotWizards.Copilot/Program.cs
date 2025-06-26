using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SandlotWizards.AiPipelines.Commands;
using SandlotWizards.AiPipelines.Extensions;
using SandlotWizards.CommandLineParser.Core;
using SandlotWizards.Core.Logging;
using SandlotWizards.OpenAi.Extensions;
using SandlotWizards.SoftwareFactory.Commands;
using SandlotWizards.SoftwareFactory.Extensions;
using Serilog;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddUserSecrets<Program>(optional: true);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddAiPipelineServices(hostContext.Configuration);
        services.AddOpenAiServices(hostContext.Configuration);
        services.AddSoftwareFactoryServices(hostContext.Configuration);
    })
    .UseSerilog((context, services, configuration) =>
    {
        //configuration.WriteTo.Console();
    });

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Starting SandlotWizard CLI...");

await CommandLineApp.Run(args, registry =>
{
    registry.Register("project", "create", app.Services.GetRequiredService<ProjectCreateCommand>());
    registry.Register("feature", "design", app.Services.GetRequiredService<FeatureDesignCommand>());
    registry.Register("feature", "plan", app.Services.GetRequiredService<FeaturePlanCommand>());
    registry.Register("feature", "build", app.Services.GetRequiredService<FeatureBuildCommand>());
    registry.Register("rag", "embed", app.Services.GetRequiredService<RagEmbedCommand>());
}, app.Services);