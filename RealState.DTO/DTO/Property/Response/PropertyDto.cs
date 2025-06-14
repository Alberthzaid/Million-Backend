using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Domain.Dto.Property;

public class PropertyDto
{
    public string Id { get; set; } = string.Empty;
    public string IdOwner { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public ImagePropertyDto? Image { get; set; }
    
    public OwnerDto? Owner { get; set; }
}