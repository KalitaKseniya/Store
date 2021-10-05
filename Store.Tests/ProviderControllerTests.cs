using Microsoft.AspNetCore.Mvc;
using Moq;
using Store.Core.DTO;
using Store.Core.Entities;
using Store.Core.Interfaces;
using Store.Core.RequestFeatures;
using Store.Tests.FakeRepositories;
using Store.V1.Controllers;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Store.Tests
{
    public class ProviderControllerTests
    {
        IProviderRepository _providerRepository;
        Mock<ILoggerManager> _logger;
        ProviderController _providerController;
        public ProviderControllerTests()
        {
            _providerRepository = new ProviderRepositoryFake();
            _logger = new Mock<ILoggerManager>();

            _providerController = new ProviderController(_logger.Object, _providerRepository);
        }

        [Fact]
        public void GetProviders_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            var providerParams = new ProviderParams();
            //Act
            var okResult = _providerController.GetProviders(providerParams) as OkObjectResult;

            //Assert
            Assert.NotNull(okResult);
            Assert.Equal(((int)HttpStatusCode.OK), okResult.StatusCode);
        }

        [Fact]
        public void GetProviders_WhenCalled_ReturnsAllItems()
        {
            //Arrange
            var providerParams = new ProviderParams();

            //Act
            var okResult = _providerController.GetProviders(providerParams) as OkObjectResult;

            //Assert
            var items = Assert.IsType<PagedList<Provider>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }
        
        [Fact]
        public void GetProvider_UnkownIdPassed_ReturnsNotFoundResult()
        {
            //Arrange
            var testId = 4787357;

            //Act
            var notFoundResult = _providerController.GetProvider(testId) as NotFoundObjectResult;

            //Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal((int)HttpStatusCode.NotFound, notFoundResult.StatusCode);
        } 

        [Fact]
        public void GetProvider_ExistingIdPassed_ReturnsOkResult()
        {
            var testId = 2;
            var okResult = _providerController.GetProvider(testId) as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal((int)HttpStatusCode.OK, okResult.StatusCode);
        }

        [Fact]
        public void GetProvider_ExistingIdPassed_ReturnsRightItem()
        {
            var testId = 3;
            var okResult = _providerController.GetProvider(testId) as OkObjectResult;
            Assert.IsType<Provider>(okResult.Value);
            Assert.Equal(testId, (okResult.Value as Provider).Id);
        }

        [Fact]
        public void CreateProvider_InvalidObjectPassed_ReturnsBadRequest()
        {
            var providerForCreation = new ProviderForManipulationDto()
            {
                ImagePath = "https://dartflutter.ru/wp-content/uploads/2019/12/flutter-provider-min.jpg",
                Latitude = 10,
                Longitude = 20,
                Info = "Info about new provider"
            };
            _providerController.ModelState.AddModelError("Name", "Required");

            var badRequestObjectResult = _providerController.CreateProvider(providerForCreation) as BadRequestResult;

            Assert.NotNull(badRequestObjectResult);
            Assert.IsType<BadRequestObjectResult>(badRequestObjectResult);
        }
        
        [Fact]
        public void CreateProvider_ValidObjectPassed_ReturnsCreated()
        {
            var providerForCreation = new ProviderForManipulationDto()
            {
                Name = "Test provider",
                ImagePath = "https://dartflutter.ru/wp-content/uploads/2019/12/flutter-provider-min.jpg",
                Latitude = 10,
                Longitude = 20,
                Info = "Info about new provider"
            };
            
            var createdResult = _providerController.CreateProvider(providerForCreation) as ObjectResult;

            Assert.NotNull(createdResult);
            Assert.Equal(createdResult.StatusCode, (int)HttpStatusCode.Created);
        }
        
        [Fact]
        public void CreateProvider_ValidObjectPassed_ReturnedResponseHasCreatedItem()
        {
            var providerForCreation = new ProviderForManipulationDto()
            {
                ImagePath = "https://dartflutter.ru/wp-content/uploads/2019/12/flutter-provider-min.jpg",
                Latitude = 10,
                Longitude = 20,
                Info = "Info about new provider"
            };
            _providerController.ModelState.AddModelError("Name", "Required");

            var createdResult = _providerController.CreateProvider(providerForCreation) as ObjectResult;

            Assert.NotNull(createdResult);
            Assert.Equal(createdResult.StatusCode, (int)HttpStatusCode.Created);
        }


    }
}
