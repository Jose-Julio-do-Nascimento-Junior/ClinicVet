using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaByFilters;
using ClinicVet.PetCare.Domain.Helper.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;
using Dapper;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace ClinicVet.PetCare.Infra.Data.Oracle.Repositories.v1;

public sealed class TemporaryAgendaRepository : ITemporaryAgendaRepository
{
    private const string RepositoryName = nameof(TemporaryAgendaRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<TemporaryAgendaRepository> _logger;

    public TemporaryAgendaRepository(OracleSqlSettings settings, ILogger<TemporaryAgendaRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<IEnumerable<TemporaryAgendaDto>> GetTemporaryAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, filters, AgendaQueryBuilder.GetTemporaryAgendas);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        await using var connection = new OracleConnection(_settings.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return await connection.QueryAsync<TemporaryAgendaDto>(
            AgendaQueryBuilder.GetTemporaryAgendas,
            new 
            {
                document = filters.Document,
                offset = filters.Skip,
                limit = filters.Take,
            }
        );
    }
}