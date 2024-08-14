using Moq;
using Xunit;
using ShoppingWeb.Services.Implementations;
using ShoppingWeb.Shared.Interfaces;
using ShoppingWeb.Shared.Models;
using ShoppingWeb.Shared.DTOs;
using AutoMapper;

namespace ShoppingWeb.Tests
{
    public class ProductServiceTests
    {
        private readonly ProductService _productService;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock; // Mock IMapper

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>(); // Initialize the mock

            // Pass the mock IMapper to the service
            _productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);
        }
        [Fact]
        public async Task GetAllProductsAsync_ReturnsListOfProducts()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Product 1" },
                new Product { Id = 2, Name = "Product 2" }
            };

            _productRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);

            _mapperMock.Setup(m => m.Map<IEnumerable<ProductDto>>(It.IsAny<IEnumerable<Product>>()))
                       .Returns(products.Select(p => new ProductDto { Id = p.Id, Name = p.Name }).ToList());

            // Act
            var result = await _productService.GetAllProductsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetProductById_ProductNotFound_ReturnsNull()
        {
            // Arrange
            _productRepositoryMock.Setup(repo => repo.GetById(It.IsAny<int>())).Returns((Product)null);

            // Act
            var result = _productService.GetProductById(1);

            // Assert
            Assert.Null(result);
        }
    }
}
