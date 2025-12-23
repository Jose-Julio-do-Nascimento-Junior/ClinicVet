using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePet;
using ClinicVet.PetCare.Domain.Dtos.v1.GetPetByFilters;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePet;
using System.Net;

namespace ClinicVet.PetCare.Bff.Controllers.v1;

[ApiController]
[Route("api/v1/pets")]
public class PetController : ControllerBase
{
    private readonly IPetService _petService;
    private readonly ITemporaryPetService _temporaryPetService;

    public PetController(IPetService petService, ITemporaryPetService temporaryPetService)
    {
        _petService = petService;
        _temporaryPetService = temporaryPetService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPetsByFiltersAsync(
       [FromQuery] PetByFiltersDto filters,
       CancellationToken cancellationToken)
    {
        var response = await _petService.GetPetsAsync(filters, cancellationToken);

        return Ok(response);
    }

    [HttpGet("temporary")]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetTemporaryPetsByFiltersAsync(
       [FromQuery] PetByFiltersDto filters,
       CancellationToken cancellationToken)
    {
        var response = await _temporaryPetService.GetTemporaryPetsAsync(filters, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePetAsync(
       [FromBody] CreatePetDto createPetDto,
       CancellationToken cancellationToken)
    {
        var response = await _petService.CreatePetAsync(createPetDto, cancellationToken);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePetAsync(
       [FromBody] UpdatePetDto updatePetDto,
       CancellationToken cancellationToken)
    {
        var response = await _petService.UpdatePetAsync(updatePetDto, cancellationToken);

        return Ok(response);
    }
}