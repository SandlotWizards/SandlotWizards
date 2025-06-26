using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SandlotWizards.Core.Interfaces.Windows;
using SandlotWizards.Core.Services.Windows;
using SandlotWizards.SoftwareFactory.Commands;
using SandlotWizards.SoftwareFactory.Interfaces;
using SandlotWizards.SoftwareFactory.Services;
using SandlotWizards.SoftwareFactory.Services.FeatureDesign;
using SandlotWizards.SoftwareFactory.Services.FeaturePlan;
using SandlotWizards.SoftwareFactory.Services.ProjectCreate;

namespace SandlotWizards.SoftwareFactory.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSoftwareFactoryServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProjectCreateService, ProjectCreateService>();
        services.AddScoped<ProjectCreateCommand>();
        services.AddScoped<IFeatureDesignService, FeatureDesignService>();
        services.AddScoped<FeatureDesignCommand>();
        services.AddScoped<IFeaturePlanService, FeaturePlanService>();
        services.AddScoped<FeaturePlanCommand>();
        services.AddScoped<IFeatureBuildService, FeatureBuildService>();
        services.AddScoped<FeatureBuildCommand>();

        services.AddSingleton<ISoftwareFactoryWorkingFileSystemService, FileSystemService>();
        services.AddSingleton<IFileStoreFileSystemService, FileSystemService>();
        services.AddSingleton<IShellCommandService, ShellCommandService>();
        return services;
    }
}
