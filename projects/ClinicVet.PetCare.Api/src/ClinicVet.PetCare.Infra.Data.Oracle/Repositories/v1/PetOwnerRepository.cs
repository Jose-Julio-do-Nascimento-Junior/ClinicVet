using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Helper.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ClinicVet.PetCare.Infra.Data.Oracle.Repositories.v1;

public sealed class PetOwnerRepository : IPetOwnerRepository
{
    private const string RepositoryName = nameof(PetOwnerRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<PetOwnerRepository> _logger;

    public PetOwnerRepository(OracleSqlSettings settings, ILogger<PetOwnerRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<IEnumerable<PetOwnerDto>> GetPetOwnersAsync(PetOwnerByFiltersDto filters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, filters, PetOwnerQueryBuilder.GetPetOwners);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new()
        {
            new OracleParameter { ParameterName = Constants.OracleNameInput, Value = filters.Name, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input },
            new OracleParameter { ParameterName = Constants.OracleDocumentInput, Value = filters.Document, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input },
        };

        OracleHelper.GetBaseOracleParameters(ref oracleParameters, filters.Take, filters.Skip);

        var command = connection.CreateCommand();
        command.CommandText = PetOwnerQueryBuilder.GetPetOwners;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        List<PetOwnerDto> result = new();

        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync(cancellationToken))
            {
                result.Add(new PetOwnerDto
                {
                    Name = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleNameColumn),
                    OwnerType = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleOwnerTypeColumn),
                    CreatedAt = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleCreatedAtColumn),
                    Document = new()
                    {
                        Code = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleDocumentColumn),
                        Type = OracleHelper.GetEnumFromReader<DocumentType>(reader, Constants.OracleDocumentTypeColumn),
                    },
                    Contact = new()
                    {
                        Phone = OracleHelper.GetValueFromReader<string>(reader, Constants.OraclePhoneColumn),
                    },
                    Address = new()
                    {
                        Street = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleStreetColumn),
                        Number = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleNumberColumn),
                        City = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleCityColumn),
                        State = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleStateColumn),
                        ZipCode = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleZipCodeColumn),
                    }
                });
            }
        }

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    public async Task<string?> CreatePetOwnerAsync(PetOwnerParametersDto petOwnerParameters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, petOwnerParameters, PetOwnerQueryBuilder.CreatePetOwner);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new();
       
        CreateAndUpdateParametersAsync(oracleParameters, petOwnerParameters);

        var command = connection.CreateCommand();
        command.CommandText = PetOwnerQueryBuilder.CreatePetOwner;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        await command.ExecuteNonQueryAsync();
        var result = command.Parameters[Constants.OracleMessageOutput].Value!.ToString();

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    public async Task<string?> UpdatePetOwnerAsync(PetOwnerParametersDto petOwnerParameters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, petOwnerParameters, PetOwnerQueryBuilder.UpdatePetOwner);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new();

        CreateAndUpdateParametersAsync(oracleParameters, petOwnerParameters);

        var command = connection.CreateCommand();
        command.CommandText = PetOwnerQueryBuilder.UpdatePetOwner;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        await command.ExecuteNonQueryAsync();
        var result = command.Parameters[Constants.OracleMessageOutput].Value!.ToString();

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    private static void CreateAndUpdateParametersAsync(List<OracleParameter> oracleParameters, PetOwnerParametersDto petOwnerParameters)
    {
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleNameInput, Value = petOwnerParameters.Name, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleDocumentInput, Value = petOwnerParameters.Document, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleDocumentTypeInput, Value = petOwnerParameters.DocumentType, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleOwnerTypeInput, Value = petOwnerParameters.OwnerType, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OraclePhoneInput, Value = petOwnerParameters.Phone, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleStreetInput, Value = petOwnerParameters.Street, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleNumberInput, Value = petOwnerParameters.Number, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleCityInput, Value = petOwnerParameters.City, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleStateInput, Value = petOwnerParameters.State, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleZipCodeInput, Value = petOwnerParameters.ZipCode, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleMessageOutput, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 200 });
    }
}