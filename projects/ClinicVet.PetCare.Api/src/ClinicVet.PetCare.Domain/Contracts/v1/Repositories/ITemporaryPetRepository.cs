using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetByFilters;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Repositories;

public interface ITemporaryPetRepository
{
    Task<IEnumerable<TemporaryPetDto>> GetTemporaryPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken);
}