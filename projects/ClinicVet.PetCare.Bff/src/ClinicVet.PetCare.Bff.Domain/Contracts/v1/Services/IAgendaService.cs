using ClinicVet.PetCare.Domain.Dtos.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdateAgenda;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Services;

public interface IAgendaService
{
    Task<AgendaResponseDto>GetAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken);

    Task<string?>CreateAgendaAsync(CreateAgendaDto createAgendaDto, CancellationToken cancellationToken);

    Task<string?>UpdateAgendaAsync(UpdateAgendaDto updateAgendaDto, CancellationToken cancellationToken);
}