using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManagement.Controllers;
using ProductManagement.DTOs.Read;
using ProductManagement.MediatR.Queries;


namespace Tests.Parameterized;
public class CategoryControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CategoryController _controller;

    public CategoryControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CategoryController(_mediatorMock.Object);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task GetCategory_ReturnsOkResult_WhenCategoryExists(int id)
    {
        // Arrange
        var category = new CategoryDTO { Id = id };
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync(category);

        // Act
        var result = await _controller.GetCategory(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(category, okResult.Value);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public async Task GetCategory_ReturnsNotFound_WhenCategoryDoesNotExist(int id)
    {
        // Arrange
        _mediatorMock.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), It.IsAny<CancellationToken>()))
                     .ReturnsAsync((CategoryDTO)null);

        // Act
        var result = await _controller.GetCategory(id);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
