using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;

public class GetBaseByIdQueryHandler<Q, D, I> : IRequestHandler<Q, D?>
    where Q : IRequest<D?>
    where D : class
    where I : class
{
    private readonly I _service;

    public GetBaseByIdQueryHandler(I service)
    {
        _service = service;
    }

    public async Task<D?> Handle(Q request, CancellationToken cancellationToken)
    {
        return await _service.GetByIdAsync(request.Id);
    }
}