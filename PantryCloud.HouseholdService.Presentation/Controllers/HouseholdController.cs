using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PantryCloud.HouseholdService.Application.Commands;
using PantryCloud.HouseholdService.Application.Dtos;
using PantryCloud.HouseholdService.Application.Queries;

namespace PantryCloud.HouseholdService.Presentation.Controllers;

[ApiController]
[Authorize]
[Route("api/households")]
public class HouseholdController(IMediator mediator, IMapper mapper) : ApiControllerBase(mediator, mapper)
{
    [HttpPost("")]
    [ProducesResponseType(typeof(CreateHouseholdResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateHousehold([FromBody] CreateHouseholdRequestDto request, CancellationToken cancellationToken)
    {
        var command = Mapper.Map<CreateHouseholdCommand>(request);
        var result = await Mediator.Send(command, cancellationToken);

        return FromResult(result, StatusCodes.Status201Created);
    }
    
    [HttpGet("me")]
    [ProducesResponseType(typeof(GetCurrentHouseholdResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCurrentHousehold(CancellationToken cancellationToken)
    {
        var query = new GetCurrentHouseholdQuery();
        var result = await Mediator.Send(query, cancellationToken);

        return FromResult(result, StatusCodes.Status200OK);
    }
}