using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaByFilters;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Repositories;

public interface ITemporaryAgendaRepository
{
    Task<IEnumerable<TemporaryAgendaDto>> GetTemporaryAgendasAsync(GetAgendaByFiltersDto filters, CancellationToken cancellationToken);
}