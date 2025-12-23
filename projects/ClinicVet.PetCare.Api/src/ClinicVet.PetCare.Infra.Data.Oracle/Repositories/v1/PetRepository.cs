using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Helper.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ClinicVet.PetCare.Infra.Data.Oracle.Repositories.v1;

public sealed class PetRepository : IPetRepository
{
    private const string RepositoryName = nameof(PetRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<PetRepository> _logger;

    public PetRepository(OracleSqlSettings settings, ILogger<PetRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<IEnumerable<PetDto>> GetPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, filters, PetQueryBuilder.GetPets);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new()
        {
              new OracleParameter { ParameterName = Constants.OracleOwnerDocInput, Value = filters.Document, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input }
        };

        OracleHelper.GetBaseOracleParameters(ref oracleParameters, filters.Take, filters.Skip);

        var command = connection.CreateCommand();
        command.CommandText = PetQueryBuilder.GetPets;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        List<PetDto> result = new();

        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync())
            {
                result.Add(new PetDto
                {
                    PetOwner = new()
                    {
                        OwnerName = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleOwnerNameColumn),
                        Document = new()
                        {
                            Code = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleOwnerDocumentColumn),
                            Type = OracleHelper.GetEnumFromReader<DocumentType>(reader, Constants.OracleDocTypeColumn),
                        }
                    },
                    Name = OracleHelper.GetValueFromReader<string>(reader,Constants.OraclePetNameColumn),
                    Specie = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleSpecieColumn),
                    Breed = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleBreedColumn),
                    BirthDate = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleBirthDateColumn),
                });
            }
        }

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    public async Task<string?> CreatePetAsync(PetParameterDto petParameter, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, petParameter, PetQueryBuilder.CreatePet);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new();

        CreateAndUpdatePetParametersAsync(oracleParameters, petParameter);

        var command = connection.CreateCommand();
        command.CommandText = PetQueryBuilder.CreatePet;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        await command.ExecuteNonQueryAsync();
        var result = command.Parameters[Constants.OracleMessageOutput].Value!.ToString();

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    public async Task<string?> UpdatePetAsync(PetParameterDto petParameter, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, petParameter, PetQueryBuilder.UpdatePet);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new();

        CreateAndUpdatePetParametersAsync( oracleParameters, petParameter);

        var command = connection.CreateCommand();
        command.CommandText = PetQueryBuilder.UpdatePet;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        await command.ExecuteNonQueryAsync();
        var result = command.Parameters[Constants.OracleMessageOutput].Value!.ToString();

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    private static void CreateAndUpdatePetParametersAsync(List<OracleParameter> oracleParameters, PetParameterDto petParameter)
    {
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleNameInput, Value = petParameter.Name, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleSpeciesInput, Value = petParameter.Specie, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleBreedInput, Value = petParameter.Breed, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleBirthDateInput, Value = petParameter.BirthDate, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleOwnerDocInput, Value = petParameter.PetOwnerDocument, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleDocTypeInput, Value = petParameter.DocumentType, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleMessageOutput, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 200 });
    }
}