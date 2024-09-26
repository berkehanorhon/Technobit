﻿using ProductManagement.DTOs.Update;
using ProductManagement.Interfaces;
using ProductManagement.MediatR.Commands.Delete;
using ProductManagement.MediatR.Handlers.Commands.Update;

namespace ProductManagement.MediatR.Handlers.Commands.Delete;


public class DeleteProductImageCommandHandler : DeleteBaseCommandHandler<IProductImageService, DeleteProductImageCommand>
{
    public DeleteProductImageCommandHandler(IProductImageService service) : base(service)
    {
    }

}