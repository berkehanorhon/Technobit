using MediatR;

namespace ProductManagement.MediatR.Commands.Delete;
public record DeleteCategoryCommand(int Id) : IRequest<Unit>;