namespace ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;

public sealed record AgendaResponseDto
{
    public AgendaResponseDto(List<BaseAgendaDto> agendaResponseDetail, int totalPerPage)
    {
        AgendaResponseDetail = agendaResponseDetail;
        TotalPerPage = totalPerPage;
    }

    public List<BaseAgendaDto> AgendaResponseDetail { get; init; }

    public int TotalPerPage { get; init; }
}