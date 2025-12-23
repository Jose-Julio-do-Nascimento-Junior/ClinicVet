namespace ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;

public sealed record PetResponseDto
{
    public PetResponseDto(List<BasePetDto> petDetails, int totalPerPage)
    {
        PetDetails = petDetails;
        TotalPerPage = totalPerPage;
    }

    public List<BasePetDto> PetDetails { get; init; }

    public int TotalPerPage { get; init; }
}