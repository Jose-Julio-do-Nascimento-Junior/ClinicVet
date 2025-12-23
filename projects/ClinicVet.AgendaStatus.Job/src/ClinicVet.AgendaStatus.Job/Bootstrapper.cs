using ClinicVet.AgendaStatus.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.AgendaStatus.Job.Domain.Resources.v1;
using ClinicVet.AgendaStatus.Job.Infra.Data.Oracle.Repositories.v1;
using ClinicVet.AgendaStatus.Job.Infra.Data.Services.Models.v1;
using ClinicVet.AgendaStatus.Job.Workers.v1;
using ClinicVet.Core.Infra.Data.OracleSql.Models;
using Quartz;

namespace ClinicVet.AgendaStatus.Job;

public static class Bootstrapper
{
    public static IServiceCollection AddApplicationBootstrapper(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUpdateAgendaStatusRepository, UpdateAgendaStatusRepository>();

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

            quartz.AddJob<AgendaStatusWorkerHandler>(opts => opts.WithIdentity(jobKey));

            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(ApplicationSettings.AgendaStatusTrigger)
                .WithCronSchedule(cronValue));
        });

        services.AddQuartzHostedService(quartz => quartz.WaitForJobsToComplete = true);
    }
}