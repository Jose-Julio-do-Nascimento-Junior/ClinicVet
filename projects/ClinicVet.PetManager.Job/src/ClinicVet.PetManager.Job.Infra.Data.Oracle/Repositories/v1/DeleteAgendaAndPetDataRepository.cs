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

public sealed class DeleteAgendaAndPetDataRepository : IDeleteAgendaAndPetDataRepository
{
    private const string RepositoryName = nameof(DeleteAgendaAndPetDataRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<DeleteAgendaAndPetDataRepository> _logger;

    public DeleteAgendaAndPetDataRepository(OracleSqlSettings settings, ILogger<DeleteAgendaAndPetDataRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task DeleteAgendaAndPetDataAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartDbQuery, RepositoryName);

        await using var connection = new OracleConnection(_settings.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        await connection.ExecuteAsync(DeleteAgendaAndPetDataBuilder.DeleteAgendaAndPetData,
             new { ownerType = OwnerType.Temporary.DataBaseValue() });

        _logger.LogInformation(LogTemplate.EndDbQuery, RepositoryName);
    }
}