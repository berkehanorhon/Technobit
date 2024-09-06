using MediatR;
using TechnoBit.DTOs;

namespace TechnoBit.MediatR.Commands;

public record LoginCommand(string Username, string Password) : IRequest<TokenDTO>;
