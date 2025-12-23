using ClinicVet.PetCare.Domain.Dtos.v1.AgendaParameter;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Repositories;

public interface IAgendaRepository
{
    Task<IEnumerable<AgendaDto>> GetAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken);

    Task<string?> CreateAgendaAsync(AgendaParameterDto agendaParameter, CancellationToken cancellationToken);

    Task<string?> UpdateAgendaAsync(AgendaParameterDto agendaParameter, CancellationToken cancellationToken);
}