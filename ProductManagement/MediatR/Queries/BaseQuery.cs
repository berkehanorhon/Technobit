using MediatR;

namespace ProductManagement.MediatR.Queries;

public abstract class BaseQuery<T> : IRequest<T>
{
    public int Id { get; set; }
}
