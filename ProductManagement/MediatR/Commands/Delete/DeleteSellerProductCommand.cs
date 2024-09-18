using MediatR;

namespace ProductManagement.MediatR.Commands.Delete;

public record DeleteSellerProductCommand(int Id) : BaseDeleteCommand(Id);