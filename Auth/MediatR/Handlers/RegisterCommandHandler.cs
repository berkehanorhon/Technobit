using MediatR;
using TechnoBit.Interfaces;
using TechnoBit.MediatR.Commands;
using TechnoBit.Models;

namespace TechnoBit.MediatR.Handlers;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByUsernameAsync(request.Username) != null || 
            await _userRepository.GetUserByEmailAsync(request.Email) != null)
        {
            throw new Exception("Kullanıcı adı veya e-posta zaten mevcut.");
        }

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        await _userRepository.AddModelAsync(new User
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            Email = request.Email
        });
        
        
        return Unit.Value;
    }
}
