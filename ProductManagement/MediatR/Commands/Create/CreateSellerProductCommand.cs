using MediatR;
using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;

namespace ProductManagement.MediatR.Commands.Create;

public record CreateSellerProductCommand(CreateSellerProductDTO dto) : BaseCreateCommand<CreateSellerProductDTO>(dto);