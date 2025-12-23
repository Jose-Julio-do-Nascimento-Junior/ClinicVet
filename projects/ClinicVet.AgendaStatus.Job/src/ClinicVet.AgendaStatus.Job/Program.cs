using ClinicVet.AgendaStatus.Job;
using ClinicVet.Core.Domain;
using ClinicVet.Core.Infra.Data.OracleSql;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    var configuration = context.Configuration;
    services.AddDomainContext(configuration);
    services.AddApplicationBootstrapper(configuration);
    services.AddOracleContextWithDapper(configuration);
    services.AddSettings(configuration);
    services.AddQuartzCronExpression(configuration);
});

await builder
   .Build()
   .RunAsync();