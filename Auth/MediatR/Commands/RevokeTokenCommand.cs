using MediatR;

namespace TechnoBit.MediatR.Commands;

public record RevokeTokenCommand(string RefreshToken) : IRequest;
