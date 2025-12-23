using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;

namespace ClinicVet.PetCare.Domain.Dtos.v1.GetTemporaryAgendaResponses;

public sealed record TemporaryAgendaResponseDto
{
    public TemporaryAgendaResponseDto(List<BaseAgendaDto> agendaResponseDetail, int totalPerPage)
    {
        AgendaResponseDetail = agendaResponseDetail;
        TotalPerPage = totalPerPage;
    }

    public List<BaseAgendaDto> AgendaResponseDetail { get; init; }

    public int TotalPerPage { get; init; }
}