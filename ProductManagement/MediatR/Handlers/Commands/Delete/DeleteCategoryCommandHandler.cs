using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Delete;
using ProductManagement.MediatR.Handlers.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Delete;


public class DeleteCategoryCommandHandler : DeleteBaseCommandHandler<ICategoryService, DeleteCategoryCommand>
{
    public DeleteCategoryCommandHandler(ICategoryService service) : base(service)
    {
    }

}