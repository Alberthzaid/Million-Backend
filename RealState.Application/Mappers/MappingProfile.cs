using AutoMapper;
using RealState.Domain.Dto.Property;
using RealState.Domain.Entity;
using RealState.Domain.Entity.Owner;
using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Application.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreatePropertyDto, Property>().ReverseMap();
        CreateMap<Property, PropertyDto>();

        CreateMap<ImageProperty, ImagePropertyDto>().ReverseMap(); 
        CreateMap<CreateImageDto, ImageProperty>().ReverseMap();

        CreateMap<CreateOwnerDto, Owner>().ReverseMap();
        CreateMap<Owner, OwnerDto>();
    }
}
