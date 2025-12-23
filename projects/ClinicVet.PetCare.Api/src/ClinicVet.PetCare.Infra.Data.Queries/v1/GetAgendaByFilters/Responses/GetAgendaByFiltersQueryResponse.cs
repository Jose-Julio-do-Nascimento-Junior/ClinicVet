namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters.Responses;

public sealed record GetAgendaByFiltersQueryResponse
{
    public GetAgendaByFiltersQueryResponse(List<GetAgendaByFiltersQueryResponseDetail> agendaResponseDetail, int totalPerPage)
    {
        AgendaResponseDetail = agendaResponseDetail;
        TotalPerPage = totalPerPage;
    }

    public List<GetAgendaByFiltersQueryResponseDetail> AgendaResponseDetail { get; init; }

    public int TotalPerPage { get; init; }
}