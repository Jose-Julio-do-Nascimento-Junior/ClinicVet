using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using ClinicVet.PetCare.Infra.Data.Services.Services.v1;

namespace ClinicVet.PetCare.Bff;

public static class Bootstrapper
{
    public static IServiceCollection AddApplicationBootstrapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IAgendaService, AgendaService>();
        services.AddSingleton<IPetService, PetService>();
        services.AddSingleton<IPetOwnerService, PetOwnerService>();
        services.AddSingleton<ITemporaryAgendaService, TemporaryAgendaService>();
        services.AddSingleton<ITemporaryPetService, TemporaryPetService>();

        return services;
    }

    public static IServiceCollection AddDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        var cacheSettings = configuration.GetSection(CacheSettings.SessionName).Get<CacheSettings>();

        var minutesToExpire = configuration.GetSection(CacheOptions.TokenSessionName).Get<int?>();

        services.AddSingleton(cacheSettings!);
        services.AddSingleton(new CacheOptions(minutesToExpire.GetValueOrDefault()));

        ArgumentNullException.ThrowIfNull(cacheSettings);

        return services.AddStackExchangeRedisCache(op =>
        {
            op.Configuration = cacheSettings.ConnectionString;
            op.InstanceName = cacheSettings.InstanceName;
        });
    }
}