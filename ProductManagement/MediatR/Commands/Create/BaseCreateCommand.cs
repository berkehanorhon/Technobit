using MediatR;

namespace ProductManagement.MediatR.Commands.Create;

public class BaseCreateCommand
{
    
}

public abstract record BaseCreateCommand<TCreateDTO>(TCreateDTO dto) : IRequest<int>;
