using ClinicVet.Core.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClinicVet.PetCare.Domain.Dtos;

public class BaseFiltersQueryDto : Query
{
    [FromQuery(Name = "offset")]
    public int Skip { get; set; } = 0;

    [FromQuery(Name = "limit")]
    public int Take { get; set; } = 10;
}