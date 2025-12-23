using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetResponse;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Services;

public interface ITemporaryPetService
{
    Task<TemporaryPetResponse> GetTemporaryPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken);
}