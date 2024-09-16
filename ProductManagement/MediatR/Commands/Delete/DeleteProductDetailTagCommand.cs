using MediatR;

namespace ProductManagement.MediatR.Commands.Delete;
public record DeleteProductDetailTagCommand(int Id) : IRequest<Unit>;
