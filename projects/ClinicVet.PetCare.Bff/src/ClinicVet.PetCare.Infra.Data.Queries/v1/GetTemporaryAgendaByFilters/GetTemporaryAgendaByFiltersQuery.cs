using ClinicVet.PetCare.Domain.Dtos;
using ClinicVet.PetCare.Domain.Dtos.v1;

namespace ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;

public sealed class GetTemporaryAgendaByFiltersQuery : BaseFiltersQueryDto
{
    public string? Document { get; set; }
}