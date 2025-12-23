namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters.Responses;

public sealed record GetTemporaryPetByFiltersQueryResponses
{
    public GetTemporaryPetByFiltersQueryResponses(List<GetTemporaryPetByFiltersQueryResponsesDetail> petDetails, int totalPerPage)
    {
        PetDetails = petDetails;
        TotalPerPage = totalPerPage;
    }

    public List<GetTemporaryPetByFiltersQueryResponsesDetail> PetDetails { get; init; }

    public int TotalPerPage { get; init; }
}