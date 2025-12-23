using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;

namespace ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryPetResponse;

public sealed record TemporaryPetResponse
{
    public TemporaryPetResponse(List<BasePetDto> petDetails, int totalPerPage)
    {
        PetDetails = petDetails;
        TotalPerPage = totalPerPage;
    }

    public List<BasePetDto> PetDetails { get; init; }

    public int TotalPerPage { get; init; }
}