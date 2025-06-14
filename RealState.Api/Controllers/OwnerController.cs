using Microsoft.AspNetCore.Mvc;
using RealState.Application.Interfaces;
using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Controllers;

public class OwnerController : BaseApiController
{
    private readonly IOwnerService _ownerService;

    public OwnerController(IOwnerService ownerService)
    {
        _ownerService = ownerService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
    {
        return await _ownerService.GetOwners();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OwnerDto>> GetById(string id)
    {
        return await _ownerService.GetOwnerById(id);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(CreateOwnerDto createOwnerDto)
    {
        await _ownerService.CreateOwner(createOwnerDto);
        return Ok(new {message = "Owner created"});
    }
}