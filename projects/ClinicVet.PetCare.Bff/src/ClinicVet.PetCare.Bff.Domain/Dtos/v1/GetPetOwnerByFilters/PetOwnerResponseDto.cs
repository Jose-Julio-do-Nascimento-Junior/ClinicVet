namespace ClinicVet.PetCare.Domain.Dtos.v1.GetPetOwnerByFilters;

public sealed record PetOwnerResponseDto
{
    public PetOwnerResponseDto(List<PetOwnerDto> petOwners, int totalPerPage)
    {
        PetOwners = petOwners;
        TotalPerPage = totalPerPage;
    }

    public List<PetOwnerDto> PetOwners { get; init; }

    public int TotalPerPage { get; init; }
}