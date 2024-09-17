using MediatR;

namespace ProductManagement.MediatR.Queries;

public abstract record BaseQuery<T>(int Id) : IRequest<T>;