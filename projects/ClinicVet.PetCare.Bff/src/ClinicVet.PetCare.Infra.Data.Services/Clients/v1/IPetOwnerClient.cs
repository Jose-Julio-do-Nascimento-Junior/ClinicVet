using ClinicVet.Core.Infra.Services.Http.Attributes;
using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePetOwner;
using Refit;

namespace ClinicVet.PetCare.Infra.Data.Services.Clients.v1;

[HttpService("PetCare")]
public interface IPetOwnerClient
{
    [Get("/api/v1/pet-owners")]
    Task<HttpResponseMessage> GetPetOwnerByFiltersAsync(
         [Query] PetOwnerByFiltersDto filters,
         CancellationToken CancellationToken);

    [Post("/api/v1/pet-owners")]
    Task<HttpResponseMessage> CreatePetOwnerAsync(
         [Body] CreatePetOwnerDto createPetOwnerDto,
         CancellationToken CancellationToken);

    [Patch("/api/v1/pet-owners")]
    Task<HttpResponseMessage> UpdatePetOwnerAsync(
         [Body]UpdatePetOwnerDto updatePetOwnerDto,
         CancellationToken CancellationToken);
}