﻿using TechnoBit.Models;

namespace TechnoBit.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> AddUserAsync(User user);
    Task<User> UpdateUserAsync(User user);
    void DeleteUserAsync(User user);
}