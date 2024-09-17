using MediatR;

namespace ProductManagement.MediatR.Commands.Delete;

public abstract record BaseDeleteCommand(int Id) : IRequest<Unit>;
