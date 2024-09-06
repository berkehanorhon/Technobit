using MediatR;
using TechnoBit.Interfaces;
using TechnoBit.MediatR.Commands;

namespace TechnoBit.MediatR.Handlers;

public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
{
    private readonly IUserRepository _userRepository;

    public RevokeTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Unit> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByTokenAsync(request.RefreshToken);

        if (user == null)
            throw new Exception("Geçersiz token.");

        user.RefreshToken = null;
        await _userRepository.UpdateModelAsync(user);

        return Unit.Value;
    }
}
