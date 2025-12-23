namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters.Responses;

public sealed record GetTemporaryAgendaByFiltersQueryResponse
{
    public GetTemporaryAgendaByFiltersQueryResponse(List<GetTemporaryAgendaByFiltersQueryResponseDetail> agendaResponseDetail, int totalPerPage)
    {
        AgendaResponseDetail = agendaResponseDetail;
        TotalPerPage = totalPerPage;
    }

    public List<GetTemporaryAgendaByFiltersQueryResponseDetail> AgendaResponseDetail { get; init; }

    public int TotalPerPage { get; init; }
}