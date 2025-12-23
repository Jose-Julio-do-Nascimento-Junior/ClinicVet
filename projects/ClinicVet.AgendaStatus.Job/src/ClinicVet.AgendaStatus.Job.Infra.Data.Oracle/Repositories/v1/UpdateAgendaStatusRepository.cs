using ClinicVet.AgendaStatus.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.AgendaStatus.Job.Domain.Fixeds.v1;
using ClinicVet.AgendaStatus.Job.Domain.Helpers.v1;
using ClinicVet.AgendaStatus.Job.Domain.Resources.v1;
using ClinicVet.AgendaStatus.Job.Infra.Data.Oracle.Builder.v1;
using ClinicVet.Core.Infra.Data.OracleSql.Models;
using Dapper;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace ClinicVet.AgendaStatus.Job.Infra.Data.Oracle.Repositories.v1;

public sealed class UpdateAgendaStatusRepository : IUpdateAgendaStatusRepository
{
    private const string RepositoryName = nameof(UpdateAgendaStatusRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<UpdateAgendaStatusRepository> _logger;

    public UpdateAgendaStatusRepository(OracleSqlSettings settings, ILogger<UpdateAgendaStatusRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task UpdateAgendaStatusAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartDbQuery, RepositoryName);

        await using var connection = new OracleConnection(_settings.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await connection.ExecuteAsync(UpdateAgendaStatusQueryBuilder.UpdateAgendaStatus,
        new
        {
           absent = AgendaStatusType.Absent.DataBaseValue(),
           scheduled = AgendaStatusType.Scheduled.DataBaseValue()
        });

        _logger.LogInformation(LogTemplate.EndDbQuery, RepositoryName);
    }
}