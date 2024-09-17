using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Queries;

namespace ProductManagement.MediatR.Handlers.Queries;

public class GetBaseByIdQueryHandler<TQuery, TDTO, TService> : IRequestHandler<TQuery, TDTO?>
    where TQuery : BaseQuery<TDTO>
    where TDTO : class
    where TService : class
{
    private readonly TService _service;

    public GetBaseByIdQueryHandler(TService service)
    {
        _service = service;
    }

    public async Task<TDTO?> Handle(TQuery request, CancellationToken cancellationToken)
    {
        return await ((dynamic)_service).GetByIdAsync(request.Id);
    }
}