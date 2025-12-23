using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePetOwner;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Services;

public interface IPetOwnerService
{
    Task<PetOwnerResponseDto>GetPetOwnersAsync(PetOwnerByFiltersDto filters, CancellationToken cancellationToken);

    Task<string?> CreatePetOwnerAsync(CreatePetOwnerDto createPetOwnerDto, CancellationToken cancellationToken);

    Task<string?> UpdatePetOwnerAsync(UpdatePetOwnerDto updatePetOwnerDto, CancellationToken cancellationToken);
}