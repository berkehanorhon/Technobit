using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Controllers;
using ProductManagement.DTOs.Read;
using ProductManagement.MediatR.Queries;

namespace Tests.Parameterized;

public class ProductControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductController _controller;

    public ProductControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductController(_mediatorMock.Object);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetProduct_ReturnsOkResult_WhenProductExists(int id)
    {
        // Arrange
        var product = new ProductDTO { Id = id };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        var result = await _controller.GetProduct(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, okResult.Value);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public async Task GetProduct_ReturnsNotFound_WhenProductDoesNotExist(int id)
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductDTO)null);

        // Act
        var result = await _controller.GetProduct(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
