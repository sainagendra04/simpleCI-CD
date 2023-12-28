using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Model;
using WebApi.Repository;

namespace WebApiTest.Controller
{
    public class ProductControllerTest
    {
        [Fact]
        public void GetProductById_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            int validProductId = 1;

            mockRepository.Setup(repo => repo.GetProductById(validProductId))
                          .Returns(new Product { ProductId = validProductId, ProductName = "Product1" });

            // Act
            var result = controller.GetProductById(validProductId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(validProductId, product.ProductId);
        }

        [Fact]
        public void GetProductByName_ValidName_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            var expectedProducts=new List<Product> { new Product { ProductId = 2, ProductName = "existingProduct" } };

            mockRepository.Setup(repo => repo.GetProductByName("existingProduct"))
                          .Returns(expectedProducts);

            // Act
            var result = controller.GetProductByName("existingProduct");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var actualProducts = Assert.IsType<List<Product>>(okResult.Value);
            Assert.Equal(expectedProducts, actualProducts);
        }

        [Fact]
        public void AddProduct_ValidProduct_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            var newProduct = new Product { ProductId = 1, ProductName = "Product2" };

            // Act
            var result = controller.AddProduct(newProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(newProduct, product);
        }

        [Fact]
        public void UpdateProduct_ValidIdAndProduct_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            int productId = 1;
            var existingProduct= new Product { ProductId = productId, ProductName="Existing Product" };
            var updatedProduct = new Product { ProductId = productId, ProductName = "Updated Product" };

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(productId))
                          .Returns(existingProduct);

            // Act
            var result = controller.UpdateProduct(productId, updatedProduct);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(updatedProduct.ProductId, product.ProductId);
            Assert.Equal(updatedProduct.ProductName, product.ProductName);
        }

        [Fact]
        public void DeleteProduct_ValidId_ReturnsOkResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            int validProductId = 1;
            var existingProduct = new Product { ProductId = validProductId, ProductName = "Existing Product" };
            mockRepository.Setup(repo => repo.GetProductById(validProductId))
                          .Returns(existingProduct);

            // Act
            var result = controller.DeleteProduct(validProductId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var message = Assert.IsType<string>(okResult.Value);
            Assert.Contains(validProductId.ToString(), message);
        }


        [Fact]
        public void GetProductById_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            int invalidProductId = 5;

            mockRepository.Setup(repo => repo.GetProductById(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.GetProductById(invalidProductId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void GetProductByName_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);

            mockRepository.Setup(repo => repo.GetProductByName("nonexistingProduct"))
                          .Returns(new List<Product>());

            // Act
            var result = controller.GetProductByName("nonexistingProduct");

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void AddProduct_InvalidProduct_ReturnsBadRequestResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            
            // Act
            var result = controller.AddProduct(null);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UpdateProduct_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            int invalidProductId = 999;
            var updatedProduct = new Product { ProductId = invalidProductId, ProductName = "Updated Product" };

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.UpdateProduct(invalidProductId, updatedProduct);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void DeleteProduct_ProductNotFound_ReturnsNotFoundResult()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var controller = new ProductController(mockRepository.Object);
            int invalidProductId = 999;

            // Set up mock repository behavior
            mockRepository.Setup(repo => repo.GetProductById(invalidProductId))
                          .Returns((Product)null);

            // Act
            var result = controller.DeleteProduct(invalidProductId);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }
    }
}
