using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1.CreateAgenda;
using ClinicVet.PetCare.Domain.Dtos.v1.GetAgendaByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdateAgenda;
using System.Net;

namespace ClinicVet.PetCare.Bff.Controllers.v1;

[ApiController]
[Route("api/v1/agendas")]
public class AgendaController : ControllerBase
{
    private readonly IAgendaService _agendaService;
    private readonly ITemporaryAgendaService _temporaryAgendaService;

    public AgendaController(IAgendaService agendaService, ITemporaryAgendaService temporaryAgendaService)
    {
        _agendaService = agendaService;
        _temporaryAgendaService = temporaryAgendaService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAgendaByFiltersAsync(
        [FromQuery] GetAgendaByFiltersDto filters,
        CancellationToken cancellationToken)
    {
        var response = await _agendaService.GetAgendasAsync(filters, cancellationToken);

        return Ok(response);
    }

    [HttpGet("temporary")]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetTemporaryAgendaByFiltersAsync(
       [FromQuery] GetAgendaByFiltersDto filters,
       CancellationToken cancellationToken)
    {
        var response = await _temporaryAgendaService.GetTemporaryAgendasAsync(filters, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAgendaAsync(
       [FromBody] CreateAgendaDto createAgendaDto,
       CancellationToken cancellationToken)
    {
        var response = await _agendaService.CreateAgendaAsync(createAgendaDto, cancellationToken);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateAgendaAsync(
       [FromBody] UpdateAgendaDto updateAgendaDto,
       CancellationToken cancellationToken)
    {
        var response = await _agendaService.UpdateAgendaAsync(updateAgendaDto, cancellationToken);

        return Ok(response);
    }
}