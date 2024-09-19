using MediatR;

namespace ProductManagement.MediatR.Commands.Delete;

public record DeleteSellerCommand(int Id) : BaseDeleteCommand(Id);