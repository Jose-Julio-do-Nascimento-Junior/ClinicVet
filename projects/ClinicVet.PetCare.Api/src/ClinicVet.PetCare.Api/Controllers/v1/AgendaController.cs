using ClinicVet.Core.Api.Http.Controllers;
using ClinicVet.Core.Api.Http.Models;
using ClinicVet.Core.Domain.Contracts;
using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Commands.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Commands.v1.UdpateAgenda;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Infra.Data.Queries.v1.GetTemporaryAgendaByFilters;
using System.Net;

namespace ClinicVet.PetCare.Api.Controllers.v1;

[Route("api/v1/agendas")]
public sealed class AgendaController : BaseController
{
    public AgendaController(
        IMediator bus,
        IDomainContextNotifications domainNotificationContext,
        ApplicationSettings settings) : base(bus, domainNotificationContext, settings)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAgendaByFiltersAsync(
        [FromQuery] GetAgendaByFiltersQuery query,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(query, cancellationToken);
    }

    [HttpGet("temporary")]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetTemporaryAgendaByFiltersAsync(
        [FromQuery] GetTemporaryAgendaByFiltersQuery query,
        CancellationToken cancellationToken)
    {
        return await ExecuteAsync(query, cancellationToken);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAgendaAsync(
      CreateAgendaCommand command,
      CancellationToken cancellationToken)
    {
        return await ExecuteAsync(command, cancellationToken);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateAgendaAsync(
       UpdateAgendaCommand command,
       CancellationToken cancellationToken)
    {
        return await ExecuteAsync(command, cancellationToken);
    }
}