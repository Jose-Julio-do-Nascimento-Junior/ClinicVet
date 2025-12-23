using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaResponses;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Services;

public interface ITemporaryAgendaService
{
    Task<TemporaryAgendaResponseDto> GetTemporaryAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken);
}