using ClinicVet.Core.Infra.Services.Http.Helpers;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePetOwner;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using ClinicVet.PetCare.Infra.Data.Services.Extensions.v1;
using ClinicVet.PetCare.Infra.Data.Services.Models.v1;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace ClinicVet.PetCare.Infra.Data.Services.Services.v1;

public sealed class PetOwnerService : IPetOwnerService
{
    private const string ServiceName = nameof(PetOwnerService);

    private readonly ILogger<PetOwnerService> _logger;
    private readonly IPetOwnerClient _petOwnerClient;
    private readonly IDistributedCache _distributedCache;
    private readonly CacheSettings _cacheSettings;

    public PetOwnerService(
        ILogger<PetOwnerService> logger, 
        IPetOwnerClient petOwnerClient, 
        IDistributedCache distributedCache,
        CacheSettings cacheSettings)
    {
        _logger = logger;
        _petOwnerClient = petOwnerClient;
        _distributedCache = distributedCache;
        _cacheSettings = cacheSettings;
    }

    public async Task<PetOwnerResponseDto> GetPetOwnersAsync(PetOwnerByFiltersDto filters, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var filtersKey = JsonSerializer.Serialize(filters);
        var cacheKey = $"{Constants.PetOWnerKey}:{filtersKey.GetHashCode()}";

        var petOwnerCache = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);

        if (!string.IsNullOrWhiteSpace(petOwnerCache))
        {
            var cachedResult = JsonSerializer.Deserialize<PetOwnerResponseDto>(petOwnerCache)!;
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);
            return cachedResult;
        }

        var httpResponseMessage = await _petOwnerClient.GetPetOwnerByFiltersAsync(filters, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return default!;
        }

        var petOwnerResponse = result!.Content!.ParseRefitObjectResponseJson<PetOwnerResponseDto>()!;

        await CacheExtensions.DistributedCacheAsync(_distributedCache, _cacheSettings, cacheKey, petOwnerResponse, cancellationToken);

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return petOwnerResponse;
    }

    public async Task<string?> CreatePetOwnerAsync(CreatePetOwnerDto createPetOwnerDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _petOwnerClient.CreatePetOwnerAsync(createPetOwnerDto, cancellationToken);
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

    public async Task<string?> UpdatePetOwnerAsync(UpdatePetOwnerDto updatePetOwnerDto, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _petOwnerClient.UpdatePetOwnerAsync(updatePetOwnerDto, cancellationToken);
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