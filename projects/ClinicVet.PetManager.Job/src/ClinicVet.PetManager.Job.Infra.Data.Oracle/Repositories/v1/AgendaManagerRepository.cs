using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetManager.Job.Domain.Contracts.v1.Repositories;
using ClinicVet.PetManager.Job.Domain.Fixeds.v1;
using ClinicVet.PetManager.Job.Domain.Helpers.v1;
using ClinicVet.PetManager.Job.Domain.Resources.v1;
using ClinicVet.PetManager.Job.Infra.Data.Oracle.Builder.v1;
using Dapper;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace ClinicVet.PetManager.Job.Infra.Data.Oracle.Repositories.v1;

public sealed class AgendaManagerRepository : IAgendaManagerRepository
{
    private const string RepositoryName = nameof(AgendaManagerRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<AgendaManagerRepository> _logger;

    public AgendaManagerRepository(OracleSqlSettings settings, ILogger<AgendaManagerRepository> logger)
    {
        
        _settings = settings;
        _logger = logger;
    }

    public async Task<int> AgendaManagerAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartDbQuery, RepositoryName);

        await using var connection = new OracleConnection(_settings.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        _logger.LogInformation(LogTemplate.EndDbQuery, RepositoryName);

        return await connection.ExecuteAsync(AgendaManageQueryBuilder.AgendaManager,
             new { ownerType = OwnerType.Temporary.DataBaseValue() });
    }
}