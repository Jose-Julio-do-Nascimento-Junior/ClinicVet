namespace ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;

public sealed record GetAgendaByFiltersDto : ParametersForPaginatedDto
{
    public string? Document { get; init; }

    public string? Status { get; init; }
}