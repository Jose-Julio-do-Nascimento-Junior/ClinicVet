using ClinicVet.Core.Infra.Services.Http.Helpers;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePet;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePet;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Extensions.v1;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ClinicVet.PetCare.Infra.Data.Services.Services.v1;

public sealed class PetService : IPetService
{
    private const string ServiceName = nameof(PetService);

    private readonly ILogger<PetService> _logger;
    private readonly IPetClient _petClient;
    private readonly IDistributedCache _distributedCache;
    private readonly CacheSettings _cacheSettings;

    public PetService(
        ILogger<PetService> logger,
        IPetClient petClient,
        IDistributedCache distributedCache,
        CacheSettings cacheSettings)
    {
        _logger = logger;
        _petClient = petClient;
        _distributedCache = distributedCache;
        _cacheSettings = cacheSettings;
    }

    public async Task<PetResponseDto> GetPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var filtersKey = JsonSerializer.Serialize(filters);
        var cacheKey = $"{Constants.PetKey}:{filtersKey.GetHashCode()}";

        var petCache = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrWhiteSpace(petCache))
        {
            var cachedResult = JsonSerializer.Deserialize<PetResponseDto>(petCache)!;
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);
            return cachedResult;
        }

        var httpResponseMessage = await _petClient.GetPetsByFiltersAsync(filters, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return default!;
        }
        var petResponse = result!.Content!.ParseRefitObjectResponseJson<PetResponseDto>()!;

        await CacheExtensions.DistributedCacheAsync(_distributedCache, _cacheSettings, cacheKey, petResponse, cancellationToken);
       
        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return petResponse;
    }

    public async Task<string?> CreatePetAsync(CreatePetDto createPetDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _petClient.CreatePetAsync(createPetDto, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return null;
        }

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return result!.Content!.ParseRefitObjectResponseJson<string>()!;
    }

    public async Task<string?> UpdatePetAsync(UpdatePetDto updatePetDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _petClient.UpdatePetAsync(updatePetDto, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return null;
        }

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return result!.Content!.ParseRefitObjectResponseJson<string>()!;
    }
}