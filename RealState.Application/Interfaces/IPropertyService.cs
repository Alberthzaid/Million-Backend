using RealState.Domain.Dto.Property;

namespace RealState.Application.Interfaces;

public interface IPropertyService
{
    Task<List<PropertyDto>> GetAllProperties();
    Task<List<PropertyDto>> GetFilteredProperties(PropertyFilterDto filterDto);
    Task<PropertyDto> CreateAsync(CreatePropertyDto createDto);
    Task<PropertyDto> GetProperty(string id);

}