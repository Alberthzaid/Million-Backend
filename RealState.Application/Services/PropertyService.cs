using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using RealState.Application.Interfaces;
using RealState.Domain.Dto.Property;
using RealState.Domain.Entity;
using RealState.Domain.Interfaces;
using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Application.Services;

public class PropertyService : IPropertyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IImagePropertyService _imagePropertyService;
    private readonly IOwnerService _ownerService;

    public PropertyService(IUnitOfWork unitOfWork, 
        IMapper mapper , 
        IImagePropertyService imagePropertyService
        ,IOwnerService ownerService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _imagePropertyService = imagePropertyService;
        _ownerService = ownerService;
    }

    public async Task<List<PropertyDto>> GetAllProperties()
    {
        try
        {
            IEnumerable<Property> properties = await _unitOfWork.Property.GetAllAsync();
            List<PropertyDto> listProperties = new List<PropertyDto>();
            
            foreach (var property in properties)
            {
                Task<ImagePropertyDto> imageProperty=  _imagePropertyService.GetImagePropertyAsync(property.Id);
                Task<OwnerDto> owner = _ownerService.GetOwnerById(property.IdOwner);
                PropertyDto propertyDto = new PropertyDto();
                propertyDto.Id = property.Id;
                propertyDto.Name = property.Name;
                propertyDto.Address = property.Address;
                propertyDto.IdOwner = property.IdOwner;
                propertyDto.Price = property.Price;
                propertyDto.Image = imageProperty.Result;
                propertyDto.Owner = owner.Result;
                listProperties.Add(propertyDto);
            }
            return listProperties;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<PropertyDto> GetProperty(string id)
    {
        try
        {
            PropertyDto vo = new PropertyDto();
            Property? property = await _unitOfWork.Property.GetByIdAsync(id);
            Task<ImagePropertyDto> imageProperty=  _imagePropertyService.GetImagePropertyAsync(property.Id);
            Task<OwnerDto> owner = _ownerService.GetOwnerById(property.IdOwner);
            
            vo.Id = property.Id;
            vo.Name = property.Name;
            vo.Address = property.Address;
            vo.Price = property.Price;
            vo.Image = imageProperty.Result;
            vo.Owner = owner.Result;
            
            return vo;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<PropertyDto> CreateAsync(CreatePropertyDto createDto)
    {
        try
        {
            var property = _mapper.Map<Property>(createDto);
            var propertyCreated = await _unitOfWork.Property.AddAndReturnAsync(property);

            CreateImageDto imagePropertyDto = new CreateImageDto();
            imagePropertyDto.idProperty = propertyCreated.Id;
            imagePropertyDto.fileLink = createDto.ImageUrl;
            
            await _imagePropertyService.CreateImageProperty(imagePropertyDto);
            return _mapper.Map<PropertyDto>(property);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    
    public async Task<List<PropertyDto>> GetFilteredProperties(PropertyFilterDto filterDto)
    {
        List<PropertyDto> listProperties = new List<PropertyDto>();
        try
        {
            var builder = Builders<Property>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrWhiteSpace(filterDto.Name))
            {
                filter &= builder.Regex(p => p.Name, new BsonRegularExpression(filterDto.Name, "i"));
            }

            if (!string.IsNullOrWhiteSpace(filterDto.Address))
            {
                filter &= builder.Regex(p => p.Address, new BsonRegularExpression(filterDto.Address, "i"));
            }

            if (filterDto.MinPrice.HasValue)
            {
                filter &= builder.Gte(p => p.Price, filterDto.MinPrice.Value);
            }

            if (filterDto.MaxPrice.HasValue)
            {
                filter &= builder.Lte(p => p.Price, filterDto.MaxPrice.Value);
            }

            var result = await _unitOfWork.Property.FindAsync(filter);

            foreach (var property in result)
            {
                Task<ImagePropertyDto> imageProperty=  _imagePropertyService.GetImagePropertyAsync(property.Id);
                Task<OwnerDto> owner = _ownerService.GetOwnerById(property.IdOwner);
                PropertyDto propertyDto = new PropertyDto();
                propertyDto.Id = property.Id;
                propertyDto.Name = property.Name;
                propertyDto.Address = property.Address;
                propertyDto.Price = property.Price;
                propertyDto.Image = imageProperty.Result;
                propertyDto.Owner = owner.Result;
                listProperties.Add(propertyDto);
            }
            
            return listProperties;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}