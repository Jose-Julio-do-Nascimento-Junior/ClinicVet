using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.PetManager.Job.Domain.Resources.v1;
using ClinicVet.PetManager.Job.Infra.Data.Oracle.Repositories.v1;
using ClinicVet.PetManager.Job.Infra.Data.Services.Models.v1;
using ClinicVet.PetManager.Job.Workers.v1;
using Quartz;

namespace ClinicVet.PetManager.Job;

public static class Bootstrapper
{
    public static IServiceCollection AddApplicationBootstrapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPetManageRepository, PetManagerRepository>();
        services.AddScoped<IAgendaManagerRepository, AgendaManagerRepository>();
        services.AddScoped<IDeleteAgendaAndPetDataRepository, DeleteAgendaAndPetDataRepository>();

        return services;
    }

    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        var oracleSettings = configuration.GetSection(OracleSqlSettings.SessionName).Get<OracleSqlSettings>();
        ArgumentNullException.ThrowIfNull(oracleSettings);
        services.AddSingleton(oracleSettings);

        return services;
    }

    public static void AddQuartzCronExpression(this IServiceCollection services, IConfiguration configuration)
    {
        var cronExpression = configuration.GetValue<string>(ApplicationSettings.SessionName);

        var cronValue = cronExpression ?? throw new InvalidOperationException(Messages.CronExpressionNotFound);

        services.AddQuartz(quartz =>
        {
            var jobKey = new JobKey(ApplicationSettings.JobKey);

            quartz.AddJob<PetManagerWorkerHandler>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(ApplicationSettings.PetManageTrigger)
                .WithCronSchedule(cronValue));
        });

        services.AddQuartzHostedService(quartz => quartz.WaitForJobsToComplete = true);
    }
}