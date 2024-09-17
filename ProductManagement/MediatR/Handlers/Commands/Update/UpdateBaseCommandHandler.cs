using MediatR;
using ProductManagement.MediatR.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Update;

public class UpdateBaseCommandHandler<TService, TCommand, TUpdateDTO> : IRequestHandler<TCommand, TUpdateDTO>
    where TCommand : BaseUpdateCommand<TUpdateDTO>
{
    private readonly TService _service;

    public UpdateBaseCommandHandler(TService service)
    {
        _service = service;
    }

    public async Task<TUpdateDTO> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await ((dynamic)_service).UpdateAsync(request.dto);
    }
}