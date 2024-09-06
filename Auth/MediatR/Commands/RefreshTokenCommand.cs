using MediatR;
using TechnoBit.DTOs;

namespace TechnoBit.MediatR.Commands;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<TokenDTO>;
