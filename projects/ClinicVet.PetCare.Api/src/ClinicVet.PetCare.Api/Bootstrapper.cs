using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Infra.Data.Oracle.Repositories.v1;

namespace ClinicVet.PetCare.Api;

public static  class Bootstrapper
{
    public static IServiceCollection AddApplicationBootstrapper(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddScoped<IPetOwnerRepository, PetOwnerRepository>();
        services.AddScoped<IPetRepository, PetRepository>();
        services.AddScoped<IAgendaRepository, AgendaRepository>();
        services.AddScoped<ITemporaryAgendaRepository, TemporaryAgendaRepository>();
        services.AddScoped<ITemporaryPetRepository, TemporaryPetRepository>();

        return services;
    }

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var oracleSettings = configuration.GetSection(OracleSqlSettings.SessionName).Get<OracleSqlSettings>();
        ArgumentNullException.ThrowIfNull(oracleSettings);
        services.AddSingleton(oracleSettings);

        return services;
    }
}