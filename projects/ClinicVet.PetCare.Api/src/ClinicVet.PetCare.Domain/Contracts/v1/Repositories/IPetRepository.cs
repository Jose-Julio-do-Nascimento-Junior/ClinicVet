using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.PetParameter;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Repositories;

public interface IPetRepository
{
    Task<IEnumerable<PetDto>> GetPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken);

    Task<string?> CreatePetAsync(PetParameterDto petParameter, CancellationToken cancellationToken);

    Task<string?> UpdatePetAsync(PetParameterDto petParameter, CancellationToken cancellationToken);
}