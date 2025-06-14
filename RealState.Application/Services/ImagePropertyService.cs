using AutoMapper;
using RealState.Application.Interfaces;
using RealState.Domain.Dto.Property;
using RealState.Domain.Entity;
using RealState.Domain.Interfaces;

namespace RealState.Application.Services;

public class ImagePropertyService : IImagePropertyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ImagePropertyService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ImagePropertyDto> GetImagePropertyAsync(string idProperty)
    {
        try
        {
            var imageList = _unitOfWork.ImageProperty.Find(i => i.idProperty == idProperty);
            var image = imageList.FirstOrDefault();

            if (image == null)
                return null;

            return _mapper.Map<ImagePropertyDto>(image);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<ImagePropertyDto> CreateImageProperty(CreateImageDto dto)
    {
        try
        {
            var image = _mapper.Map<ImageProperty>(dto);
            await _unitOfWork.ImageProperty.AddAsync(image);
            return _mapper.Map<ImagePropertyDto>(image);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}