using MediatR;

namespace TechnoBit.MediatR.Commands;

public record RegisterCommand(string Username, string Password, string Email) : IRequest;
