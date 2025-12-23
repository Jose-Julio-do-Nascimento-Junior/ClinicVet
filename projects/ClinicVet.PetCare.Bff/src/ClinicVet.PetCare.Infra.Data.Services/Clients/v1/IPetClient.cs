using ClinicVet.Core.Infra.Services.Http.Attributes;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePet;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePet;
using Refit;

namespace ClinicVet.PetCare.Infra.Data.Services.Clients.v1;

[HttpService("PetCare")]
public interface IPetClient
{
    [Get("/api/v1/pets")]
    Task<HttpResponseMessage> GetPetsByFiltersAsync(
         [Query] PetByFiltersDto filters,
         CancellationToken CancellationToken);

    [Get("/api/v1/pets/temporary")]
    Task<HttpResponseMessage> GetTemporaryPetsByFiltersAsync(
         [Query] PetByFiltersDto filters,
         CancellationToken CancellationToken);

    [Post("/api/v1/pets")]
    Task<HttpResponseMessage> CreatePetAsync(
         [Body] CreatePetDto createPetDto,
         CancellationToken CancellationToken);

    [Patch("/api/v1/pets")]
    Task<HttpResponseMessage> UpdatePetAsync(
         [Body] UpdatePetDto updatePetDto,
         CancellationToken CancellationToken);
}