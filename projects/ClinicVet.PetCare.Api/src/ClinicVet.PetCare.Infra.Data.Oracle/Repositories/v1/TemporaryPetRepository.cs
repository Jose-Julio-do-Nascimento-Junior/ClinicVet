using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetByFilters;
using ClinicVet.PetCare.Domain.Helper.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;
using Dapper;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;

namespace ClinicVet.PetCare.Infra.Data.Oracle.Repositories.v1;

public sealed class TemporaryPetRepository : ITemporaryPetRepository
{
    private const string RepositoryName = nameof(TemporaryPetRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<TemporaryPetRepository> _logger;

    public TemporaryPetRepository(OracleSqlSettings settings, ILogger<TemporaryPetRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<IEnumerable<TemporaryPetDto>> GetTemporaryPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, filters, PetQueryBuilder.GetTemporaryPets);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        await using var connection = new OracleConnection(_settings.ConnectionString);
        await connection.OpenAsync(cancellationToken);

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return await connection.QueryAsync<TemporaryPetDto>(
             PetQueryBuilder.GetTemporaryPets,
               new
               {
                   document = filters.Document,
                   offset = filters.Skip,
                   limit = filters.Take,
               }
        );
    }
}