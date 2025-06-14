using RealState.Domain.Dto.Property;

namespace RealState.Application.Interfaces;

public interface IImagePropertyService
{
    Task<ImagePropertyDto> GetImagePropertyAsync(string idProperty);
    Task<ImagePropertyDto> CreateImageProperty(CreateImageDto dto);
}