using ClinicVet.Core.Domain.Models;
using ClinicVet.PetCare.Domain.Contracts.v1.Services;
using ClinicVet.PetCare.Domain.Dtos.v1;
using ClinicVet.PetCare.Domain.Dtos.v1.CreatePetOwner;
using ClinicVet.PetCare.Domain.Dtos.v1.UpdatePetOwner;
using System.Net;

namespace ClinicVet.PetCare.Bff.Controllers.v1;

[ApiController]
[Route("api/v1/pet-owners")]
public class PetOwnerController : ControllerBase
{
    private readonly IPetOwnerService _petOwnerService;

    public PetOwnerController(IPetOwnerService petOwnerService)
    {
        _petOwnerService = petOwnerService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPetOwnerByFiltersAsync(
       [FromQuery] PetOwnerByFiltersDto filters,
       CancellationToken cancellationToken)
    {
        var response = await _petOwnerService.GetPetOwnersAsync(filters, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePetOwnerAsync(
       [FromBody] CreatePetOwnerDto createPetOwnerDto,
       CancellationToken cancellationToken)
    {
        var response = await _petOwnerService.CreatePetOwnerAsync(createPetOwnerDto, cancellationToken);

        return Ok(response);
    }

    [HttpPatch]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(Response), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdatePetOwnerAsync(
       [FromBody] UpdatePetOwnerDto updatePetOwnerDto,
       CancellationToken cancellationToken)
    {
        var response = await _petOwnerService.UpdatePetOwnerAsync(updatePetOwnerDto, cancellationToken);

        return Ok(response);
    }
}