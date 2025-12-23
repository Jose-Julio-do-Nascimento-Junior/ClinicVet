using ClinicVet.Core.Infra.Services.Http.Attributes;
using ClinicVet.PetCare.Domain.Dtos.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdateAgenda;
using Refit;

namespace ClinicVet.PetCare.Infra.Data.Services.Clients.v1;

[HttpService("PetCare")]
public interface IAgendaClient
{
    [Get("/api/v1/agendas")]
    Task<HttpResponseMessage> GetAgendaByFiltersAsync(
         [Query] GetAgendaByFiltersDto filters,
         CancellationToken CancellationToken);

    [Get("/api/v1/agendas/temporary")]
    Task<HttpResponseMessage> GetTemporaryAgendaByFiltersAsync(
         [Query] GetAgendaByFiltersDto filter,
         CancellationToken CancellationToken);

    [Post("/api/v1/agendas")]
    Task<HttpResponseMessage> CreateAgendaAsync(
         [Body]CreateAgendaDto createAgendaDto,
         CancellationToken CancellationToken);

    [Patch("/api/v1/agendas")]
    Task<HttpResponseMessage> UpdateAgendaAsync(
         [Body]UpdateAgendaDto updateAgendaDto,
         CancellationToken CancellationToken);
}