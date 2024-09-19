﻿using ProductManagement.DTOs.Create;
using ProductManagement.DTOs.Read;
using ProductManagement.DTOs.Update;
using ProductManagement.Models;

namespace ProductManagement.Interfaces;


public interface ISellerProductService : IService<CreateSellerProductDTO, SellerProductDTO, UpdateSellerProductDTO>;