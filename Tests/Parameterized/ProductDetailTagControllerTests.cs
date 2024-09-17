using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Controllers;
using ProductManagement.DTOs.Read;
using ProductManagement.MediatR.Queries;

namespace Tests.Parameterized;

public class ProductDetailTagControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ProductDetailTagController _controller;

    public ProductDetailTagControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ProductDetailTagController(_mediatorMock.Object);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetProductDetailTag_ReturnsOkResult_WhenTagExists(int id)
    {
        // Arrange
        var tag = new ProductDetailTagDTO { Id = id };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductDetailTagByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(tag);

        // Act
        var result = await _controller.GetProductDetailTag(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(tag, okResult.Value);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public async Task GetProductDetailTag_ReturnsNotFound_WhenTagDoesNotExist(int id)
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetProductDetailTagByIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ProductDetailTagDTO)null);

        // Act
        var result = await _controller.GetProductDetailTag(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
