using MediatR;
using ProductManagement.MediatR.Commands.Delete;

namespace ProductManagement.MediatR.Handlers.Commands.Delete;

public class DeleteBaseCommandHandler<TService, TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : BaseDeleteCommand
{
    private readonly TService _service;

    public DeleteBaseCommandHandler(TService service)
    {
        _service = service;
    }

    public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await ((dynamic)_service).DeleteAsync(request.Id);
    }
}