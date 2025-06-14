using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Application.Interfaces;

public interface IOwnerService
{
    Task<OwnerDto> GetOwnerById(string ownerId);
    Task<List<OwnerDto>> GetOwners();
    Task<OwnerDto> CreateOwner(CreateOwnerDto createOwnerDto);
    
}