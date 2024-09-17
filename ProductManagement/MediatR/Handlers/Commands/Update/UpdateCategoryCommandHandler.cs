using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateCategoryCommandHandler : UpdateBaseCommandHandler<ICategoryService, UpdateCategoryCommand, UpdateCategoryDTO>
{
    public UpdateCategoryCommandHandler(ICategoryService service) : base(service)
    {
    }

}