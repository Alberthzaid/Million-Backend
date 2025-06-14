using Moq;
using AutoMapper;
using RealState.Application.Services;
using RealState.Domain.Dto.Property;
using RealState.Domain.Entity;
using RealState.Domain.Interfaces;
using RealState.Application.Interfaces;
using MongoDB.Driver;
using RealState.DTO.DTO.Property.Requests.Owner;

namespace RealState.Tests
{
    public class PropertyServiceTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IImagePropertyService> _imageServiceMock;
        private Mock<IOwnerService> _ownerServiceMock;
        private PropertyService _propertyService;

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _imageServiceMock = new Mock<IImagePropertyService>();
            _ownerServiceMock = new Mock<IOwnerService>();

            _propertyService = new PropertyService(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _imageServiceMock.Object,
                _ownerServiceMock.Object
            );
        }

        [Test]
        public async Task GetAllProperties_ReturnsMappedProperties()
        {
          
            var propertyId = "123";
            var ownerId = "456";

            var property = new Property
            {
                Id = propertyId,
                Name = "Casa Bonita",
                Address = "Calle XZQ",
                IdOwner = ownerId,
                Price = 100000
            };

            var properties = new List<Property> { property };

            _unitOfWorkMock.Setup(u => u.Property.GetAllAsync())
                .ReturnsAsync(properties);

            _imageServiceMock.Setup(i => i.GetImagePropertyAsync(propertyId))
                .ReturnsAsync(new ImagePropertyDto { Id = "img1", fileLink = "img.jpg" });

            _ownerServiceMock.Setup(o => o.GetOwnerById(ownerId))
                .ReturnsAsync(new OwnerDto { Id = ownerId, Name = "Pedro", Address = "Calle X", Photo = "pedro.jpg" });

           
            var result = await _propertyService.GetAllProperties();
            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Casa Bonita", result[0].Name);
            Assert.AreEqual("img.jpg", result[0].Image.fileLink);
            Assert.AreEqual("Pedro", result[0].Owner.Name);
        }
        
        [Test]
        public async Task GetProperty_ReturnsCorrectProperty()
        {
            var id = "prop1";
            var ownerId = "owner1";

            var property = new Property
            {
                Id = id,
                Name = "Casa Verde",
                Address = "Av. Siempreviva",
                IdOwner = ownerId,
                Price = 200000
            };

            _unitOfWorkMock.Setup(u => u.Property.GetByIdAsync(id))
                .ReturnsAsync(property);

            _imageServiceMock.Setup(i => i.GetImagePropertyAsync(id))
                .ReturnsAsync(new ImagePropertyDto { Id = "img123", fileLink = "green.jpg" });

            _ownerServiceMock.Setup(o => o.GetOwnerById(ownerId))
                .ReturnsAsync(new OwnerDto { Id = ownerId, Name = "Lisa", Address = "Calle B", Photo = "lisa.jpg" });

            var result = await _propertyService.GetProperty(id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Casa Verde", result.Name);
            Assert.AreEqual("green.jpg", result.Image.fileLink);
            Assert.AreEqual("Lisa", result.Owner.Name);
        }
        
        [Test]
        public async Task CreateAsync_ShouldMapAndCreateProperty()
        {
            var createDto = new CreatePropertyDto
            {
                Name = "Nueva Casa",
                Address = "Calle Nueva",
                Price = 300000,
                ImageUrl = "new.jpg",
                IdOwner = "owner123"
            };

            var property = new Property
            {
                Id = "newid",
                Name = createDto.Name,
                Address = createDto.Address,
                Price = createDto.Price,
                IdOwner = createDto.IdOwner
            };

            _mapperMock.Setup(m => m.Map<Property>(createDto)).Returns(property);
            _unitOfWorkMock.Setup(u => u.Property.AddAndReturnAsync(property)).ReturnsAsync(property);
            _mapperMock.Setup(m => m.Map<PropertyDto>(It.IsAny<Property>())).Returns(new PropertyDto { Id = "newid" });

            _imageServiceMock.Setup(i => i.CreateImageProperty(It.IsAny<CreateImageDto>()))
                .Returns(Task.FromResult(new ImagePropertyDto { Id = "img1", fileLink = "img.jpg" }));

            var result = await _propertyService.CreateAsync(createDto);

            Assert.IsNotNull(result);
            Assert.AreEqual("newid", result.Id);
            _imageServiceMock.Verify(i => i.CreateImageProperty(It.Is<CreateImageDto>(dto =>
                dto.idProperty == "newid" && dto.fileLink == "new.jpg")), Times.Once);
        }
        
        [Test]
        public async Task GetFilteredProperties_ReturnsCorrectFilteredList()
        {
            var filterDto = new PropertyFilterDto
            {
                Name = "casa",
                Address = "calle",
                MinPrice = 100000,
                MaxPrice = 500000
            };

            var filteredProperty = new Property
            {
                Id = "filter1",
                Name = "Casa Blanca",
                Address = "Calle Norte",
                IdOwner = "own1",
                Price = 300000
            };

            var mockCollection = new List<Property> { filteredProperty };

            _unitOfWorkMock.Setup(u => u.Property.FindAsync(It.IsAny<FilterDefinition<Property>>()))
                .ReturnsAsync(mockCollection);

            _imageServiceMock.Setup(i => i.GetImagePropertyAsync("filter1"))
                .ReturnsAsync(new ImagePropertyDto { Id = "imgfilter", fileLink = "blanca.jpg" });

            _ownerServiceMock.Setup(o => o.GetOwnerById("own1"))
                .ReturnsAsync(new OwnerDto { Id = "own1", Name = "Ana", Address = "Calle Z", Photo = "ana.jpg" });

            var result = await _propertyService.GetFilteredProperties(filterDto);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Casa Blanca", result[0].Name);
        }
    }
        
    
}
