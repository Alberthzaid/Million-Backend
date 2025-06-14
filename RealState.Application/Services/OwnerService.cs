using AutoMapper;
using RealState.Application.Interfaces;
using RealState.Domain.Entity.Owner;
using RealState.Domain.Interfaces;
using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Application.Services;

public class OwnerService : IOwnerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OwnerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<OwnerDto> CreateOwner(CreateOwnerDto createOwnerDto)
    {
        try
        {
            var owner = _mapper.Map<Owner>(createOwnerDto);
            await _unitOfWork.Owner.AddAsync(owner);
            return _mapper.Map<OwnerDto>(owner);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }  
    }

    public async Task<OwnerDto> GetOwnerById(string ownerId)
    {
        try
        {
            var owner = await _unitOfWork.Owner.GetByIdAsync(ownerId);
            return _mapper.Map<OwnerDto>(owner);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<OwnerDto>> GetOwners()
    {
        try
        {
            IEnumerable<Owner> owners= await _unitOfWork.Owner.GetAllAsync();
            return _mapper.Map<List<OwnerDto>>(owners);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}