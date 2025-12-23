using ClinicVet.PetCare.Domain.Dtos.v1.PetOwnerParameter;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Repositories;

public interface IPetOwnerRepository
{
    Task<IEnumerable<PetOwnerDto>> GetPetOwnersAsync(PetOwnerByFiltersDto filters, CancellationToken cancellationToken);

    Task<string?>CreatePetOwnerAsync(PetOwnerParametersDto petOwnerParameters, CancellationToken cancellationToken);

    Task<string?>UpdatePetOwnerAsync(PetOwnerParametersDto petOwnerParameters, CancellationToken cancellationToken);
}