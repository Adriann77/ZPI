using WebStore.Services.Interfaces;
using WebStore.ViewModels.VM;
using Xunit;

namespace WebStore.Tests
{
    public class ProductServiceTests
    {
        private readonly IProductService _productService;

        public ProductServiceTests(IProductService productService)
        {
            _productService = productService;
        }

        [Fact]
        public void GetProducts_ShouldReturnAllProducts()
        {
            // Act
            var products = _productService.GetProducts();

            // Assert
            Assert.NotNull(products);
            Assert.NotEmpty(products);
            Assert.Equal(2, products.Count());
        }

        [Fact]
        public void GetProduct_WithValidId_ShouldReturnProduct()
        {
            // Act
            var product = _productService.GetProduct(p => p.Id == 1);

            // Assert
            Assert.NotNull(product);
            Assert.Equal("Monitor Dell 32", product.Name);
            Assert.Equal(1000, product.Price);
        }

        [Fact]
        public void GetProduct_WithInvalidId_ShouldReturnNull()
        {
            // Act
            var product = _productService.GetProduct(p => p.Id == 999);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public void AddOrUpdateProduct_WithNewProduct_ShouldAddProduct()
        {
            // Arrange
            var newProduct = new AddOrUpdateProductVm
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Weight = 1.0f,
                CategoryId = 1,
                SupplierId = 1,
                ImageBytes = new byte[] { 0x01, 0x02, 0x03 }
            };

            // Act
            var result = _productService.AddOrUpdateProduct(newProduct);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
            Assert.Equal(100, result.Price);
        }
    }
}
