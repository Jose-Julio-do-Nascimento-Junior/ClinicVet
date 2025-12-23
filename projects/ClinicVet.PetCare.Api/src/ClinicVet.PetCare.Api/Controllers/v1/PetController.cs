using ClinicVet.Core.Api.Http.Controllers;
using ClinicVet.Core.Api.Http.Models;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Commands.v1.CreatePet;
using ClinicVet.PetCare.Domain.Commands.v1.UpdatePet;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryPetByFilters;
using System.Net;

namespace ClinicVet.PetCare.Api.Controllers.v1;

[Route("api/v1/pets")]
public sealed class PetController : BaseController
{
    public PetController(
        IMediator bus, 
        IDomainContextNotifications domainNotificationContext, 
        ApplicationSettings settings) : base(bus, domainNotificationContext, settings)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPetsByFiltersAsync(
        [FromQuery] GetPetByFiltersQuery query,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(query, cancellationToken);
    }


    [HttpGet("temporary")]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetTemporaryPetsByFiltersAsync(
        [FromQuery] GetTemporaryPetByFiltersQuery query,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(query, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePetAsync(
        CreatePetCommand command,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(command, cancellationToken);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePetAsync(
        UpdatePetCommand command,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(command, cancellationToken);
    }
}