namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters.Responses;

public sealed record GetPetOwnerByFiltersQueryResponse
{
    public GetPetOwnerByFiltersQueryResponse(List<GetPetOwnerByFiltersQueryResponseDetail> petOwner, int totalPerPage)
    {
        PetOwners = petOwner;
        TotalPerPage = totalPerPage;
    }

    public List<GetPetOwnerByFiltersQueryResponseDetail> PetOwners { get; init; }

    public int TotalPerPage { get; init; }
}