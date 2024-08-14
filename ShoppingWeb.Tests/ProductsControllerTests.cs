using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using ShoppingWeb.API.Controllers;
using ShoppingWeb.Shared.DTOs;
using ShoppingWeb.Shared.Interfaces;
using Microsoft.Extensions.Logging;  // Add this namespace

namespace ShoppingWeb.Tests
{
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ILogger<ProductsController>> _loggerMock;  // Mock the logger

        public ProductsControllerTests()
        {
            _productServiceMock = new Mock<IProductService>();
            _loggerMock = new Mock<ILogger<ProductsController>>();  // Initialize the mock

            _controller = new ProductsController(_productServiceMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task GetAllProducts_NoProducts_ReturnsNotFound()
        {
            // Arrange
            _productServiceMock.Setup(service => service.GetAllProductsAsync()).ReturnsAsync((IEnumerable<ProductDto>)null);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<NotFoundObjectResult>(result);

            // Ensure actionResult.Value is not null
            Assert.NotNull(actionResult.Value);

            // Check if the Value is an anonymous type with a Message property
            var messageProperty = actionResult.Value.GetType().GetProperty("Message");

            // Assert that the Message property exists and has the expected value
            Assert.NotNull(messageProperty);
            Assert.Equal("No products available.", messageProperty.GetValue(actionResult.Value)?.ToString());
        }

        [Fact]
        public async Task GetAllProducts_ProductsExist_ReturnsOk()
        {
            // Arrange
            var products = new List<ProductDto>
            {
                new ProductDto { Id = 1, Name = "Product 1" },
                new ProductDto { Id = 2, Name = "Product 2" }
            };
            _productServiceMock.Setup(service => service.GetAllProductsAsync()).ReturnsAsync(products);

            // Act
            var result = await _controller.GetAllProducts();

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<ProductDto>>(actionResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

    }
}
