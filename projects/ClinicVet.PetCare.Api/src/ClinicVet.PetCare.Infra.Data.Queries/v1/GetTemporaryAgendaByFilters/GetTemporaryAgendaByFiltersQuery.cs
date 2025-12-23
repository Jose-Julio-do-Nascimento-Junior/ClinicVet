using ClinicVet.PetCare.Domain.Dtos;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;

public sealed class GetTemporaryAgendaByFiltersQuery : BaseFiltersQueryDto
{
    public string? Document { get; set; }
}