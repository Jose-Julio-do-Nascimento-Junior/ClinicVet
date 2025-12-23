using ClinicVet.PetCare.Domain.Dtos.v1.CreatePet;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePet;

namespace ClinicVet.PetCare.Domain.Contracts.v1.Services;

public interface IPetService
{
    Task<PetResponseDto>GetPetsAsync(PetByFiltersDto filters, CancellationToken cancellationToken);

    Task<string?> CreatePetAsync(CreatePetDto createPetDto, CancellationToken cancellationToken);

    Task<string?> UpdatePetAsync(UpdatePetDto updatePetDto, CancellationToken cancellationToken);
}