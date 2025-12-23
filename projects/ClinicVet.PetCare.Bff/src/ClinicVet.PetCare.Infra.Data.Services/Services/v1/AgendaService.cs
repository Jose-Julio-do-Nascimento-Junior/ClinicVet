using ClinicVet.Core.Infra.Services.Http.Helpers;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdateAgenda;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Extensions.v1;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ClinicVet.PetCare.Infra.Data.Services.Services.v1;

public sealed class AgendaService : IAgendaService
{
    private const string ServiceName = nameof(AgendaService);

    private readonly ILogger<AgendaService> _logger;
    private readonly IAgendaClient _agendaClient;
    private readonly IDistributedCache _distributedCache;
    private readonly CacheSettings _cacheSettings;

public AgendaService(
    ILogger<AgendaService> logger,
    IAgendaClient agendaClient,
    IDistributedCache distributedCache,
    CacheSettings cacheSettings)
    {
        _logger = logger;
        _agendaClient = agendaClient;
        _distributedCache = distributedCache;
        _cacheSettings = cacheSettings;
    }

    public async Task<AgendaResponseDto> GetAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var filtersKey = JsonSerializer.Serialize(filters);
        var cacheKey = $"{Constants.AgendaKey}:{filtersKey.GetHashCode()}";

        var agendaCache = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        
        if (!string.IsNullOrWhiteSpace(agendaCache))
        {
            var cachedResult = JsonSerializer.Deserialize<AgendaResponseDto>(agendaCache)!;
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);
            return cachedResult;
        }

        var httpResponseMessage = await _agendaClient.GetAgendaByFiltersAsync(filters, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);

            return default!;
        }

        var agendaResponse = result!.Content!.ParseRefitObjectResponseJson<AgendaResponseDto>()!;

        await CacheExtensions.DistributedCacheAsync(_distributedCache, _cacheSettings, cacheKey, agendaResponse, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return agendaResponse;
    }

    public async Task<string?> CreateAgendaAsync(CreateAgendaDto createAgenda, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _agendaClient.CreateAgendaAsync(createAgenda, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return null;
        }

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return result!.Content!.ParseRefitObjectResponseJson<string>();
    }

    public async Task<string?> UpdateAgendaAsync(UpdateAgendaDto updateAgendaDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _agendaClient.UpdateAgendaAsync(updateAgendaDto, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return null;
        }

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return result!.Content!.ParseRefitObjectResponseJson<string>();
    }
}