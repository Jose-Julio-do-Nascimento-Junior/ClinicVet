namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters.Responses;

public sealed record GetPetByFiltersQueryResponse
{
    public GetPetByFiltersQueryResponse(List<GetPetByFiltersQueryResponseDetail>? petDetails, int totalPerPage)
    {
        PetDetails = petDetails;
        TotalPerPage = totalPerPage;
    }

    public List<GetPetByFiltersQueryResponseDetail>? PetDetails { get; init; }

    public int TotalPerPage { get; init; }
}