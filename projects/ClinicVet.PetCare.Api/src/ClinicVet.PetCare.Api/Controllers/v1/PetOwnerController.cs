using ClinicVet.Core.Api.Http.Controllers;
using ClinicVet.Core.Api.Http.Models;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Commands.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Commands.v1.UpdatePetOwner;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetPetOwnerByFilters;
using System.Net;

namespace ClinicVet.PetCare.Api.Controllers.v1;

[Route("api/v1/pet-owners")]
public sealed class PetOwnerController : BaseController
{
    public PetOwnerController(
        IMediator bus,
        IDomainContextNotifications domainNotificationContext,
        ApplicationSettings settings) : base(bus, domainNotificationContext, settings)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPetOwnerByFiltersAsync(
        [FromQuery] GetPetOwnerByFiltersQuery query,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(query, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePetOwnerAsync(
        CreatePetOwnerCommand command,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(command, cancellationToken);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePetOwnerAsync(
       UpdatePetOwnerCommand command,
       CancellationToken cancellationToken)
    {
        return await ExecuteAsync(command, cancellationToken);
    }
}