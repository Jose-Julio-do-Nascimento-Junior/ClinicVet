using ClinicVet.PetCare.Domain.Dtos;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;

public sealed class GetAgendaByFiltersQuery : BaseFiltersQueryDto
{
    public string? Document { get; set; }

    public string? Status { get; set; }
}