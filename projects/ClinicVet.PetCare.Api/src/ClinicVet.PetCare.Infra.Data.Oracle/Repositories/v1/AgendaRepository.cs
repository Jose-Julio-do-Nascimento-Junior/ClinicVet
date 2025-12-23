using ClinicVet.Core.Infra.Data.OracleSql.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Repositories;
using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Fixeds.v1;
using ClinicVet.PetCare.Domain.Helper.v1;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Oracle.Builder.v1;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ClinicVet.PetCare.Infra.Data.Oracle.Repositories.v1;

public sealed class AgendaRepository : IAgendaRepository
{
    private const string RepositoryName = nameof(AgendaRepository);

    private readonly OracleSqlSettings _settings;
    private readonly ILogger<AgendaRepository> _logger;

    public AgendaRepository(OracleSqlSettings settings, ILogger<AgendaRepository> logger)
    {
        _settings = settings;
        _logger = logger;
    }

    public async Task<IEnumerable<AgendaDto>> GetAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, filters, AgendaQueryBuilder.GetAgendas);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new()
        {
              new OracleParameter { ParameterName = Constants.OracleOwnerDocInput, Value = filters.Document, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input },
              new OracleParameter { ParameterName = Constants.OracleStatusInput, Value = filters.Status, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input }
        };

        OracleHelper.GetBaseOracleParameters(ref oracleParameters, filters.Take, filters.Skip);

        var command = connection.CreateCommand();
        command.CommandText = AgendaQueryBuilder.GetAgendas;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        List<AgendaDto> result = new();

        await using (var reader = await command.ExecuteReaderAsync(cancellationToken))
        {
            while (await reader.ReadAsync())
            {
                result.Add(new AgendaDto
                {
                    AppointmentAt = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleAppointmentAtColumn),
                    Reason = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleRasonColumn),
                    AgendaStatusType = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleAgendaStatusTypeColumn),
                    Pet = new()
                    {
                        Name = OracleHelper.GetValueFromReader<string>(reader, Constants.OraclePetAgendaColumn),
                        Specie = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleSpeciePetAgendaColumn),
                    },
                    PetOwner = new()
                    {
                        OwnerType = OracleHelper.GetEnumFromReader<PetOwnerType>(reader, Constants.OracleOwnerTypeAgendaColumn),
                        Contact = new()
                        {
                            Phone = OracleHelper.GetValueFromReader<string>(reader, Constants.OraclePhoneOwnerAgendaColumn),
                        },
                        Document = new()
                        {
                            Code = OracleHelper.GetValueFromReader<string>(reader, Constants.OracleDocumentOwnerAgendaColumn),
                            Type = OracleHelper.GetEnumFromReader<DocumentType>(reader, Constants.OracleDocumentTypeAgendaColumn)
                        },
                    },
                });
            }
        }

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    public async Task<string?> CreateAgendaAsync(AgendaParameterDto agendaParameter, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, agendaParameter, AgendaQueryBuilder.CreateAgenda);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new();

        CreateAndUpdateAgendaParametersAsync(oracleParameters, agendaParameter);

        var command = connection.CreateCommand();
        command.CommandText = AgendaQueryBuilder.CreateAgenda;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        await command.ExecuteNonQueryAsync();
        var result = command.Parameters[Constants.OracleMessageOutput].Value!.ToString();

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    public async Task<string?> UpdateAgendaAsync(AgendaParameterDto agendaParameter, CancellationToken cancellationToken)
    {
        var logIdentifier = LogHelper.GetLogIdentifier(RepositoryName, agendaParameter, AgendaQueryBuilder.UpdateAgenda);

        _logger.LogInformation(LogTemplate.StartDbQuery, logIdentifier);

        using var connection = new OracleConnection(_settings.ConnectionString);

        await connection.OpenAsync(cancellationToken);

        List<OracleParameter> oracleParameters = new();

        CreateAndUpdateAgendaParametersAsync(oracleParameters, agendaParameter);

        var command = connection.CreateCommand();
        command.CommandText = AgendaQueryBuilder.UpdateAgenda;
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddRange(oracleParameters.ToArray());

        await command.ExecuteNonQueryAsync();
        var result = command.Parameters[Constants.OracleMessageOutput].Value!.ToString();

        _logger.LogInformation(LogTemplate.EndDbQuery, logIdentifier);

        return result;
    }

    private static void CreateAndUpdateAgendaParametersAsync( List<OracleParameter> oracleParameters, AgendaParameterDto agendaParameter)
    {
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleAppointmentAtInput, Value = agendaParameter.AppointmentAt, OracleDbType = OracleDbType.Date, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleRasonInput, Value = agendaParameter.Reason, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleStatusInput, Value = agendaParameter.Status, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OraclePetAgendaInput, Value = agendaParameter.PetName, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleSpeciePetAgendaInput, Value = agendaParameter.PetSpecie, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleDocumentOwnerAgenda, Value = agendaParameter.OwnerDocument, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OraclePhoneOwnerAgendaInput, Value = agendaParameter.OwnerPhone, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleOwnerTypeAgendaInput, Value = agendaParameter.OwnerType, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleDocumentTypeAgendaInput, Value = agendaParameter.DocumentType, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Input });
        oracleParameters.Add(new OracleParameter { ParameterName = Constants.OracleMessageOutput, OracleDbType = OracleDbType.Varchar2, Direction = ParameterDirection.Output, Size = 200 });
    }
}