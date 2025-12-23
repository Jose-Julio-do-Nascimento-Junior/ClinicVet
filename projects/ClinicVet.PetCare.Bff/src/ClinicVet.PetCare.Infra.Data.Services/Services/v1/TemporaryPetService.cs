using ClinicVet.Core.Infra.Services.Http.Helpers;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetResponse;
using ClinicVet.PetCare.Domain.Resources.v1;
using ClinicVet.PetCare.Infra.Data.Services.Clients.v1;
using Microsoft.Extensions.Logging;

namespace ClinicVet.PetCare.Infra.Data.Services.Services.v1;

public sealed class TemporaryPetService : ITemporaryPetService
{
    private const string ServiceName = nameof(TemporaryPetService);

    private readonly ILogger<TemporaryPetService> _logger;
    private readonly IPetClient _petClient;

    public TemporaryPetService(ILogger<TemporaryPetService> logger, IPetClient petClient)
    {
        _logger = logger;
        _petClient = petClient;
    }

    public async Task<TemporaryPetResponse> GetTemporaryPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken)
    {
        _logger.LogInformation(LogTemplate.StartHandler, ServiceName);

        var httpResponseMessage = await _petClient.GetTemporaryPetsByFiltersAsync(filters, cancellationToken);
        var content = await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken);
        var result = content.ParseJsonToResponse();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            _logger.LogInformation(LogTemplate.EndHandler, ServiceName, result!.Notifications);
            return default!;
        }

        _logger.LogInformation(LogTemplate.EndHandler, ServiceName, string.Empty);

        return result!.Content!.ParseRefitObjectResponseJson<TemporaryPetResponse>()!;
    }
}