using MediatR;
using ProductManagement.MediatR.Commands.Create;

namespace ProductManagement.MediatR.Handlers.Commands.Create;

public class CreateBaseCommandHandler<TService, TCommand, TCreateDTO> : IRequestHandler<TCommand, int>
    where TCommand : BaseCreateCommand<TCreateDTO>
{
    private readonly TService _service;

    public CreateBaseCommandHandler(TService service)
    {
        _service = service;
    }

    public async Task<int> Handle(TCommand request, CancellationToken cancellationToken)
    {
        return await ((dynamic)_service).CreateAsync(request.dto);
    }
}