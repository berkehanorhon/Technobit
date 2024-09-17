using MediatR;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;

namespace ProductManagement.MediatR.Commands.Update;

public record UpdateCategoryCommand(UpdateCategoryDTO dto) : BaseUpdateCommand<UpdateCategoryDTO>(dto);