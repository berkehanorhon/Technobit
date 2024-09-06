using MediatR;
using TechnoBit.DTOs;
using TechnoBit.Interfaces;
using TechnoBit.MediatR.Commands;

namespace TechnoBit.MediatR.Handlers;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public RefreshTokenCommandHandler(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<TokenDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(request.AccessToken);
        var username = principal.Identity.Name;

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedAccessException("Geçersiz veya süresi dolmuş refresh token.");
        }

        var returnDto = new TokenDTO
        {
            AccessToken = _tokenService.GenerateAccessToken(principal.Claims),
            RefreshToken = _tokenService.GenerateRefreshToken()
        };

        user.RefreshToken = returnDto.RefreshToken;
        await _userRepository.UpdateModelAsync(user);

        return returnDto;
    }
}
