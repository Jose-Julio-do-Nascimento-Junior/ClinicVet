using ClinicVet.Core.Infra.Services.Http.Helpers;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaResponses;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Services.Services.v1;

public sealed class TemporaryAgendaService : ITemporaryAgendaService
{
    private const string ServiceName = nameof(TemporaryAgendaService);

    private readonly ILogger<TemporaryAgendaService> _logger;
    private readonly IAgendaClient _agendaClient;

    public TemporaryAgendaService(ILogger<TemporaryAgendaService> logger, IAgendaClient agendaClient)
    {
        _logger = logger;
        _agendaClient = agendaClient;
    }

    public async Task<TemporaryAgendaResponseDto> GetTemporaryAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _agendaClient.GetTemporaryAgendaByFiltersAsync(filters, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return default!;
        }

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return result!.Content!.ParseRefitObjectResponseJson<TemporaryAgendaResponseDto>()!;
    }
}