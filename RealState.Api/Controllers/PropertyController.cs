using Microsoft.AspNetCore.Mvc;
using RealState.Application.Interfaces;
using RealState.Domain.Dto.Property;

namespace RealState.Controllers;

[ApiController]
public class PropertyController : BaseApiController
{
    private readonly IPropertyService _propertyService;

    public PropertyController(IPropertyService propertyService)
    {
        _propertyService = propertyService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task <ActionResult<IEnumerable<PropertyDto>>>Get()
    {
        return await _propertyService.GetAllProperties();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PropertyDto>> GetOne(string id)
    {
        return await _propertyService.GetProperty(id);
    }

    [HttpPost("Filter")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> Filter([FromBody] PropertyFilterDto filter)
    {
        return await _propertyService.GetFilteredProperties(filter);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Add([FromBody] CreatePropertyDto dto)
    { 
        await _propertyService.CreateAsync(dto);
        return Ok(new { message = "Property created" });
    }
}