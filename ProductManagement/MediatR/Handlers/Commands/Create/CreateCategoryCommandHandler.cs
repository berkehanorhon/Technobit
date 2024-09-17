using ProductManagement.DTOs.Create;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateCategoryCommandHandler : CreateBaseCommandHandler<ICategoryService, CreateCategoryCommand, CreateCategoryDTO>
{
    public CreateCategoryCommandHandler(ICategoryService service) : base(service)
    {
    }

}