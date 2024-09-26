using MediatR;

namespace ProductManagement.MediatR.Commands.Delete;

public record DeleteProductImageCommand(int Id) : BaseDeleteCommand(Id);