using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;

namespace ProductManagement.MediatR.Commands.Update;

public record UpdateSellerCommand(UpdateSellerDTO dto) : BaseUpdateCommand<UpdateSellerDTO>(dto);

